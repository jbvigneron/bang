using Bang.Database.Enums;

namespace Bang.Database.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public GameStatusEnum GameStatus { get; set; }
        public string? CurrentPlayerName { get; set; }
        public virtual IList<Player> Players { get; set; }
    }
}
