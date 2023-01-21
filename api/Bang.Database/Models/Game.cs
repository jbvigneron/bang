using Bang.Database.Enums;
using System.Text.Json.Serialization;

namespace Bang.Database.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public GameStatus GameStatus { get; set; }
        public string? CurrentPlayerName { get; set; }
        public virtual IList<Player> Players { get; set; }
        public int DeckCount { get; set; }

        [JsonIgnore]
        public virtual ICollection<GameDeckCard> Deck { get; set; }
        public virtual IEnumerable<GameDiscardCard> DiscardPile { get; set; }
    }
}
