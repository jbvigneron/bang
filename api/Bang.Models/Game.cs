using Bang.Models.Enums;

namespace Bang.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public GameStatus Status { get; set; }
        public string? CurrentPlayerName { get; set; }
        public virtual IList<Player> Players { get; set; }
        public int DeckCount { get; set; }
        public virtual IEnumerable<GameDiscardPile> DiscardPile { get; set; }
    }
}
