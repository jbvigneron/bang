using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Queries.Handlers
{
    public class CardQueryHandler : IRequestHandler<CardQuery, Card>
    {
        private readonly BangDbContext dbContext;

        public CardQueryHandler(BangDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Card> Handle(CardQuery request, CancellationToken cancellationToken)
        {
            return dbContext.Cards.SingleAsync(c => c.Id == request.CardId, cancellationToken);
        }
    }
}
