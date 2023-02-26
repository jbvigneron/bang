using Bang.Models;

namespace Bang.Tests.Contexts
{
    public class GameContext
    {
        public Game Current { get; set; }
        public IDictionary<string, Player>? Players { get; } = new Dictionary<string, Player>();
        public IDictionary<string, IList<Card>> PlayerCardsInHand { get; } = new Dictionary<string, IList<Card>>();
    }
}
