using Bang.App.Events;
using Bang.App.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
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
            return this.publicHub.Clients.All.SendAsync(Domain.Constants.Events.Public.GameCreated, game, cancellationToken);
        }
    }
}