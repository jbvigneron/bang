using Bang.Models;
using System;

namespace Bang.Core.Exceptions
{
    [Serializable]
    public class GameException : Exception
    {
        public GameException(string message) : base(message)
        {
        }

        public GameException(string message, Game game) : this(message)
        {
            this.Game = game;
        }

        public GameException(string message, Guid gameId) : this(message)
        {
            this.GameId = gameId;
        }

        public Game Game { get; }
        public Guid GameId { get; }
    }
}