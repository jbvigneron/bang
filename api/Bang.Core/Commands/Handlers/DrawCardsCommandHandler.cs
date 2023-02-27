using Bang.Core.Events;
using Bang.Core.Exceptions;
using Bang.Core.Queries;
using Bang.Models.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bang.Core.Commands.Handlers
{
    public class DrawCardsCommandHandler : IRequestHandler<DrawCardsCommand>
    {
        private readonly IMediator mediator;
        private readonly ILogger<DrawCardsCommandHandler> logger;

        public DrawCardsCommandHandler(IMediator mediator, ILogger<DrawCardsCommandHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Handle(DrawCardsCommand request, CancellationToken cancellationToken)
        {
            var gameId = request.GameId;
            var playerId = request.PlayerId;
            var playerName = request.PlayerName;

            var game = await this.mediator.Send(new GameQuery(gameId), cancellationToken);
            var player = game.Players.First(p => p.Id == playerId);

            if (player.HasDrawnCards)
            {
                throw new PlayerException("Vous avez déjà pioché vos cartes.", player);
            }

            if (game.Status == GameStatus.WaitingForPlayers)
            {
                throw new GameException("La partie n'a pas encore démarré.", game);
            }

            if (game.Status == GameStatus.Finished)
            {
                throw new GameException("La partie est terminée.", game);
            }

            if (game.CurrentPlayerName != playerName)
            {
                throw new PlayerException("Ce n'est pas à vous de jouer.", player);
            }

            await this.mediator.Publish(
                new PlayerDrawCards(gameId, playerId, playerName), cancellationToken
            );

            this.logger.LogInformation("Player {PlayerName} ({PlayerId}) draw cards", playerName, playerId);
        }
    }
}
