using Bang.Models;
using MediatR;

namespace Bang.Core.Queries
{
    public class CardQuery : IRequest<Card>
    {
        public CardQuery(Guid cardId)
        {
            this.CardId = cardId;
        }

        public Guid CardId { get; }
    }
}
