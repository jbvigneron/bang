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
    public class DeckInitializeHandler : INotificationHandler<DeckInitialize>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<PublicHub> publicHub;

        public DeckInitializeHandler(BangDbContext dbContext, IHubContext<PublicHub> publicHub)
        {
            this.dbContext = dbContext;
            this.publicHub = publicHub;
        }

        public async Task Handle(DeckInitialize notification, CancellationToken cancellationToken)
        {
            var cards = this.dbContext.Cards.OrderBy(c => Guid.NewGuid());

            var deck = new GameDeck
            {
                GameId = notification.GameId,
                Cards = cards
            };

            var game = await this.dbContext.Games.SingleAsync(g => g.Id == notification.GameId, cancellationToken);
            game.DeckCount = cards.Count();

            await this.dbContext.SaveChangesAsync(cancellationToken);
            await this.publicHub.Clients.All.SendAsync(HubMessages.DeckReady, game, cancellationToken);
        }
    }
}
