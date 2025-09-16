using Bang.Models;
using MediatR;

namespace Bang.Core.Events
{
    public sealed class CardDiscarded : INotification
    {
        public CardDiscarded(Guid gameId, Guid playerId, Card card)
        {
            this.GameId = gameId;
            this.PlayerId = playerId;
            this.Card = card;
        }

        public Guid GameId { get; }
        public Guid PlayerId { get; }
        public Card Card { get; }
    }
}
