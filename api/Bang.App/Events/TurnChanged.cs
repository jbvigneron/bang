using MediatR;
using System;

namespace Bang.App.Events
{
    public sealed class TurnChanged : INotification
    {
        public TurnChanged(Guid gameId, Guid playerId, string playerName)
        {
            this.GameId = gameId;
            this.PlayerId = playerId;
            this.PlayerName = playerName;
        }

        public Guid GameId { get; }
        public Guid PlayerId { get; }
        public string PlayerName { get; }
    }
}
