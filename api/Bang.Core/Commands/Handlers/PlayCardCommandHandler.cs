using Bang.Core.Events;
using Bang.Core.Queries;
using Bang.Models.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bang.Core.Commands.Handlers
{
    public class PlayCardCommandHandler : AsyncRequestHandler<PlayCardCommand>
    {
        private readonly IMediator mediator;
        private readonly ILogger<PlayCardCommandHandler> logger;

        public PlayCardCommandHandler(IMediator mediator, ILogger<PlayCardCommandHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        protected override async Task Handle(PlayCardCommand request, CancellationToken cancellationToken)
        {
            var playerId = request.PlayerId;
            var playerName = request.PlayerName;
            var gameId = request.GameId;
            var cardId = request.CardId;

            var card = await this.mediator.Send(new CardQuery(cardId), cancellationToken);

            switch (card.Type)
            {
                case CardType.Brown:
                    var opponentId = request.OpponentId;
                    this.logger.LogInformation("{PlayerName} ({PlayerId}) play brown card {CardName} (OpponentId: {OpponentId})", playerName, playerId, card.Name, opponentId);

                    await mediator.Publish(
                        new BrownCardPlay(gameId, playerId, cardId, opponentId), cancellationToken
                    );
                    break;
                case CardType.Blue:
                    this.logger.LogInformation("{PlayerName} ({PlayerId}) play blue card {CardName}", playerName, playerId, card.Name);

                    await mediator.Publish(
                        new BlueCardPlay(gameId, playerId, cardId), cancellationToken
                    );
                    break;
                case CardType.Weapon:
                    this.logger.LogInformation("{PlayerName} ({PlayerId}) play weapon card {CardName}", playerName, playerId, card.Name);

                    await mediator.Publish(
                        new WeaponCardPlay(gameId, playerId, cardId), cancellationToken
                    );
                    break;
            }

        }
    }
}
