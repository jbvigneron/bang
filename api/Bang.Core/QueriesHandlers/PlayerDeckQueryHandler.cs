using Bang.Core.Queries;
using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.QueriesHandlers
{
    public class PlayerDeckQueryHandler : IRequestHandler<PlayerDeckQuery, IList<Card>>
    {
        private readonly BangDbContext dbContext;

        public PlayerDeckQueryHandler(BangDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<Card>> Handle(PlayerDeckQuery request, CancellationToken cancellationToken)
        {
            var deck = await this.dbContext.PlayersHands
                .Include(p => p.Cards)
                .FirstAsync(g => g.PlayerId == request.PlayerId, cancellationToken);

            return deck.Cards.ToList();
        }
    }
}
