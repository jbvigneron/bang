using Bang.Core.Constants;
using Bang.Core.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.Core.Events.Handlers
{
    public class GameCreatedNotificationHandler : INotificationHandler<GameCreated>
    {
        private readonly IHubContext<PublicHub> publicHub;

        public GameCreatedNotificationHandler(IHubContext<PublicHub> publicHub)
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
