using Bang.Models;

namespace Bang.Core.Events
{
    public class BrownCardPlay : CardEvent
    {
        public BrownCardPlay(Guid playerId, Card card, Guid? opponentId)
            : base(playerId, card)
        {
            this.OpponentId = opponentId;
        }

        public Guid? OpponentId { get; }
    }
}
