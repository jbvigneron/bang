namespace Pang.Database.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public GameStatus GameStatus { get; set; }
        public virtual List<Player>? Players { get; set; }
    }
}
