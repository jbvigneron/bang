using Bang.Core.Extensions;
using MediatR;
using System.Security.Claims;

namespace Bang.Core.Commands
{
    public class DrawCardsCommand : IRequest
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
