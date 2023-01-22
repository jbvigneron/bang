using Bang.Core.Queries;
using Bang.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.QueriesHandlers
{
    public class PlayerIdQueryHandler : IRequestHandler<PlayerIdQuery, Guid>
    {
        private readonly BangDbContext dbContext;

        public PlayerIdQueryHandler(BangDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> Handle(PlayerIdQuery request, CancellationToken cancellationToken)
        {
            var game = await this.dbContext.Games
                .Include(g => g.Players)
                .FirstAsync(g => g.Id == request.GameId, cancellationToken);

            var player = game.Players.First(p => p.Name == request.PlayerName);
            return player.Id;
        }
    }
}
