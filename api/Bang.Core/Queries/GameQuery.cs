using Bang.Models;
using MediatR;

namespace Bang.Core.Queries
{
    public class GameQuery : IRequest<Game>
    {
        public GameQuery(Guid gameId)
        {
            this.GameId = gameId;
        }

        public Guid GameId { get; }
    }
}
