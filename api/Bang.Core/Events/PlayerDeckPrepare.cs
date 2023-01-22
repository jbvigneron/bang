using MediatR;

namespace Bang.Core.Events
{
    public class PlayerDeckPrepare : INotification
    {
        public PlayerDeckPrepare(Guid playerId)
        {
            this.PlayerId = playerId;
        }

        public Guid PlayerId { get; set; }
    }
}
