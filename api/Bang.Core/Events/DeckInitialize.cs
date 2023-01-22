using MediatR;

namespace Bang.Core.Events
{
    public class DeckInitialize : INotification
    {
        public DeckInitialize(Guid gameId)
        {
            this.GameId = gameId;
        }
        public Guid GameId { get; }
    }
}
