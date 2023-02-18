using Bang.Models;
using MediatR;

namespace Bang.Core.Queries
{
    public class PlayerIdQuery : IRequest<Guid>
    {
        public PlayerIdQuery(Guid gameId, string playerName)
        {
            this.GameId = gameId;
            this.PlayerName = playerName;
        }

        public Guid GameId { get; }
        public string PlayerName { get; }
    }
}
