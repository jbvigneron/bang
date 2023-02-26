using Bang.Core.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bang.Core.Commands.Handlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
    {
        private readonly IMediator mediator;
        private readonly ILogger<CreateGameCommandHandler> logger;

        public CreateGameCommandHandler(IMediator mediator, ILogger<CreateGameCommandHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var gameId = Guid.NewGuid();
            var playerNames = request.PlayerNames;

            await this.mediator.Publish(
                new GameCreate(gameId, playerNames), cancellationToken
            );

            this.logger.LogInformation("Create a new game {GameId} with players {@PlayerNames}", gameId, playerNames);

            return gameId;
        }
    }
}
