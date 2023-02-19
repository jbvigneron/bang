using Bang.Core.Queries;
using Bang.Database;
using Bang.Models;
using MediatR;

namespace Bang.Core.QueriesHandlers
{
    public class CharactersQueryHandler : RequestHandler<CharactersQuery, IEnumerable<Character>>
    {
        private readonly BangDbContext dbContext;

        public CharactersQueryHandler(BangDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected override IEnumerable<Character> Handle(CharactersQuery request)
        {
            return this.dbContext.Characters;
        }
    }
}
