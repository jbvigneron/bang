using Bang.App.Repositories;
using Bang.Domain.Commands.Game;
using Bang.Domain.Entities;
using Bang.Domain.Enums;
using Bang.Domain.Events;
using Bang.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Commands.Game
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
    {
        private readonly IMediator mediator;
        private readonly ILogger<CreateGameCommandHandler> logger;

        private readonly IGameRepository gameRepository;
        private readonly IRoleRepository roleRepository;

        public CreateGameCommandHandler(
            IMediator mediator,
            ILogger<CreateGameCommandHandler> logger,
            IGameRepository gameRepository,
            IRoleRepository roleRepository)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.gameRepository = gameRepository;
            this.roleRepository = roleRepository;
        }

        public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            if (request.PlayerNames.Distinct().Count() != request.PlayerNames.Count())
            {
                throw new GameException("Les joueurs doivent avoir des noms différents");
            }

            var players = this.AssignRoles(request.PlayerNames);
            var firstPlayer = this.GetFirstPlayer(players);

            var game = this.gameRepository.Create(players, firstPlayer);

            await this.mediator.Publish(
                new GameCreated(game), cancellationToken
            );

            this.logger.LogInformation("Created game {@Game}", game);

            return game.Id;
        }

        private IEnumerable<Player> AssignRoles(IEnumerable<string> playerNames)
        {
            var roles = GetAvailablesRoles(playerNames.Count());

            return playerNames.Select(playerName =>
            {
                var role = this.PickRole(roles);

                var player = new Player
                {
                    Name = playerName,
                    Status = PlayerStatus.NotReady,
                    Role = role
                };

                if (role.Id == RoleKind.Sheriff)
                {
                    player.IsSheriff = true;
                }

                return player;
            }).ToArray();
        }

        private IList<RoleKind> GetAvailablesRoles(int playersCount)
        {
            if (playersCount < 4 || playersCount > 7)
                throw new ArgumentOutOfRangeException(nameof(playersCount), "Le nombre de joueurs doit être compris entre 4 et 7");

            var availableRoles = new List<RoleKind> {
                RoleKind.Sheriff,
                RoleKind.Renegade,
                RoleKind.Outlaw,
                RoleKind.Outlaw
            };

            if (playersCount >= 5)
                availableRoles.Add(RoleKind.DeputySheriff);

            if (playersCount >= 6)
                availableRoles.Add(RoleKind.Outlaw);

            if (playersCount == 7)
                availableRoles.Add(RoleKind.DeputySheriff);

            return availableRoles;
        }

        private Role PickRole(IList<RoleKind> roles)
        {
            var index = new Random().Next(roles.Count);
            var roleId = roles[index];

            roles.RemoveAt(index);

            return this.roleRepository.Get(roleId);
        }

        private Player GetFirstPlayer(IEnumerable<Player> players)
            => players.Single(p => p.IsSheriff);
    }
}