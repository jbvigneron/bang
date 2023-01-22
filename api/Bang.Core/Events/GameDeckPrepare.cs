using MediatR;

namespace Bang.Core.Events
{
    public class GameDeckPrepare : INotification
    {
        public GameDeckPrepare(Guid gameId)
        {
            this.GameId = gameId;
        }
        public Guid GameId { get; }
    }
}
