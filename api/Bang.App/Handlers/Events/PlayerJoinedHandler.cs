using Bang.App.Events;
using Bang.App.Extensions;
using Bang.App.Hubs;
using Bang.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Events
{
    public class PlayerJoinedHandler : INotificationHandler<PlayerJoined>
    {
        private readonly IHubContext<GameHub> gameHub;
        private readonly IHubContext<PlayerHub> playerHub;

        public PlayerJoinedHandler(IHubContext<GameHub> gameHub, IHubContext<PlayerHub> playerHub)
        {
            this.gameHub = gameHub;
            this.playerHub = playerHub;
        }

        public async Task Handle(PlayerJoined notification, CancellationToken cancellationToken)
        {
            var game = notification.Game;
            var gameId = game.Id;
            var player = notification.Player;

            await this.gameHub
                .Clients.Group(gameId.ToString())
                .SendAsync(Domain.Constants.Events.Game.PlayerJoined, gameId, player, cancellationToken);

            if (game.Status == GameStatus.InProgress)
            {
                await this.gameHub
                    .Clients.Group(gameId.ToString())
                    .SendAsync(Domain.Constants.Events.Game.AllPlayerJoined, gameId, game, cancellationToken);

                var sheriff = game.GetSheriff();

                await this.gameHub
                    .Clients.Group(gameId.ToString())
                    .SendAsync(Domain.Constants.Events.Game.PlayerTurn, gameId, sheriff.Name, cancellationToken);

                await this.playerHub
                    .Clients.Group(sheriff.Id.ToString())
                    .SendAsync(Domain.Constants.Events.Player.YourTurn, cancellationToken);
            }
        }
    }
}