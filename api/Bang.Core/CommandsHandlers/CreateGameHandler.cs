using Bang.Core.Commands;
using Bang.Core.Events;
using MediatR;

namespace Bang.Core.CommandsHandlers
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

            await this.mediator.Publish(
                new NewGame(gameId, request.PlayerNames), cancellationToken
            );

            await this.mediator.Publish(
                new DeckInitialize(gameId), cancellationToken
            );

            return gameId;
        }
    }
}
