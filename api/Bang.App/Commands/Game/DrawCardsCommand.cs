using Bang.App.Extensions;
using MediatR;
using System;
using System.Security.Claims;

namespace Bang.App.Commands.Game
{
    public sealed class DrawCardsCommand : IRequest
    {
        public DrawCardsCommand(ClaimsPrincipal user)
        {
            this.PlayerId = user.GetId();
            this.PlayerName = user.GetName();
            this.GameId = user.GetGameId();
        }

        public Guid PlayerId { get; }
        public string PlayerName { get; }
        public Guid GameId { get; }
    }
}