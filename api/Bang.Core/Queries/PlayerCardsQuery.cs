using Bang.Database.Models;
using MediatR;

namespace Bang.Core.Queries
{
    public class PlayerCardsQuery : IRequest<IList<Card>>
    {
        public PlayerCardsQuery(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}
