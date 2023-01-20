using Bang.Database.Models;
using MediatR;

namespace Bang.Core.Notifications
{
    public class PlayerReadyEvent : INotification
    {
        public PlayerReadyEvent(Game game, Player player)
        {
            this.Game = game;
            this.Player = player;
        }

        public Game Game { get; }
        public Player Player { get; }
    }
}
