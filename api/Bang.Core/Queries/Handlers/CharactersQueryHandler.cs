using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Queries.Handlers
{
    public class CharactersQueryHandler : IRequestHandler<CharactersQuery, Character[]>
    {
        private readonly BangDbContext dbContext;

        public CharactersQueryHandler(BangDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Character[]> Handle(CharactersQuery request, CancellationToken cancellationToken)
        {
            return this.dbContext.Characters.ToArrayAsync(cancellationToken);
        }
    }
}
