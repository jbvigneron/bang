using Bang.App.Events;
using Bang.App.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class CardDiscardedHandler : INotificationHandler<CardDiscarded>
    {
        private readonly IRealtimeNotificationPublisher notificationPublisher;

        public CardDiscardedHandler(IRealtimeNotificationPublisher notificationPublisher)
        {
            this.notificationPublisher = notificationPublisher;
        }

        public Task Handle(CardDiscarded notification, CancellationToken cancellationToken)
        {
            var gameId = notification.GameId;
            var playerId = notification.PlayerId;
            var card = notification.Card;

            return this.notificationPublisher.PublishCardDiscardedAsync(
                gameId,
                playerId,
                card,
                cancellationToken);
        }
    }
}