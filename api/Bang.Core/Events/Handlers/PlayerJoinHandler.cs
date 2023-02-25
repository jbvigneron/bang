using Bang.Core.Constants;
using Bang.Core.Exceptions;
using Bang.Core.Extensions;
using Bang.Core.Hubs;
using Bang.Core.Notifications;
using Bang.Database;
using Bang.Models;
using Bang.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Events.Handlers
{
    public class PlayerJoinHandler : INotificationHandler<PlayerJoin>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<GameHub> gameHub;
        private readonly IHubContext<PlayerHub> playerHub;

        public PlayerJoinHandler(BangDbContext dbContext, IHubContext<GameHub> gameHub, IHubContext<PlayerHub> playerHub)
        {
            this.dbContext = dbContext;
            this.gameHub = gameHub;
            this.playerHub = playerHub;
        }

        public async Task Handle(PlayerJoin notification, CancellationToken cancellationToken)
        {
            var gameId = notification.GameId;
            var playerName = notification.PlayerName;

            var game = await this.dbContext.Games
                .Include(g => g.Players)
                .FirstAsync(g => g.Id == gameId, cancellationToken);

            if (game.Status != GameStatus.WaitingForPlayers)
            {
                throw new GameException("L'identifiant de la partie est incorrect", gameId);
            }

            var player = game.Players.First(p => p.Name == playerName);
            player.Character = await GetRandomCharacterAsync(cancellationToken);
            player.Lives = GetLives(player.Character, player.IsSheriff);
            player.Weapon = await GetColt45Async(cancellationToken);
            player.Status = PlayerStatus.Alive;

            if (game.Players.All(p => p.Status == PlayerStatus.Alive))
            {
                game.Status = GameStatus.InProgress;
            }

            await this.dbContext.SaveChangesAsync(cancellationToken);

            await gameHub
                .Clients.Group(gameId.ToString())
                .SendAsync(HubMessages.Game.PlayerJoin, gameId, player, cancellationToken);

            if (game.Status == GameStatus.InProgress)
            {
                await gameHub
                    .Clients.Group(gameId.ToString())
                    .SendAsync(HubMessages.Game.AllPlayerJoined, gameId, game, cancellationToken);

                var sheriff = game.GetSheriff();

                await gameHub
                    .Clients.Group(gameId.ToString())
                    .SendAsync(HubMessages.Game.PlayerTurn, gameId, sheriff.Name, cancellationToken);

                await playerHub
                    .Clients.Group(sheriff.Id.ToString())
                    .SendAsync(HubMessages.Player.YourTurn, cancellationToken);
            }
        }
        private Task<Character> GetRandomCharacterAsync(CancellationToken cancellationToken) =>
            dbContext.Characters.OrderBy(c => Guid.NewGuid()).FirstAsync(cancellationToken);

        private static int GetLives(Character character, bool isScheriff)
        {
            var lives = character.Lives;
            lives += isScheriff ? 1 : 0;
            return lives;
        }

        private Task<Weapon> GetColt45Async(CancellationToken cancellationToken) =>
            dbContext.Weapons.SingleAsync(w => w.Id == WeaponKind.Colt45, cancellationToken);
    }
}
