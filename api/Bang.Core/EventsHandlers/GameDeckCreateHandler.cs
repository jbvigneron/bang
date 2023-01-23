using Bang.Core.Constants;
using Bang.Core.Events;
using Bang.Core.Hubs;
using Bang.Database;
using Bang.Database.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.EventsHandlers
{
    public class GameDeckCreateHandler : INotificationHandler<GameDeckPrepare>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;

        public GameDeckCreateHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub)
        {
            this.dbContext = dbContext;
            this.gameHub = gameHub;
        }

        public async Task Handle(GameDeckPrepare notification, CancellationToken cancellationToken)
        {
            var cards = await this.dbContext.Cards.OrderBy(c => Guid.NewGuid()).ToListAsync(cancellationToken);

            var deck = new GameDeck
            {
                GameId = notification.GameId,
                Cards = cards
            };

            await this.dbContext.GameDecks.AddAsync(deck, cancellationToken);

            var game = await this.dbContext.Games.SingleAsync(g => g.Id == notification.GameId, cancellationToken);
            game.DeckCount = cards.Count;

            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.gameHub
                .Clients.Group(game.Id.ToString())
                .SendAsync(HubMessages.Game.GameDeckReady, game.Id, cards.Count, cancellationToken);
        }
    }
}
