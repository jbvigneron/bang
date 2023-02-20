using Bang.Models;
using MediatR;

namespace Bang.Core.Events
{
    public class ChangeWeapon : INotification
    {
        public ChangeWeapon(Guid playerId, Card card)
        {
            this.PlayerId = playerId;
            this.Card = card;
        }

        public Guid PlayerId { get; }
        public Card Card { get; }
    }
}
