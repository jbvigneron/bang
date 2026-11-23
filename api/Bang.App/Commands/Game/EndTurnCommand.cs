using Bang.App.Extensions;
using MediatR;
using Bang.App.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Bang.App.Commands.Game
{
    public sealed class EndTurnCommand : IRequest
    {
        public EndTurnCommand(ClaimsPrincipal user, IEnumerable<Guid> cardIds)
        {
            this.PlayerId = user.GetId();
            this.GameId = user.GetGameId();
            this.CardIds = cardIds;
        }

        public Guid PlayerId { get; }
        public Guid GameId { get; }
        public IEnumerable<Guid> CardIds { get; }
    }
}
