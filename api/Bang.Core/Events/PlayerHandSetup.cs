using MediatR;

namespace Bang.Core.Events
{
    public class PlayerHandSetup : INotification
    {
        public PlayerHandSetup(Guid gameId, Guid playerId)
        {
            this.GameId = gameId;
            this.PlayerId = playerId;
        }
        public Guid GameId { get; }
        public Guid PlayerId { get; }
    }
}