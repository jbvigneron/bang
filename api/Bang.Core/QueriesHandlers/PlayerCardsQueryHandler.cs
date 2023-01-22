using Bang.Core.Queries;
using Bang.Database;
using Bang.Database.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.QueriesHandlers
{
    public class PlayerCardsQueryHandler : IRequestHandler<PlayerCardsQuery, IList<Card>>
    {
        private readonly BangDbContext context;

        public PlayerCardsQueryHandler(BangDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<Card>> Handle(PlayerCardsQuery request, CancellationToken cancellationToken)
        {
            var deck = await this.context.PlayerDecks
                .Include(p => p.Cards)
                .FirstAsync(g => g.PlayerId == request.PlayerId, cancellationToken);

            return deck.Cards.ToList();
        }
    }
}
