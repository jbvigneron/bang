using Bang.Core.Constants;
using Bang.Core.Hubs;
using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Events.Handlers
{
    public class PlayerPrepareDeckHandler : INotificationHandler<PlayerPrepareDeck>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;

        public PlayerPrepareDeckHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub)
        {
            this.dbContext = dbContext;
            this.gameHub = gameHub;
        }

        public async Task Handle(PlayerPrepareDeck notification, CancellationToken cancellationToken)
        {
            var hand = new PlayerHand
            {
                PlayerId = notification.PlayerId,
                Cards = new List<Card>()
            };

            var player = await dbContext.Players.FirstAsync(p => p.Id == notification.PlayerId, cancellationToken);

            var gameDeck = await dbContext.GamesDecks
                .Include(d => d.Cards)
                .Include(d => d.Game)
                .FirstAsync(d => d.Game.Players.Any(p => p.Id == notification.PlayerId), cancellationToken);

            var game = gameDeck.Game;

            for (int i = 1; i <= player.Lives; i++)
            {
                var card = gameDeck.Cards.First();

                hand.Cards.Add(card);
                player.CardsInHand++;

                gameDeck.Cards.Remove(card);
                game.DeckCount--;
            }

            await dbContext.PlayersHands.AddAsync(hand, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            await gameHub
                .Clients.Group(game.Id.ToString())
                .SendAsync(HubMessages.Game.DeckUpdated, game.Id, game.DeckCount, cancellationToken);
        }
    }
}
