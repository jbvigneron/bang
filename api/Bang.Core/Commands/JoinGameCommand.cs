using Bang.Database.Models;
using MediatR;

namespace Bang.Core.Commands
{
    public class JoinGameCommand : IRequest<Player>
    {
        public JoinGameCommand(Guid gameId, string playerName)
        {
            this.GameId = gameId;
            this.PlayerName = playerName;
        }

        public Guid GameId { get; }
        public string PlayerName { get; }
    }
}
