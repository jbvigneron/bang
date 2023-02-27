using Bang.Models;

namespace Bang.Tests.Contexts
{
    public class GameContext
    {
        public GameContext()
        {
            this.Players = new Dictionary<string, Player>();
            this.PlayerCardsInHand = new Dictionary<string, IList<Card>>();
        }

        public Game? Current { get; set; }
        public IDictionary<string, Player>? Players { get; }
        public IDictionary<string, IList<Card>> PlayerCardsInHand { get; }
    }
}
