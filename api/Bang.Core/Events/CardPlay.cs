using MediatR;

namespace Bang.Core.Events
{
    public abstract class CardPlay : INotification
    {
        protected CardPlay(Guid gameId, Guid playerId, Guid cardId)
        {
            this.GameId = gameId;
            this.PlayerId = playerId;
            this.CardId = cardId;
        }

        public Guid GameId { get; }
        public Guid PlayerId { get; }
        public Guid CardId { get; }
    }
}
