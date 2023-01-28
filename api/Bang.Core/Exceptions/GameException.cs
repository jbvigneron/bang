using Bang.Database.Models;

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

        public Game? Game { get; }
        public Guid? GameId { get; }
    }
}
