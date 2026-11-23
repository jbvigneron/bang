using Bang.Domain.Entities;
using System;

namespace Bang.Domain.Exceptions
{
    public class GameException : Exception
    {
        public GameException(string message) : base(message)
        {
        }

        public GameException(string message, CurrentGame game) : this(message)
        {
            this.Game = game;
        }

        public GameException(string message, Guid gameId) : this(message)
        {
            this.GameId = gameId;
        }

        public CurrentGame Game { get; }
        public Guid? GameId { get; }
    }
}