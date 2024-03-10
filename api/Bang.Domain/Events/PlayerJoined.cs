using Bang.Domain.Entities;
using MediatR;

namespace Bang.Domain.Events
{
    public sealed class PlayerJoined : INotification
    {
        public PlayerJoined(CurrentGame game, Player player)
        {
            this.Game = game;
            this.Player = player;
        }

        public CurrentGame Game { get; }
        public Player Player { get; }
    }
}