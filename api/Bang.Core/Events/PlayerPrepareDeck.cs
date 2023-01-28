using MediatR;

namespace Bang.Core.Events
{
    public class PlayerPrepareDeck : INotification
    {
        public PlayerPrepareDeck(Guid playerId)
        {
            this.PlayerId = playerId;
        }

        public Guid PlayerId { get; set; }
    }
}
