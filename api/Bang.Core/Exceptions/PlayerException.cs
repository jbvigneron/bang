using Bang.Models;
using System.Runtime.Serialization;

namespace Bang.Core.Exceptions
{
    [Serializable]
    public class PlayerException : Exception
    {
        public PlayerException(string message, Player player) : base(message)
        {
            this.Player = player;
        }

        protected PlayerException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            serializationInfo.AddValue("Player", this.Player);
        }

        public Player? Player { get; }
    }
}
