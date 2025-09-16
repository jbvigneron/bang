using Bang.Core.Constants;
using Bang.Core.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Bang.Core.Events.Handlers
{
    public class GameCreatedHandler : INotificationHandler<GameCreated>
    {
        private readonly IHubContext<PublicHub> publicHub;

        public GameCreatedHandler(IHubContext<PublicHub> publicHub)
        {
            this.publicHub = publicHub;
        }

        public Task Handle(GameCreated notification, CancellationToken cancellationToken)
        {
            var game = notification.Game;
            return this.publicHub.Clients.All.SendAsync(HubMessages.Public.GameCreated, game, cancellationToken);
        }
    }
}
