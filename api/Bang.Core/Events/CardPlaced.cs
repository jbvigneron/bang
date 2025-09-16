using Bang.Models;
using MediatR;
using System;

namespace Bang.Core.Events
{
    public class CardPlaced : INotification
    {
        public CardPlaced(Guid gameId, Guid playerId, Guid targetPlayerId, Card card)
        {
            this.GameId = gameId;
            this.PlayerId = playerId;
            this.TargetPlayerId = targetPlayerId;
            this.Card = card;
        }

        public Guid GameId { get; }
        public Guid PlayerId { get; }
        public Guid TargetPlayerId { get; }
        public Card Card { get; }
    }
}
