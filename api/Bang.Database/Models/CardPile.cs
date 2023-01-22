namespace Bang.Database.Models
{
    public abstract class CardPile
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public virtual Game Game { get; set; }
        public virtual IEnumerable<Card> Cards { get; set; }
    }
}
