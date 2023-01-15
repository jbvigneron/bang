using MediatR;
using Bang.Core.Commands;
using Bang.Database;
using Bang.Database.Models;

namespace Bang.Core.CommandsHandlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Game>
    {
        private readonly BangDbContext context;

        private readonly List<PlayerRole> roles = new()
        {
            PlayerRole.Sheriff,
            PlayerRole.Renegade,
            PlayerRole.Outlaw,
            PlayerRole.Outlaw
        };

        public CreateGameCommandHandler(BangDbContext context)
        {
            this.context = context;
        }

        public async Task<Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            if (request.PlayerNames.Count() < 4 || request.PlayerNames.Count() > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(request.PlayerNames), "Le nombre de joueurs doit être compris entre 4 et 7");
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
                GameStatus = GameStatus.WaitingForPlayers,
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

                if (player.Role == PlayerRole.Sheriff)
                {
                    player.IsScheriff = true;
                    player.Lives++;
                }

                roles.RemoveAt(roleIndex);

                game.Players.Add(player);
            }

            await context.Games.AddAsync(game, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return game;
        }
    }
}
