using Bang.Domain.Entities;
using System.Linq;

namespace Bang.Domain.Extensions
{
    public static class CurrentGameExtensions
    {
        public static Player GetSheriff(this CurrentGame game)
        {
            return game.Players.First(p => p.IsSheriff);
        }
    }
}