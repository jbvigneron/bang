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
            var hand = await dbContext.PlayersHands
                .Include(d => d.Cards)
                .Include(d => d.Player)
                .SingleAsync(p => p.PlayerId == notification.PlayerId, cancellationToken);

            var player = hand.Player;

            var gameDeck = await dbContext.GamesDecks
                .Include(d => d.Cards)
                .Include(d => d.Game)
                .SingleAsync(d => d.GameId == player.GameId, cancellationToken);

            var game = gameDeck.Game;

            for (var i = 1; i <= 2; i++)
            {
                var card = gameDeck.Cards.First();

                hand.Cards.Add(card);
                player.CardsInHand++;

                gameDeck.Cards.Remove(card);
                game.DeckCount--;
            }

            player.HasDrawnCards = true;

            await dbContext.SaveChangesAsync(cancellationToken);

            await gameHub
                .Clients.Group(game.Id.ToString())
                .SendAsync(HubMessages.Game.CardsDrawn, game.Id, game.DeckCount, player.Name, player.CardsInHand, cancellationToken);

            await playerHub
                .Clients.Group(player.Id.ToString())
                .SendAsync(HubMessages.Player.CardsInHand, hand.Cards, cancellationToken);
        }
    }
}
