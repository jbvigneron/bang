using Bang.Models;

namespace Bang.Core.Events
{
    public class BlueCardPlay : CardEvent
    {
        public BlueCardPlay(Guid playerId, Card card)
            : base(playerId, card) { }
    }
}
