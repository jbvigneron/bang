using Bang.Models;

namespace Bang.WebApi.Models
{
    public class PlayCardRequest
    {
        public PlayCardRequest()
        {
        }

        public PlayCardRequest(Card card)
        {
            this.Card = card;
        }

        public PlayCardRequest(Card card, Guid opponentId)
            : this(card)
        {
            this.OpponentId = opponentId;
        }

        public Card Card { get; set; }
        public Guid? OpponentId { get; set; }
    }
}
