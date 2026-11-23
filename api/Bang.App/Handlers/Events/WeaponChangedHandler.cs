using Bang.App.Events;
using Bang.App.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class WeaponChangedHandler : INotificationHandler<WeaponChanged>
    {
        private readonly IRealtimeNotificationPublisher notificationPublisher;

        public WeaponChangedHandler(IRealtimeNotificationPublisher notificationPublisher)
        {
            this.notificationPublisher = notificationPublisher;
        }

        public Task Handle(WeaponChanged notification, CancellationToken cancellationToken)
        {
            var gameId = notification.GameId;
            var playerId = notification.PlayerId;
            var weapon = notification.Weapon;

            return this.notificationPublisher.PublishWeaponChangedAsync(
                gameId,
                playerId,
                weapon,
                cancellationToken);
        }
    }
}