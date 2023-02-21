using Bang.Core.Events;
using MediatR;

namespace Bang.Core.Commands.Handlers
{
    public class CreateGameHandler : IRequestHandler<CreateGameCommand, Guid>
    {
        private readonly IMediator mediator;

        public CreateGameHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var gameId = Guid.NewGuid();

            await mediator.Publish(
                new NewGame(gameId, request.PlayerNames), cancellationToken
            );

            await mediator.Publish(
                new GameDeckPrepare(gameId), cancellationToken
            );

            return gameId;
        }
    }
}
