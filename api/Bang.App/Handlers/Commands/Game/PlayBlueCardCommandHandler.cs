using Bang.App.Repositories;
using Bang.Domain.Commands.Game;
using Bang.Domain.Events;
using Bang.Domain.Exceptions;
using Bang.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Commands.Game
{
    public class PlayBlueCardCommandHandler : IRequestHandler<PlayBlueCardCommand>
    {
        private readonly ILogger<PlayBlueCardCommand> logger;
        private readonly IMediator mediator;

        private readonly IPlayerRepository playerRepository;

        public PlayBlueCardCommandHandler(
            ILogger<PlayBlueCardCommand> logger,
            IMediator mediator,
            IPlayerRepository playerRepository)
        {
            this.logger = logger;
            this.mediator = mediator;

            this.playerRepository = playerRepository;
        }

        public async Task Handle(PlayBlueCardCommand request, CancellationToken cancellationToken)
        {
            var playerId = request.User.GetId();
            var gameId = request.User.GetGameId();
            var card = request.Card;

            var playerHand = this.playerRepository.GetHand(playerId);

            if (card.RequireOpponent && !request.OpponentId.HasValue)
            {
                throw new GameException("Veuillez cibler un adversaire pour cette carte.");
            }

            this.playerRepository.PlayBlueCard(playerHand, card);

            if (card.RequireOpponent)
            {
                var opponent = this.playerRepository.Get(request.OpponentId.Value);
                this.logger.LogInformation("Player {PlayerName} plays brown card {CardName} to {OpponentName}", playerHand.Player.Name, card.Name, opponent.Name);

                await this.mediator.Publish(
                    new CardPlaced(gameId, playerId, opponent.Id, card)
                );
            }
            else
            {
                this.logger.LogInformation("Player {PlayerName} plays brown card {CardName}", playerHand.Player.Name, card.Name);

                await this.mediator.Publish(
                    new CardPlaced(gameId, playerId, playerId, card)
                );
            }
        }
    }
}