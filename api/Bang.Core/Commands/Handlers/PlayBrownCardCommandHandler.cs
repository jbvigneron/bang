using Bang.Core.Events;
using Bang.Core.Extensions;
using Bang.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bang.Core.Commands.Handlers
{
    public class PlayBrownCardCommandHandler : IRequestHandler<PlayBrownCardCommand>
    {
        private readonly BangDbContext dbContext;
        private readonly IMediator mediator;
        private readonly ILogger<PlayBrownCardCommandHandler> logger;

        public PlayBrownCardCommandHandler(BangDbContext dbContext, IMediator mediator, ILogger<PlayBrownCardCommandHandler> logger)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Handle(PlayBrownCardCommand request, CancellationToken cancellationToken)
        {
            var playerId = request.User.GetId();
            var gameId = request.User.GetGameId();
            var card = request.Card;

            var hand = this.dbContext.PlayersHands
                .Include(d => d.Cards)
                .Include(d => d.Player)
                .Single(p => p.PlayerId == playerId);

            var discardPile = this.dbContext.DiscardPiles
                .Include(d => d.Cards)
                .Single(g => g.GameId == gameId);

            hand.Cards!.Remove(card);
            discardPile.Cards!.Add(card);

            this.dbContext.SaveChanges();

            if (request.OpponentId.HasValue)
            {
                var opponent = this.dbContext.Players.Single(p => p.Id == request.OpponentId);
                this.logger.LogInformation("{@Player} plays brown card {CardName} to {@Opponent}", hand.Player, card.Name, opponent);
            }
            else
            {
                this.logger.LogInformation("{@Player} plays brown card {CardName}", hand.Player, card.Name);
            }

            await this.mediator.Publish(
                new CardDiscarded(gameId, playerId, card)
            );
        }
    }
}
