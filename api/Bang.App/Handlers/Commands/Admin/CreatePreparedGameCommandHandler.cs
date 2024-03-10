using Bang.App.Repositories;
using Bang.Domain.Commands.Admin;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Commands.Admin
{
    public class CreatePreparedGameCommandHandler : IRequestHandler<CreatePreparedGameCommand, Guid>
    {
        private readonly ILogger<CreatePreparedGameCommandHandler> logger;

        private readonly IGameRepository gameRepository;

        public CreatePreparedGameCommandHandler(
            ILogger<CreatePreparedGameCommandHandler> logger,
            IGameRepository gameRepository)
        {
            this.logger = logger;

            this.gameRepository = gameRepository;
        }

        public Task<Guid> Handle(CreatePreparedGameCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Creating prepared game with {Players}", request.Players);

            var players = request.Players.Select(p => (p.Name, p.RoleId, p.CharacterId));
            var game = this.gameRepository.Create(players);

            this.logger.LogInformation("Created game {@Game}", game);

            return Task.FromResult(game.Id);
        }
    }
}