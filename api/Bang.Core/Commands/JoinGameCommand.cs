using MediatR;
using System;

namespace Bang.Core.Commands
{
    public class JoinGameCommand : IRequest<Guid>
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