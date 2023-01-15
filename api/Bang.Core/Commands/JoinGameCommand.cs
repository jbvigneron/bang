using MediatR;
using Bang.Database.Models;

namespace Bang.Core.Commands
{
    public class JoinGameCommand : IRequest<Player>
    {
        public JoinGameCommand(Guid gameId, string playerName)
        {
            GameId = gameId;
            PlayerName = playerName;
        }

        public Guid GameId { get; }
        public string PlayerName { get; }
    }
}
