using MediatR;

namespace Bang.Core.Events
{
    public class CardsDraw : INotification
    {
        public CardsDraw(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}
