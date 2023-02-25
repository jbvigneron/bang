namespace Bang.WebApi.Models
{
    public class PlayCardRequest
    {
        public PlayCardRequest()
        {
        }

        public PlayCardRequest(Guid cardId)
        {
            this.CardId = cardId;
        }

        public PlayCardRequest(Guid cardId, Guid opponentId)
            : this(cardId)
        {
            this.OpponentId = opponentId;
        }

        public Guid CardId { get; set; }
        public Guid? OpponentId { get; set; }
    }
}
