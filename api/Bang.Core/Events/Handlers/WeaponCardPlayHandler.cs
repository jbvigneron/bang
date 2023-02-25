using Bang.Core.Constants;
using Bang.Core.Hubs;
using Bang.Database;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Events.Handlers
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
            var playerId = notification.PlayerId;
            var gameId = notification.GameId;
            var cardId = notification.CardId;

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

            await gameHub
                .Clients.Group(gameId.ToString())
                .SendAsync(HubMessages.Game.WeaponChanged, gameId, playerId, hand.Player.Weapon, cancellationToken);

            await playerHub
                .Clients.Group(playerId.ToString())
                .SendAsync(HubMessages.Player.CardsInHand, hand.Cards, cancellationToken);
        }
    }
}
