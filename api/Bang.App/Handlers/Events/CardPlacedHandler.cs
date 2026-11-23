using Bang.App.Events;
using Bang.App.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class CardPlacedHandler : INotificationHandler<CardPlaced>
    {
        private readonly IRealtimeNotificationPublisher notificationPublisher;

        public CardPlacedHandler(IRealtimeNotificationPublisher notificationPublisher)
        {
            this.notificationPublisher = notificationPublisher;
        }

        public Task Handle(CardPlaced notification, CancellationToken cancellationToken)
        {
            var gameId = notification.GameId;
            var playerId = notification.PlayerId;
            var targetPlayerId = notification.TargetPlayerId;
            var card = notification.Card;

            return this.notificationPublisher.PublishCardPlacedAsync(
                gameId,
                playerId,
                targetPlayerId,
                card,
                cancellationToken);
        }
    }
}