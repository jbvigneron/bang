using Bang.Database.Models;
using MediatR;

namespace Bang.Core.Events
{
    public class GameReadyEvent : INotification
    {
        public GameReadyEvent(Game game)
        {
            this.Game = game;
        }

        public Game Game { get; }
    }
}
