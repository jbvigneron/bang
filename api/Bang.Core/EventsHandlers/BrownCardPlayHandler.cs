using Bang.Core.Constants;
using Bang.Core.Events;
using Bang.Core.Hubs;
using Bang.Database;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.NotificationsHandlers
{
    public class BrownCardPlayHandler : INotificationHandler<BrownCardPlay>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;
        private readonly IHubContext<PlayerHub> playerHub;

        public BrownCardPlayHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub, IHubContext<PlayerHub> playerHub)
        {
            this.dbContext = dbContext;

            this.gameHub = gameHub;
            this.playerHub = playerHub;
        }

        public async Task Handle(BrownCardPlay notification, CancellationToken cancellationToken)
        {
            var hand = await this.dbContext.PlayersHands
                .Include(d => d.Cards)
                .Include(d => d.Player)
                    .ThenInclude(p => p.CardsInGame)
                .SingleAsync(p => p.PlayerId == notification.PlayerId, cancellationToken);

            var discardPile = await this.dbContext.GamesDiscardPiles
                .Include(d => d.Game)
                    .ThenInclude(g => g.Players)
                .Include(d => d.Cards)
                .SingleAsync(g => g.Game.Players.Any(p => p.Id == notification.PlayerId), cancellationToken);

            var card = hand.Cards.First(c => c.Id == notification.Card.Id);

            hand.Cards.Remove(card);
            discardPile.Cards.Add(card);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.gameHub
                .Clients.Group(discardPile.GameId.ToString())
                .SendAsync(HubMessages.Game.CardPlaced, discardPile.GameId, hand.PlayerId, card, cancellationToken);

            await this.playerHub
                .Clients.Group(notification.PlayerId.ToString())
                .SendAsync(HubMessages.Player.CardsInHand, hand.Cards, cancellationToken);
        }
    }
}
