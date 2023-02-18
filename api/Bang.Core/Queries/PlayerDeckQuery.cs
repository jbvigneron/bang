using Bang.Models;
using MediatR;

namespace Bang.Core.Queries
{
    public class PlayerDeckQuery : IRequest<IList<Card>>
    {
        public PlayerDeckQuery(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}
