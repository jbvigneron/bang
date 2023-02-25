using Bang.Core.Extensions;
using MediatR;
using System.Security.Claims;

namespace Bang.Core.Commands
{
    public class PlayCardCommand : IRequest
    {
        public PlayCardCommand(ClaimsPrincipal user, Guid cardId, Guid? opponentId)
        {
            this.PlayerId = user.GetId();
            this.PlayerName = user.GetName();
            this.GameId = user.GetGameId();
            this.CardId = cardId;
            this.OpponentId = opponentId;
        }

        public Guid PlayerId { get; }
        public string PlayerName { get; }
        public Guid GameId { get; }
        public Guid CardId { get; }
        public Guid? OpponentId { get; }
    }
}
