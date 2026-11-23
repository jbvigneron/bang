using Bang.Domain.Entities;
using MediatR;
using System;

namespace Bang.App.Queries
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