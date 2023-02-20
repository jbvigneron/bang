using Bang.Models;

namespace Bang.WebApi.Requests
{
    public class SwitchCardRequest
    {
        public SwitchCardRequest(Card oldCard, string newCardName)
        {
            this.OldCard = oldCard;
            this.NewCardName = newCardName;
        }

        public Card OldCard { get; set; }
        public string NewCardName { get; set; }
    }
}
