using MediatR;

namespace Bang.Core.Notifications
{
    public class PlayerJoin : INotification
    {
        public PlayerJoin(Guid gameId, string playerName)
        {
            this.GameId = gameId;
            this.PlayerName = playerName;
        }

        public Guid GameId { get; }
        public string PlayerName { get; }
    }
}
