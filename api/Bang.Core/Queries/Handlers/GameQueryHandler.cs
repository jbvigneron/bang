using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Queries.Handlers
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
                .Include(g => g.Players)
                    .ThenInclude(p => p.Weapon)
                .Include(g => g.Players)
                    .ThenInclude(p => p.Role)
                .Include(g => g.Players)
                    .ThenInclude(p => p.CardsInGame)
                .FirstAsync(g => g.Id == request.GameId, cancellationToken);
        }
    }
}
