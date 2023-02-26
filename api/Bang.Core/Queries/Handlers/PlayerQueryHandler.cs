using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Queries.Handlers
{
    public class PlayerQueryHandler : IRequestHandler<PlayerQuery, Player>
    {
        private readonly BangDbContext dbContext;

        public PlayerQueryHandler(BangDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Player> Handle(PlayerQuery request, CancellationToken cancellationToken)
        {
            return this.dbContext.Players
                .Include(p => p.Character)
                .Include(p => p.Role)
                .Include(p => p.Weapon)
                .FirstAsync(g => g.Id == request.PlayerId, cancellationToken);
        }
    }
}
