using Bang.Models;
using MediatR;

namespace Bang.Core.Commands
{
    public class PlayCardCommand : IRequest
    {
        public PlayCardCommand(Guid playerId, Card card, Guid? opponentId)
        {
            this.PlayerId = playerId;
            this.Card = card;
            this.OpponentId = opponentId;
        }

        public Guid PlayerId { get; }
        public Card Card { get; }
        public Guid? OpponentId { get; }
    }
}
