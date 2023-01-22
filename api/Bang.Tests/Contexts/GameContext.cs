using Bang.Database.Models;

namespace Bang.Tests.Contexts
{
    public class GameContext
    {
        public Game Current { get; set; }
        public IEnumerable<Card> Cards { get; set; }
    }
}
