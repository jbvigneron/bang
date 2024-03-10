using Bang.App.Repositories;
using Bang.Domain.Commands.Game;
using Bang.Domain.Events;
using Bang.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Commands.Game
{
    public class PlayBrownCardCommandHandler : IRequestHandler<PlayBrownCardCommand>
    {
        private readonly ILogger<PlayBrownCardCommandHandler> logger;
        private readonly IMediator mediator;

        private readonly IPlayerRepository playerRepository;

        public PlayBrownCardCommandHandler(
            ILogger<PlayBrownCardCommandHandler> logger,
            IMediator mediator,
            IPlayerRepository playerRepository)
        {
            this.logger = logger;
            this.mediator = mediator;

            this.playerRepository = playerRepository;
        }

        public async Task Handle(PlayBrownCardCommand request, CancellationToken cancellationToken)
        {
            var playerId = request.User.GetId();
            var gameId = request.User.GetGameId();
            var card = request.Card;

            var playerHand = this.playerRepository.GetHand(playerId);
            this.playerRepository.PlayBlueCard(playerHand, card);

            this.playerRepository.PlayBrownCard(playerHand, card);

            if (request.OpponentId.HasValue)
            {
                var opponent = this.playerRepository.Get(request.OpponentId.Value);
                this.logger.LogInformation("Player {PlayerName} plays brown card {CardName} to {@Opponent}", playerHand.Player.Name, card.Name, opponent);
            }
            else
            {
                this.logger.LogInformation("Player {PlayerName} plays brown card {CardName}", playerHand.Player.Name, card.Name);
            }

            await this.mediator.Publish(
                new CardDiscarded(gameId, playerId, card)
            );
        }
    }
}