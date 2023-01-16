using Bang.Database.Models;
using MediatR;

namespace Bang.Core.Queries
{
    public class PlayerQuery : IRequest<Player>
    {
        public PlayerQuery(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}
