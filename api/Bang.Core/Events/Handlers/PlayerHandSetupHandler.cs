using Bang.Core.Constants;
using Bang.Core.Hubs;
using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Events.Handlers
{
    public class PlayerHandSetupHandler : INotificationHandler<PlayerHandSetup>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;

        public PlayerHandSetupHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub)
        {
            this.dbContext = dbContext;
            this.gameHub = gameHub;
        }

        public async Task Handle(PlayerHandSetup notification, CancellationToken cancellationToken)
        {
            var playerId = notification.PlayerId;
            var gameId = notification.GameId;

            var hand = new PlayerHand
            {
                PlayerId = playerId,
                Cards = new List<Card>()
            };

            var player = await this.dbContext.Players.FirstAsync(p => p.Id == playerId, cancellationToken);

            var gameDeck = await this.dbContext.GamesDecks
                .Include(d => d.Cards)
                .Include(d => d.Game)
                .FirstAsync(d => d.Game.Players.Any(p => p.Id == playerId), cancellationToken);

            var game = gameDeck.Game;

            for (int i = 1; i <= player.Lives; i++)
            {
                var card = gameDeck.Cards.First();

                hand.Cards.Add(card);
                player.CardsInHand++;

                gameDeck.Cards.Remove(card);
                game.DeckCount--;
            }

            await this.dbContext.PlayersHands.AddAsync(hand, cancellationToken);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.gameHub
                .Clients.Group(gameId.ToString())
                .SendAsync(HubMessages.Game.DeckUpdated, gameId, game.DeckCount, cancellationToken);
        }
    }
}
