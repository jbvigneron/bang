using Bang.Core.Constants;
using Bang.Core.Hubs;
using Bang.Database;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Events.Handlers
{
    public class PlayerDrawCardsHandler : INotificationHandler<PlayerDrawCards>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;
        private readonly IHubContext<PlayerHub> playerHub;

        public PlayerDrawCardsHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub, IHubContext<PlayerHub> playerHub)
        {
            this.dbContext = dbContext;
            this.gameHub = gameHub;
            this.playerHub = playerHub;
        }

        public async Task Handle(PlayerDrawCards notification, CancellationToken cancellationToken)
        {
            var playerId = notification.PlayerId;
            var playerName = notification.PlayerName;
            var gameId = notification.GameId;

            var hand = await this.dbContext.PlayersHands
                .Include(d => d.Cards)
                .Include(d => d.Player)
                .SingleAsync(p => p.PlayerId == playerId, cancellationToken);

            var gameDeck = await this.dbContext.GamesDecks
                .Include(d => d.Cards)
                .Include(d => d.Game)
                .SingleAsync(d => d.GameId == gameId, cancellationToken);

            var game = gameDeck.Game;
            var player = hand.Player;

            for (var i = 1; i <= 2; i++)
            {
                var card = gameDeck.Cards.First();

                hand.Cards.Add(card);
                player.CardsInHand++;

                gameDeck.Cards.Remove(card);
                game.DeckCount--;
            }

            player.HasDrawnCards = true;

            await this.dbContext.SaveChangesAsync(cancellationToken);

            await gameHub
                .Clients.Group(gameId.ToString())
                .SendAsync(HubMessages.Game.CardsDrawn, gameId, game.DeckCount, playerName, player.CardsInHand, cancellationToken);

            await playerHub
                .Clients.Group(playerId.ToString())
                .SendAsync(HubMessages.Player.CardsInHand, hand.Cards, cancellationToken);
        }
    }
}
