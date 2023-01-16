using Bang.Database.Models;
using MediatR;

namespace Bang.Core.Queries
{
    public class GameQuery : IRequest<Game>
    {
        public GameQuery(Guid gameId)
        {
            GameId = gameId;
        }

        public Guid GameId { get; }
    }
}
