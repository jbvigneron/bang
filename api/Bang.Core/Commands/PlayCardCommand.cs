using MediatR;
using System.Security.Claims;

namespace Bang.Core.Commands
{
    public sealed class PlayCardCommand : IRequest
    {
        public PlayCardCommand(ClaimsPrincipal user, Guid cardId, Guid? opponentId = null)
        {
            this.User = user;
            this.CardId = cardId;
            this.OpponentId = opponentId;
        }

        public ClaimsPrincipal User { get; }
        public Guid CardId { get; }
        public Guid? OpponentId { get; }
    }
}
