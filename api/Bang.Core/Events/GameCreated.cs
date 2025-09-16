using Bang.Models;
using MediatR;

namespace Bang.Core.Events
{
    public sealed class GameCreated : INotification
    {
        public GameCreated(Game game)
        {
            this.Game = game;
        }

        public Game Game { get; }
    }
}
