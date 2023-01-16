using Bang.Core.Queries;
using Bang.Database;
using Bang.Database.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.QueriesHandlers
{
    public class PlayerQueryHandler : IRequestHandler<PlayerQuery, Player>
    {
        private readonly BangDbContext context;

        public PlayerQueryHandler(BangDbContext context)
        {
            this.context = context;
        }

        public Task<Player> Handle(PlayerQuery request, CancellationToken cancellationToken)
        {
            return this.context.Players
                .Include(p => p.Character)
                .Include(p => p.Role)
                .Include(p => p.Weapon)
                .FirstAsync(g => g.Id == request.PlayerId, cancellationToken);
        }
    }
}
