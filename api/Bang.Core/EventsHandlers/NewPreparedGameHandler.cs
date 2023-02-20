using Bang.Core.Events;
using Bang.Core.Constants;
using Bang.Core.Hubs;
using Bang.Database;
using Bang.Models;
using Bang.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Bang.Core.EventsHandlers
{
    public class NewPreparedGameHandler : INotificationHandler<NewPreparedGame>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<PublicHub> publicHub;

        public NewPreparedGameHandler(BangDbContext dbContext, IHubContext<PublicHub> publicHub)
        {
            this.dbContext = dbContext;
            this.publicHub = publicHub;
        }

        public async Task Handle(NewPreparedGame notification, CancellationToken cancellationToken)
        {
            var game = new Game
            {
                Id = notification.GameId,
                Status = GameStatus.WaitingForPlayers,
                Players = notification.Players.Select(player =>
                {
                    var character = this.dbContext.Characters.First(c => c.Id == player.Character);
                    var role = this.dbContext.Roles.First(r => r.Id == player.Role);
                    var isSheriff = role.Id == RoleKind.Sheriff;

                    return new Player
                    {
                        Name = player.Name,
                        Role = role,
                        IsSheriff = isSheriff,
                        Character = character,
                        Lives = character.Lives + (isSheriff ? 1 : 0),
                        Status = PlayerStatus.NotReady,
                        Weapon = this.dbContext.Weapons.First(w => w.Id == WeaponKind.Colt45)
                    };
                }).ToList(),
                DiscardPile = new List<GameDiscard>(),
                CurrentPlayerName = notification.Players.Single(info => info.Role == RoleKind.Sheriff).Name,
            };

            await this.dbContext.Games.AddAsync(game);
            await this.dbContext.SaveChangesAsync(cancellationToken);
            await this.publicHub.Clients.All.SendAsync(HubMessages.Public.NewGame, game, cancellationToken);
        }
    }
}
