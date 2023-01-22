using MediatR;

namespace Bang.Core.Events
{
    public class NewGame : INotification
    {
        public NewGame(Guid gameId, IEnumerable<string> playerNames)
        {
            this.GameId = gameId;
            this.PlayerNames = playerNames;
        }

        public Guid GameId { get; }
        public IEnumerable<string> PlayerNames { get; }
    }
}
