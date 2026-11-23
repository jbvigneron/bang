using Bang.App.Commands.Game;
using Bang.App.Events;
using Bang.App.Extensions;
using Bang.App.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Commands.Game
{
    public class PlayBlueCardCommandHandler : IRequestHandler<PlayBlueCardCommand>
    {
        private readonly ILogger<PlayBlueCardCommandHandler> logger;
        private readonly IMediator mediator;

        private readonly IPlayerRepository playerRepository;

        public PlayBlueCardCommandHandler(
            ILogger<PlayBlueCardCommandHandler> logger,
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

            this.playerRepository.PlayBlueCard(playerHand, card);

            if (card.RequireOpponent)
            {
                var opponentId = request.OpponentId!.Value;
                var opponent = this.playerRepository.Get(opponentId);
                this.logger.LogInformation("Player {PlayerName} plays brown card {CardName} to {OpponentName}", playerHand.Player.Name, card.Name, opponent.Name);

                await this.mediator.Publish(
                    new CardPlaced(gameId, playerId, opponent.Id, card),
                    cancellationToken
                );
            }
            else
            {
                this.logger.LogInformation("Player {PlayerName} plays brown card {CardName}", playerHand.Player.Name, card.Name);

                await this.mediator.Publish(
                    new CardPlaced(gameId, playerId, playerId, card),
                    cancellationToken
                );
            }
        }
    }
}