using Bang.Models;
using MediatR;
using System;

namespace Bang.Core.Queries
{
    public class PlayerQuery : IRequest<Player>
    {
        public PlayerQuery(Guid playerId)
        {
            this.PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}