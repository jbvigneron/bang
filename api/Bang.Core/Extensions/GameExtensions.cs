using Bang.Models;

namespace Bang.Core.Extensions
{
    public static class GameExtensions
    {
        public static Player GetSheriff(this Game game)
        {
            return game.Players.First(p => p.IsSheriff);
        }
    }
}
