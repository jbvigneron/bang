using Bang.App.Constants;
using Bang.App.Hubs;
using Bang.Domain.Events;
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
                    .SendAsync(HubMessages.Game.WeaponChanged, gameId, playerId, weapon, cancellationToken);
        }
    }
}