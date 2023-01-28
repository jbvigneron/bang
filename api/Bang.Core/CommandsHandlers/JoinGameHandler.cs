using Bang.Core.Commands;
using Bang.Core.Events;
using Bang.Core.Notifications;
using Bang.Core.Queries;
using MediatR;

namespace Bang.Core.CommandsHandlers
{
    public class JoinGameHandler : IRequestHandler<JoinGameCommand, Guid>
    {
        private readonly IMediator mediator;

        public JoinGameHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Guid> Handle(JoinGameCommand request, CancellationToken cancellationToken)
        {
            await this.mediator.Publish(
                new PlayerJoin(request.GameId, request.PlayerName), cancellationToken
            );

            var playerId = await this.mediator.Send(
                new PlayerIdQuery(request.GameId, request.PlayerName), cancellationToken
            );

            await this.mediator.Publish(
                new PlayerPrepareDeck(playerId), cancellationToken
            );

            return playerId;
        }
    }
}
