using Bang.Core.Constants;
using Bang.Core.Events;
using Bang.Core.Hubs;
using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.EventsHandlers
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

            var player = await this.dbContext.Players.FirstAsync(p => p.Id == notification.PlayerId, cancellationToken);

            var gameDeck = await this.dbContext.GamesDecks
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

            await this.dbContext.PlayersHands.AddAsync(hand, cancellationToken);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.gameHub
                .Clients.Group(game.Id.ToString())
                .SendAsync(HubMessages.Game.DeckUpdated, game.Id, game.DeckCount, cancellationToken);
        }
    }
}
