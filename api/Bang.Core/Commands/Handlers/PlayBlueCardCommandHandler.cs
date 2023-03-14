using Bang.Core.Events;
using Bang.Core.Exceptions;
using Bang.Core.Extensions;
using Bang.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bang.Core.Commands.Handlers
{
    public class PlayBlueCardCommandHandler : IRequestHandler<PlayBlueCardCommand>
    {
        private readonly BangDbContext dbContext;
        private readonly IMediator mediator;
        private readonly ILogger<PlayBlueCardCommand> logger;

        public PlayBlueCardCommandHandler(BangDbContext dbContext, IMediator mediator, ILogger<PlayBlueCardCommand> logger)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Handle(PlayBlueCardCommand request, CancellationToken cancellationToken)
        {
            var playerId = request.User.GetId();
            var gameId = request.User.GetGameId();
            var card = request.Card;

            var hand = this.dbContext.PlayersHands
                .Include(d => d.Cards)
                .Include(d => d.Player!)
                    .ThenInclude(p => p.CardsInGame)
                .Single(p => p.PlayerId == playerId);

            if (card.RequireOpponent && !request.OpponentId.HasValue)
            {
                throw new GameException("Veuillez cibler un adversaire pour cette carte.");
            }

            hand.Cards!.Remove(card);
            hand.Player!.CardsInGame!.Add(card);

            this.dbContext.SaveChanges();

            if (card.RequireOpponent)
            {
                var opponent = this.dbContext.Players.Single(p => p.Id == request.OpponentId);
                this.logger.LogInformation("{@Player} plays brown card {CardName} to {@Opponent}", hand.Player, card.Name, opponent);

                await this.mediator.Publish(
                    new CardPlaced(gameId, playerId, opponent.Id, card)
                );
            }
            else
            {
                this.logger.LogInformation("{@Player} plays brown card {CardName}", hand.Player, card.Name);

                await this.mediator.Publish(
                    new CardPlaced(gameId, playerId, playerId, card)
                );
            }
        }
    }
}
