using Bang.App.Repositories;
using Bang.Domain.Commands.Game;
using Bang.Domain.Enums;
using Bang.Domain.Events;
using Bang.Domain.Exceptions;
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

            if (player.HasDrawnCards)
            {
                throw new PlayerException("Vous avez déjà pioché vos cartes.", player);
            }

            var game = this.gameRepository.Get(request.GameId);

            if (game.Status == GameStatus.WaitingForPlayers)
            {
                throw new GameException("La partie n'a pas encore démarré.", game);
            }

            if (game.Status == GameStatus.Finished)
            {
                throw new GameException("La partie est terminée.", game);
            }

            if (game.CurrentPlayerName != player.Name)
            {
                throw new PlayerException("Ce n'est pas à vous de jouer.", player);
            }

            var cards = this.playerRepository.DrawCards(player);

            await this.mediator.Publish(
                new CardsDrawn(game, player, cards), cancellationToken
            );

            this.logger.LogInformation("Player {PlayerName} draw cards: {@Cards}", player, cards);
        }
    }
}