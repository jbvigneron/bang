using Bang.Models;

namespace Bang.Core.Events
{
    public class WeaponCardPlay : CardEvent
    {
        public WeaponCardPlay(Guid playerId, Card card)
            : base(playerId, card) { }
    }
}
