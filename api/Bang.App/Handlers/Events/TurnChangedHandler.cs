using Bang.App.Events;
using Bang.App.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class TurnChangedHandler : INotificationHandler<TurnChanged>
    {
        private readonly IRealtimeNotificationPublisher notificationPublisher;

        public TurnChangedHandler(IRealtimeNotificationPublisher notificationPublisher)
        {
            this.notificationPublisher = notificationPublisher;
        }

        public async Task Handle(TurnChanged notification, CancellationToken cancellationToken)
        {
            await this.notificationPublisher.PublishPlayerTurnAsync(
                notification.GameId,
                notification.PlayerName,
                cancellationToken);

            await this.notificationPublisher.PublishYourTurnAsync(notification.PlayerId, cancellationToken);
        }
    }
}
