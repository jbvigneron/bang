using Bang.Core.Constants;
using Bang.Core.Events;
using Bang.Core.Hubs;
using Bang.Database;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.NotificationsHandlers
{
    public class WeaponCardPlayHandler : INotificationHandler<WeaponCardPlay>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;
        private readonly IHubContext<PlayerHub> playerHub;

        public WeaponCardPlayHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub, IHubContext<PlayerHub> playerHub)
        {
            this.dbContext = dbContext;

            this.gameHub = gameHub;
            this.playerHub = playerHub;
        }

        public async Task Handle(WeaponCardPlay notification, CancellationToken cancellationToken)
        {
            var (playerId, cardId) = notification;

            var hand = await this.dbContext.PlayersHands
                .Include(d => d.Cards)
                .Include(d => d.Player)
                    .ThenInclude(p => p.CardsInGame)
                .SingleAsync(p => p.PlayerId == playerId, cancellationToken);

            var card = hand.Cards.First(c => c.Id == cardId);

            hand.Cards.Remove(card);
            hand.Player.CardsInGame.Add(card);
            hand.Player.Weapon = await this.dbContext.Weapons.SingleAsync(w => w.Id.ToString() == card.Kind.ToString(), cancellationToken);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.gameHub
                .Clients.Group(hand.Player.GameId.ToString())
                .SendAsync(HubMessages.Game.WeaponChanged, hand.Player.GameId, playerId, hand.Player.Weapon, cancellationToken);

            await this.playerHub
                .Clients.Group(notification.PlayerId.ToString())
                .SendAsync(HubMessages.Player.CardsInHand, hand.Cards, cancellationToken);
        }
    }
}
