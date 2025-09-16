using Bang.Models;
using MediatR;
using System.Security.Claims;

namespace Bang.Core.Commands
{
    public sealed class PlayBlueCardCommand : IRequest
    {
        public PlayBlueCardCommand(ClaimsPrincipal user, Card card, Guid? opponentId)
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
