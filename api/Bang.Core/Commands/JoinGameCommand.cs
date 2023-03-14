using MediatR;

namespace Bang.Core.Commands
{
    public sealed class JoinGameCommand : IRequest<Guid>
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
