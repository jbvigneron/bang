using Bang.Core.Constants;
using Bang.Core.Hubs;
using Bang.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Bang.Core.NotificationsHandlers
{
    public class PlayerJoinedHandler : INotificationHandler<PlayerReadyEvent>
    {
        private readonly IHubContext<GameHub> gameHub;

        public PlayerJoinedHandler(IHubContext<GameHub> gameHub)
        {
            this.gameHub = gameHub;
        }

        public Task Handle(PlayerReadyEvent notification, CancellationToken cancellationToken)
        {
            var game = notification.Game;
            var player = notification.Player;

            return this.gameHub.Clients.Group(game.Id.ToString())
                .SendAsync(EventNames.PlayerReady, player, cancellationToken);
        }
    }
}
