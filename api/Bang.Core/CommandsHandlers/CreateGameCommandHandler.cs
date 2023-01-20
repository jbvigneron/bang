using Bang.Core.Commands;
using Bang.Core.Exceptions;
using Bang.Database;
using Bang.Database.Enums;
using Bang.Database.Models;
using MediatR;

namespace Bang.Core.CommandsHandlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Game>
    {
        private readonly BangDbContext context;

        private readonly List<RoleEnum> roles = new()
        {
            RoleEnum.Sheriff,
            RoleEnum.Renegade,
            RoleEnum.Outlaw,
            RoleEnum.Outlaw
        };

        public CreateGameCommandHandler(BangDbContext context)
        {
            this.context = context;
        }

        public async Task<Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            if(request.PlayerNames.Distinct().Count() != request.PlayerNames.Count())
            {
                throw new GameException("Les joueurs doivent avoir des noms différents");
            }

            this.DetermineRolesAvailables(request);

            var game = new Game
            {
                GameStatus = GameStatusEnum.WaitingForPlayers,
                Players = new List<Player>()
            };

            this.AssignRolesToAllPlayers(request, game);

            await context.Games.AddAsync(game, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return game;
        }
        
        private void DetermineRolesAvailables(CreateGameCommand request)
        {
            if (request.PlayerNames.Count() < 4 || request.PlayerNames.Count() > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(request.PlayerNames), "Le nombre de joueurs doit être compris entre 4 et 7");
            }

            if (request.PlayerNames.Count() >= 5)
            {
                this.roles.Add(RoleEnum.Assistant);
            }

            if (request.PlayerNames.Count() >= 6)
            {
                this.roles.Add(RoleEnum.Outlaw);
            }

            if (request.PlayerNames.Count() == 7)
            {
                this.roles.Add(RoleEnum.Assistant);
            }
        }

        private void AssignRolesToAllPlayers(CreateGameCommand request, Game game)
        {
            foreach (var playerName in request.PlayerNames)
            {
                var player = new Player
                {
                    Name = playerName,
                    Status = PlayerStatusEnum.NotReady
                };

                this.AssignRoleToPlayer(player);
                game.Players.Add(player);
            }
        }

        private void AssignRoleToPlayer(Player player)
        {
            var roleIndex = new Random().Next(this.roles.Count);

            player.Role = new PlayerRole
            {
                Value = this.roles[roleIndex]
            };

            if (player.Role.Value == RoleEnum.Sheriff)
            {
                player.IsScheriff = true;
                player.Lives++;
            }

            this.roles.RemoveAt(roleIndex);
        }
    }
}
