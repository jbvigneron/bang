using Bang.Core.Queries;
using Bang.Database;
using Bang.Database.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.QueriesHandlers
{
    public class GameQueryHandler : IRequestHandler<GameQuery, Game>
    {
        private readonly BangDbContext context;

        public GameQueryHandler(BangDbContext context)
        {
            this.context = context;
        }

        public Task<Game> Handle(GameQuery request, CancellationToken cancellationToken)
        {
            return this.context.Games
                .Include(g => g.Players)
                    .ThenInclude(p => p.Character)
                .Include(p => p.Players)
                    .ThenInclude(p => p.Weapon)
                .FirstAsync(g => g.Id == request.GameId, cancellationToken);
        }
    }
}
