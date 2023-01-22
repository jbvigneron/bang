using Bang.Database.Models;
using MediatR;

namespace Bang.Core.Queries
{
    public class PlayerGameQuery : IRequest<Game>
    {
        public PlayerGameQuery(Guid playerId)
        {
            this.PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}
