using Bang.App.Events;
using Bang.App.Extensions;
using Bang.App.Notifications;
using Bang.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class PlayerJoinedHandler : INotificationHandler<PlayerJoined>
    {
        private readonly IRealtimeNotificationPublisher notificationPublisher;

        public PlayerJoinedHandler(IRealtimeNotificationPublisher notificationPublisher)
        {
            this.notificationPublisher = notificationPublisher;
        }

        public async Task Handle(PlayerJoined notification, CancellationToken cancellationToken)
        {
            var game = notification.Game;
            var gameId = game.Id;
            var player = notification.Player;

            await this.notificationPublisher.PublishPlayerJoinedAsync(gameId, player, cancellationToken);

            if (game.Status == GameStatus.InProgress)
            {
                await this.notificationPublisher.PublishAllPlayersJoinedAsync(gameId, game, cancellationToken);

                var sheriff = game.GetSheriff();

                await this.notificationPublisher.PublishPlayerTurnAsync(gameId, sheriff.Name, cancellationToken);

                await this.notificationPublisher.PublishYourTurnAsync(sheriff.Id, cancellationToken);
            }
        }
    }
}