using Bang.Domain.Entities;
using MediatR;
using System;

namespace Bang.App.Queries
{
    public class GameQuery : IRequest<CurrentGame>
    {
        public GameQuery(Guid gameId)
        {
            this.GameId = gameId;
        }

        public Guid GameId { get; }
    }
}