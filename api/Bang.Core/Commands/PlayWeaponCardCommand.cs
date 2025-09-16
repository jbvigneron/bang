using Bang.Models;
using MediatR;
using System.Security.Claims;

namespace Bang.Core.Commands
{
    public sealed class PlayWeaponCardCommand : IRequest
    {
        public PlayWeaponCardCommand(ClaimsPrincipal user, Card card)
        {
            this.User = user;
            this.Card = card;
        }

        public ClaimsPrincipal User { get; }
        public Card Card { get; }
    }
}
