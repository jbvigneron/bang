namespace Bang.Database.Models
{
    public class GameDeckCard
    {
        public Guid Id { get; set; }
        public virtual Card Card { get; set; }
        public Guid CardId { get; set; }
        public Guid GameId { get; set; }
    }
}
