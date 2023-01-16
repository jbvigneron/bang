using Bang.Database.Models;
using MediatR;

namespace Bang.Core.Queries
{
    public class GameFromPlayerIdQuery : IRequest<Game>
    {
        public GameFromPlayerIdQuery(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}
