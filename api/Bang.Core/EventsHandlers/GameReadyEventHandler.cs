using Bang.Core.Constants;
using Bang.Core.Events;
using Bang.Core.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Bang.Core.NotificationsHandlers
{
    public class GameReadyEventHandler : INotificationHandler<GameReadyEvent>
    {
        private readonly IHubContext<GameHub> gameHub;

        public GameReadyEventHandler(IHubContext<GameHub> gameHub)
        {
            this.gameHub = gameHub;
        }

        public Task Handle(GameReadyEvent notification, CancellationToken cancellationToken)
        {
            var game = notification.Game;

            return this.gameHub.Clients.Group(game.Id.ToString())
                .SendAsync(EventNames.GameReady, game, cancellationToken);
        }
    }
}
