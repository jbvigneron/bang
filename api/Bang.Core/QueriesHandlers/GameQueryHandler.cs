using Bang.Core.Queries;
using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.QueriesHandlers
{
    public class GameQueryHandler : IRequestHandler<GameQuery, Game>
    {
        private readonly BangDbContext dbContext;

        public GameQueryHandler(BangDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Game> Handle(GameQuery request, CancellationToken cancellationToken)
        {
            return this.dbContext.Games
                .Include(g => g.Players)
                    .ThenInclude(p => p.Character)
                .Include(p => p.Players)
                    .ThenInclude(p => p.Weapon)
                .FirstAsync(g => g.Id == request.GameId, cancellationToken);
        }
    }
}
