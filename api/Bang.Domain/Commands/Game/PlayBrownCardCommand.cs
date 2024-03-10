using Bang.Domain.Entities;
using MediatR;
using System;
using System.Security.Claims;

namespace Bang.Domain.Commands.Game
{
    public sealed class PlayBrownCardCommand : IRequest
    {
        public PlayBrownCardCommand(ClaimsPrincipal user, Card card, Guid? opponentId)
        {
            this.User = user;
            this.Card = card;
            this.OpponentId = opponentId;
        }

        public ClaimsPrincipal User { get; }
        public Card Card { get; }
        public Guid? OpponentId { get; }
    }
}