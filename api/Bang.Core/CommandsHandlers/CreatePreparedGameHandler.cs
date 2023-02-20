using Bang.Core.Commands;
using Bang.Core.Events;
using MediatR;

namespace Bang.Core.CommandsHandlers
{
    public class CreatePreparedGameHandler : IRequestHandler<CreatePreparedGameCommand, Guid>
    {
        private readonly IMediator mediator;

        public CreatePreparedGameHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Guid> Handle(CreatePreparedGameCommand request, CancellationToken cancellationToken)
        {
            var gameId = Guid.NewGuid();

            await this.mediator.Publish(
                new NewPreparedGame(gameId, request.Players), cancellationToken
            );

            await this.mediator.Publish(
                new GameDeckPrepare(gameId), cancellationToken
            );

            return gameId;
        }
    }
}
