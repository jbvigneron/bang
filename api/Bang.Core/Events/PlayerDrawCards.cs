using MediatR;

namespace Bang.Core.Events
{
    public class PlayerDrawCards : INotification
    {
        public PlayerDrawCards(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}
