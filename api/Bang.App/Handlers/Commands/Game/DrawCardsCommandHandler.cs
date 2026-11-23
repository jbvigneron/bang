using Bang.App.Commands.Game;
using Bang.App.Events;
using Bang.App.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Commands.Game
{
    public class DrawCardsCommandHandler : IRequestHandler<DrawCardsCommand>
    {
        private readonly ILogger<DrawCardsCommandHandler> logger;
        private readonly IMediator mediator;

        private readonly IGameRepository gameRepository;
        private readonly IPlayerRepository playerRepository;

        public DrawCardsCommandHandler(
            ILogger<DrawCardsCommandHandler> logger,
            IMediator mediator,
            IGameRepository gameRepository,
            IPlayerRepository playerRepository)
        {
            this.logger = logger;
            this.mediator = mediator;

            this.gameRepository = gameRepository;
            this.playerRepository = playerRepository;
        }

        public async Task Handle(DrawCardsCommand request, CancellationToken cancellationToken)
        {
            var player = this.playerRepository.Get(request.PlayerId);

            player.EnsureHasNotDrawnCards();

            var game = this.gameRepository.Get(request.GameId);

            game.EnsureInProgress();
            game.EnsureTurnOf(player);

            var cards = this.playerRepository.DrawCards(player);

            await this.mediator.Publish(
                new CardsDrawn(game, player, cards), cancellationToken
            );

            this.logger.LogInformation("Player {PlayerName} draw cards: {@Cards}", player, cards);
        }
    }
}