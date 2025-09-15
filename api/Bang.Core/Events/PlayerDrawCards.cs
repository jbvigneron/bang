using MediatR;
using System;

namespace Bang.Core.Events
{
    public class PlayerDrawCards : INotification
    {
        public PlayerDrawCards(Guid gameId, Guid playerId, string playerName)
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