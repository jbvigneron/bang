using Pang.Database.Models;

namespace Pang.Tests
{
    public class GameContext
    {
        public IEnumerable<string> PlayerNames { get; set; }
        public Game CurrentGame { get; set; }
    }
}
