using Bang.App.Events;
using Bang.App.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class WeaponChangedHandler : INotificationHandler<WeaponChanged>
    {
        private readonly IHubContext<GameHub> gameHub;

        public WeaponChangedHandler(IHubContext<GameHub> gameHub)
        {
            this.gameHub = gameHub;
        }

        public Task Handle(WeaponChanged notification, CancellationToken cancellationToken)
        {
            var gameId = notification.GameId;
            var playerId = notification.PlayerId;
            var weapon = notification.Weapon;

            return this.gameHub
                    .Clients.Group(gameId.ToString())
                    .SendAsync(Domain.Constants.Events.Game.WeaponChanged, gameId, playerId, weapon, cancellationToken);
        }
    }
}