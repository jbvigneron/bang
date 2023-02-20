using Bang.Models;

namespace Bang.Tests.Contexts
{
    public class GameContext
    {
        public GameContext()
        {
            this.CardsInHand = new Dictionary<string, IList<Card>>();
        }

        public Game Current { get; set; }
        public IDictionary<string, IList<Card>> CardsInHand { get; set; }
    }
}
