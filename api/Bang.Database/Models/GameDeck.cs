namespace Bang.Database.Models
{
    public class GameDeck
    {
        public Guid Id {  get; set; }
        public Guid GameId { get; set; }
        public virtual Game Game { get; internal set; }
        public virtual IEnumerable<Card> Cards { get; set; }
    }
}
