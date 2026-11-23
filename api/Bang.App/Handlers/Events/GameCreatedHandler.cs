using Bang.App.Events;
using Bang.App.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class GameCreatedHandler : INotificationHandler<GameCreated>
    {
        private readonly IRealtimeNotificationPublisher notificationPublisher;

        public GameCreatedHandler(IRealtimeNotificationPublisher notificationPublisher)
        {
            this.notificationPublisher = notificationPublisher;
        }

        public Task Handle(GameCreated notification, CancellationToken cancellationToken)
        {
            return this.notificationPublisher.PublishGameCreatedAsync(notification.Game, cancellationToken);
        }
    }
}