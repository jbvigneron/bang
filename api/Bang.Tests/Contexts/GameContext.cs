using Bang.Database.Models;

namespace Bang.Tests.Contexts
{
    public class GameContext
    {
        public Game Current { get; internal set; }
    }
}
