using Bang.App.Commands.Game;
using Bang.App.Events;
using Bang.App.Repositories;
using Bang.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Commands.Game
{
    public class EndTurnCommandHandler : IRequestHandler<EndTurnCommand>
    {
        private readonly ILogger<EndTurnCommandHandler> logger;

        private readonly IGameRepository gameRepository;
        private readonly IPlayerRepository playerRepository;
        private readonly IMediator mediator;

        public EndTurnCommandHandler(
            ILogger<EndTurnCommandHandler> logger,
            IGameRepository gameRepository,
            IPlayerRepository playerRepository,
            IMediator mediator)
        {
            this.logger = logger;

            this.gameRepository = gameRepository;
            this.playerRepository = playerRepository;
            this.mediator = mediator;
        }

        public async Task Handle(EndTurnCommand request, CancellationToken cancellationToken)
        {
            var player = this.playerRepository.Get(request.PlayerId);
            var game = this.gameRepository.Get(request.GameId);

            game.EnsureInProgress();
            game.EnsureTurnOf(player);
            player.EnsureHasDrawnCards();

            var hand = this.playerRepository.GetHand(player.Id);
            var requiredDiscards = Math.Max(0, hand.Cards.Count - player.Lives);
            var cardIds = (request.CardIds ?? Enumerable.Empty<Guid>()).Distinct().ToList();

            if (cardIds.Count != requiredDiscards)
            {
                throw new PlayerException(
                    $"Vous devez défausser exactement {requiredDiscards} carte(s) pour terminer votre tour.",
                    player);
            }

            var cardsToDiscard = hand.Cards.Where(c => cardIds.Contains(c.Id)).ToList();

            if (cardsToDiscard.Count != requiredDiscards)
            {
                throw new PlayerException("Les cartes à défausser doivent être dans votre main.", player);
            }

            this.playerRepository.DiscardCards(hand, cardsToDiscard);

            foreach (var card in cardsToDiscard)
            {
                await this.mediator.Publish(new CardDiscarded(game.Id, player.Id, card), cancellationToken);
            }

            var nextPlayer = game.GetNextPlayer(player);

            player.HasDrawnCards = false;
            game.CurrentPlayerName = nextPlayer.Name;

            this.playerRepository.Update(player);
            this.gameRepository.Update(game);

            this.logger.LogInformation("Player {PlayerName} ends turn, next player is {NextPlayerName}", player.Name, nextPlayer.Name);

            await this.mediator.Publish(new TurnChanged(game.Id, nextPlayer.Id, nextPlayer.Name), cancellationToken);
        }
    }
}

