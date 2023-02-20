using Bang.Core.Constants;
using Bang.Core.Events;
using Bang.Core.Hubs;
using Bang.Database;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.NotificationsHandlers
{
    public class ChangeWeaponHandler : INotificationHandler<ChangeWeapon>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;
        private readonly IHubContext<PlayerHub> playerHub;

        public ChangeWeaponHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub, IHubContext<PlayerHub> playerHub)
        {
            this.dbContext = dbContext;
            this.gameHub = gameHub;
            this.playerHub = playerHub;
        }

        public async Task Handle(ChangeWeapon notification, CancellationToken cancellationToken)
        {
            var playerDeck = await this.dbContext.PlayersDecks
                .Include(d => d.Cards)
                .Include(d => d.Player)
                    .ThenInclude(p => p.CardsInGame)
                .SingleAsync(p => p.PlayerId == notification.PlayerId, cancellationToken);

            var game = await this.dbContext.Games
                .Include(g => g.Players)
                .SingleAsync(g => g.Players.Any(p => p.Id == notification.PlayerId));

            var player = playerDeck.Player;
            var card = playerDeck.Cards.First(c => c.Id == notification.Card.Id);
            player.CardsInGame.Add(card);
            player.Weapon = await this.dbContext.Weapons.SingleAsync(w => w.Id.ToString() == card.Kind.ToString(), cancellationToken);

            playerDeck.Cards.Remove(card);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.gameHub
                .Clients.Group(game.Id.ToString())
                .SendAsync(HubMessages.Game.WeaponChanged, player, cancellationToken);

            await this.playerHub
                .Clients.Group(player.Id.ToString())
                .SendAsync(HubMessages.Player.CardsInHand, playerDeck.Cards, cancellationToken);
        }
    }
}
