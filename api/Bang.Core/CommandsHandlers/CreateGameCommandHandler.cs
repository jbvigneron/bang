using Bang.Core.Commands;
using Bang.Core.Exceptions;
using Bang.Database;
using Bang.Database.Enums;
using Bang.Database.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.CommandsHandlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Game>
    {
        private readonly BangDbContext context;

        private readonly List<Role> roles = new()
        {
            Role.Sheriff,
            Role.Renegade,
            Role.Outlaw,
            Role.Outlaw
        };

        public CreateGameCommandHandler(BangDbContext context)
        {
            this.context = context;
        }

        public async Task<Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            if (request.PlayerNames.Distinct().Count() != request.PlayerNames.Count())
            {
                throw new GameException("Les joueurs doivent avoir des noms différents");
            }

            this.DetermineAvailablesRoles(request.PlayerNames);

            var deck = await this.InitializeDeckAsync(cancellationToken);

            var game = new Game
            {
                GameStatus = GameStatus.WaitingForPlayers,
                Players = new List<Player>(),
                Deck = deck,
                DeckCount = deck.Count,
                DiscardPile = new List<GameDiscardCard>()
            };

            this.AssignRolesToAllPlayers(request.PlayerNames, game);

            await context.Games.AddAsync(game, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return game;
        }

        private void DetermineAvailablesRoles(IEnumerable<string> playerNames)
        {
            if (playerNames.Count() < 4 || playerNames.Count() > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(playerNames), "Le nombre de joueurs doit être compris entre 4 et 7");
            }

            if (playerNames.Count() >= 5)
            {
                this.roles.Add(Role.Assistant);
            }

            if (playerNames.Count() >= 6)
            {
                this.roles.Add(Role.Outlaw);
            }

            if (playerNames.Count() == 7)
            {
                this.roles.Add(Role.Assistant);
            }
        }

        private Task<List<GameDeckCard>> InitializeDeckAsync(CancellationToken cancellationToken) =>
            this.context.Cards
                .OrderBy(c => Guid.NewGuid())
                .Select(c => new GameDeckCard
                {
                    CardId = c.Id
                })
                .ToListAsync(cancellationToken);

        private void AssignRolesToAllPlayers(IEnumerable<string> playerNames, Game game)
        {
            foreach (var playerName in playerNames)
            {
                var player = new Player
                {
                    Name = playerName,
                    Status = PlayerStatus.NotReady
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

            if (player.Role.Value == Role.Sheriff)
            {
                player.IsScheriff = true;
                player.Lives++;
            }

            this.roles.RemoveAt(roleIndex);
        }
    }
}
