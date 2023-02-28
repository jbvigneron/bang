using Bang.Models;
using System.Runtime.Serialization;

namespace Bang.Core.Exceptions
{
    [Serializable]
    public class GameException : Exception
    {
        public GameException(string message, Game game) : base(message)
        {
            this.Game = game;
        }

        public GameException(string message, Guid gameId) : base(message)
        {
            this.GameId = gameId;
        }

        protected GameException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            serializationInfo.AddValue("Game", this.Game);
            serializationInfo.AddValue("GameId", this.GameId);
        }

        public Game? Game { get; }
        public Guid? GameId { get; }
    }
}
