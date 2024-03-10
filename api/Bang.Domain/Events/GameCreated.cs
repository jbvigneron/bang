using Bang.Domain.Entities;
using MediatR;

namespace Bang.Domain.Events
{
    public sealed class GameCreated : INotification
    {
        public GameCreated(CurrentGame game)
        {
            this.Game = game;
        }

        public CurrentGame Game { get; }
    }
}