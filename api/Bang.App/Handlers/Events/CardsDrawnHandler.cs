using Bang.App.Events;
using Bang.App.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class CardsDrawnHandler : INotificationHandler<CardsDrawn>
    {
        private readonly IRealtimeNotificationPublisher notificationPublisher;

        public CardsDrawnHandler(IRealtimeNotificationPublisher notificationPublisher)
        {
            this.notificationPublisher = notificationPublisher;
        }

        public async Task Handle(CardsDrawn notification, CancellationToken cancellationToken)
        {
            var player = notification.Player;
            var playerId = player.Id;
            var playerName = player.Name;

            var game = notification.Game;
            var gameId = game.Id;

            var cards = notification.Cards;

            await this.notificationPublisher.PublishCardsDrawnAsync(
                gameId,
                game.DeckCount,
                playerName,
                player.CardsInHand,
                cancellationToken);

            await this.notificationPublisher.PublishYourHandAsync(playerId, cards, cancellationToken);
        }
    }
}