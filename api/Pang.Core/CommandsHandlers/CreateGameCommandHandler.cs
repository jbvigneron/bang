using MediatR;
using Pang.Core.Commands;
using Pang.Database;
using Pang.Database.Models;

namespace Pang.Core.CommandsHandlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Game>
    {
        private readonly PangDbContext context;
        private readonly List<PlayerRole> roles;

        public CreateGameCommandHandler(PangDbContext context)
        {
            this.context = context;

            this.roles = new List<PlayerRole> {
                PlayerRole.Sheriff,
                PlayerRole.Renegade,
                PlayerRole.Outlaw,
                PlayerRole.Outlaw
            };
        }

        public async Task<Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            if (request.PlayerNames.Count() < 4 || request.PlayerNames.Count() > 7)
            {
                throw new ArgumentOutOfRangeException("request.PlayerNames", "Le nombre de joueurs doit être compris entre 4 et 7");
            }

            if (request.PlayerNames.Count() >= 5)
            {
                this.roles.Add(PlayerRole.Assistant);
            }

            if (request.PlayerNames.Count() >= 6)
            {
                this.roles.Add(PlayerRole.Outlaw);
            }

            if (request.PlayerNames.Count() == 7)
            {
                this.roles.Add(PlayerRole.Assistant);
            }

            var game = new Game
            {
                GameStatus = GameStatus.Pending,
                Players = new List<Player>()
            };

            foreach (var playerName in request.PlayerNames)
            {
                var player = new Player
                {
                    Name = playerName
                };

                var roleIndex = new Random().Next(roles.Count);
                player.Role = roles[roleIndex];
                roles.RemoveAt(roleIndex);

                game.Players.Add(player);
            }

            await context.Game.AddAsync(game, cancellationToken);
            return game;
        }
    }
}
