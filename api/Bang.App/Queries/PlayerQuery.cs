using Bang.Domain.Entities;
using MediatR;
using System;

namespace Bang.App.Queries
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