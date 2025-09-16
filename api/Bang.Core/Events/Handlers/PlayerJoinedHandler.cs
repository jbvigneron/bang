using Bang.Core.Constants;
using Bang.Core.Extensions;
using Bang.Core.Hubs;
using Bang.Core.Notifications;
using Bang.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Bang.Core.Events.Handlers
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
                .SendAsync(HubMessages.Game.PlayerJoined, gameId, player, cancellationToken);

            if (game.Status == GameStatus.InProgress)
            {
                await this.gameHub
                    .Clients.Group(gameId.ToString())
                    .SendAsync(HubMessages.Game.AllPlayerJoined, gameId, game, cancellationToken);

                var sheriff = game.GetSheriff();

                await this.gameHub
                    .Clients.Group(gameId.ToString())
                    .SendAsync(HubMessages.Game.PlayerTurn, gameId, sheriff.Name, cancellationToken);

                await this.playerHub
                    .Clients.Group(sheriff.Id.ToString())
                    .SendAsync(HubMessages.Player.YourTurn, cancellationToken);
            }
        }
    }
}
