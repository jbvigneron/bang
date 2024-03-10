using Bang.Domain.Entities;
using MediatR;
using System.Security.Claims;

namespace Bang.Domain.Commands.Game
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