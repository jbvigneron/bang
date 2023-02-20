using Bang.Models;
using MediatR;

namespace Bang.Core.Events
{
    public abstract class CardEvent : INotification
    {
        protected CardEvent(Guid playerId, Card card)
        {
            this.PlayerId = playerId;
            this.Card = card;
        }

        public Guid PlayerId { get; }
        public Card Card { get; }

        public void Deconstruct(out Guid playerId, out Guid cardId)
        {
            playerId = PlayerId;
            cardId = Card.Id;
        }
    }
}
