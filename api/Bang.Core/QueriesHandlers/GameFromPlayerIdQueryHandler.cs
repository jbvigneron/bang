using Bang.Core.Queries;
using Bang.Database;
using Bang.Database.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.QueriesHandlers
{
    public class GameFromPlayerIdQueryHandler : IRequestHandler<GameFromPlayerIdQuery, Game>
    {
        private readonly BangDbContext context;

        public GameFromPlayerIdQueryHandler(BangDbContext context)
        {
            this.context = context;
        }

        public Task<Game> Handle(GameFromPlayerIdQuery request, CancellationToken cancellationToken)
        {
            return this.context.Games
                .Include(g => g.Players)
                .FirstAsync(g => g.Players.Any(p => p.Id == request.PlayerId), cancellationToken);
        }
    }
}
