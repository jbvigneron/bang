using Bang.Core.Events;
using Bang.Core.Notifications;
using Bang.Core.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bang.Core.Commands.Handlers
{
    public class JoinGameHandler : IRequestHandler<JoinGameCommand, Guid>
    {
        private readonly IMediator mediator;
        private readonly ILogger<JoinGameHandler> logger;

        public JoinGameHandler(IMediator mediator, ILogger<JoinGameHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task<Guid> Handle(JoinGameCommand request, CancellationToken cancellationToken)
        {
            var gameId = request.GameId;
            var playerName = request.PlayerName;

            this.logger.LogInformation("{PlayerName} wants to join game {GameId}", gameId);

            await this.mediator.Publish(
                new PlayerJoin(gameId, playerName), cancellationToken
            );

            var playerId = await this.mediator.Send(
                new PlayerIdQuery(gameId, playerName), cancellationToken
            );

            this.logger.LogInformation("{PlayerName} ({PlayerId}) has joined game {GameId}", playerName, playerId, gameId);

            await this.mediator.Publish(
                new PlayerHandSetup(gameId, playerId), cancellationToken
            );

            this.logger.LogInformation("{PlayerName} ({PlayerId}) hand is ready", playerName, playerId);

            return playerId;
        }
    }
}
