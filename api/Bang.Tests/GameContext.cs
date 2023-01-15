using Bang.Database.Models;

namespace Bang.Tests
{
    public class GameContext
    {
        public HttpClient HttpClient { get; internal set; }
        public IEnumerable<string> PlayerNames { get; internal set; }
        public Game CurrentGame { get; internal set; }
        public Player CurrentPlayer { get; internal set; }
    }
}
