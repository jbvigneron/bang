using Bang.Models;
using MediatR;

namespace Bang.Core.Events
{
    public class SwitchCard : INotification
    {
        public SwitchCard(Guid playerId, Card oldCard, string newCardName)
        {
            this.PlayerId = playerId;
            this.OldCard = oldCard;
            this.NewCardName = newCardName;
        }

        public Guid PlayerId { get; }
        public Card OldCard { get; }
        public string NewCardName { get; }
    }
}
