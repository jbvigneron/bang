using Bang.Database.Models;

namespace Bang.Core.Extensions
{
    public static class GameExtensions
    {
        public static Player GetScheriff(this Game game)
        {
            return game.Players.First(p => p.IsScheriff);
        }
    }
}
