using Bang.Core.Constants;
using Bang.Core.Exceptions;
using Bang.Core.Extensions;
using Bang.Core.Hubs;
using Bang.Core.Notifications;
using Bang.Database;
using Bang.Database.Enums;
using Bang.Database.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.NotificationsHandlers
{
    public class PlayerJoinHandler : INotificationHandler<PlayerJoin>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<PublicHub> publicHub;
        private readonly IHubContext<InGameHub> inGameHub;

        public PlayerJoinHandler(BangDbContext dbContext, IHubContext<PublicHub> generalHub, IHubContext<InGameHub> inGameHub)
        {
            this.dbContext = dbContext;
            this.publicHub = generalHub;
            this.inGameHub = inGameHub;
        }

        public async Task Handle(PlayerJoin notification, CancellationToken cancellationToken)
        {
            var game = await this.dbContext.Games
                .Include(g => g.Players)
                .FirstAsync(g => g.Id == notification.GameId, cancellationToken);

            if (game.GameStatus != GameStatus.WaitingForPlayers)
            {
                throw new GameException("L'identifiant de la partie est incorrect");
            }

            var player = game.Players.First(p => p.Name == notification.PlayerName);
            player.Character = await this.GetRandomCharacterAsync(cancellationToken);
            player.Lives = GetLives(player.Character, player.IsScheriff);
            player.Weapon = await this.GetColt45Async(cancellationToken);
            player.Status = PlayerStatus.Alive;

            if (game.Players.All(p => p.Status == PlayerStatus.Alive))
            {
                game.GameStatus = GameStatus.InProgress;
            }

            await this.dbContext.SaveChangesAsync(cancellationToken);

            var groupId = game.Id.ToString();
            await this.inGameHub.Clients.Group(groupId).SendAsync(HubMessages.PlayerJoin, player, cancellationToken);

            if (game.GameStatus == GameStatus.InProgress)
            {
                await this.publicHub.Clients.All.SendAsync(HubMessages.AllPlayerJoined, game, cancellationToken);

                var scheriff = game.GetScheriff();
                await this.publicHub.Clients.All.SendAsync(HubMessages.ItsYourTurn, scheriff.Name, cancellationToken);
            }
        }
        private Task<Character> GetRandomCharacterAsync(CancellationToken cancellationToken) =>
            this.dbContext.Characters.OrderBy(c => Guid.NewGuid()).FirstAsync(cancellationToken);

        private static int GetLives(Character character, bool isScheriff)
        {
            var lives = character.Lives;
            lives += isScheriff ? 1 : 0;
            return lives;
        }

        private Task<Weapon> GetColt45Async(CancellationToken cancellationToken) =>
            this.dbContext.Weapons.SingleAsync(w => w.Id == WeaponKind.Colt45, cancellationToken);
    }
}
