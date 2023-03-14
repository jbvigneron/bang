using Bang.Models;
using MediatR;

namespace Bang.Core.Notifications
{
    public sealed class PlayerJoined : INotification
    {
        public PlayerJoined(Game game, Player player)
        {
            this.Game = game;
            this.Player = player;
        }

        public Game Game { get; }
        public Player Player { get; }
    }
}
