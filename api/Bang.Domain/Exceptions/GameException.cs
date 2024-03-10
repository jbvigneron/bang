using Bang.Domain.Entities;
using System;
using System.Runtime.Serialization;

namespace Bang.Domain.Exceptions
{
    [Serializable]
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

        protected GameException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            serializationInfo.AddValue("Game", this.Game);
            serializationInfo.AddValue("GameId", this.GameId);
        }

        public CurrentGame Game { get; }
        public Guid? GameId { get; }
    }
}