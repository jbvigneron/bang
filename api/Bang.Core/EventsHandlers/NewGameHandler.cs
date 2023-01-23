using Bang.Core.Constants;
using Bang.Core.Events;
using Bang.Core.Exceptions;
using Bang.Core.Hubs;
using Bang.Database;
using Bang.Database.Enums;
using Bang.Database.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Bang.Core.EventsHandlers
{
    public class NewGameHandler : INotificationHandler<NewGame>
    {
        private readonly BangDbContext dbContext;
        private readonly IHubContext<PublicHub> publicHub;

        private readonly List<Role> roles = new()
        {
            Role.Sheriff,
            Role.Renegade,
            Role.Outlaw,
            Role.Outlaw
        };

        public NewGameHandler(BangDbContext dbContext, IHubContext<PublicHub> publicHub)
        {
            this.dbContext = dbContext;
            this.publicHub = publicHub;
        }

        public async Task Handle(NewGame notification, CancellationToken cancellationToken)
        {
            if (notification.PlayerNames.Distinct().Count() != notification.PlayerNames.Count())
            {
                throw new GameException("Les joueurs doivent avoir des noms différents");
            }

            var game = new Game
            {
                Id = notification.GameId,
                GameStatus = GameStatus.WaitingForPlayers,
                Players = new List<Player>(),
                DiscardPile = new List<GameDiscard>()
            };

            this.DetermineAvailablesRoles(notification.PlayerNames.Count());

            foreach (var playerName in notification.PlayerNames)
            {
                var player = new Player
                {
                    Name = playerName,
                    Status = PlayerStatus.NotReady,
                    Role = GetRandomRole(),
                };

                if (player.Role.Value == Role.Sheriff)
                {
                    player.IsScheriff = true;
                    game.CurrentPlayerName = player.Name;
                }

                game.Players.Add(player);
            }

            await this.dbContext.Games.AddAsync(game, cancellationToken);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            await this.publicHub.Clients.All.SendAsync(HubMessages.Public.NewGame, game, cancellationToken);
        }

        private void DetermineAvailablesRoles(int numberOfPlayers)
        {
            if (numberOfPlayers < 4 || numberOfPlayers > 7)
                throw new ArgumentOutOfRangeException(nameof(numberOfPlayers), "Le nombre de joueurs doit être compris entre 4 et 7");

            if (numberOfPlayers >= 5)
                this.roles.Add(Role.Assistant);

            if (numberOfPlayers >= 6)
                this.roles.Add(Role.Outlaw);

            if (numberOfPlayers == 7)
                this.roles.Add(Role.Assistant);
        }

        private PlayerRole GetRandomRole()
        {
            var roleIndex = new Random().Next(this.roles.Count);
            var role = this.roles[roleIndex];

            this.roles.RemoveAt(roleIndex);

            return new PlayerRole
            {
                Value = role
            };
        }
    }
}
