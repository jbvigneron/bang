namespace Bang.Models
{
    public abstract class CardPile
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public virtual Game? Game { get; set; }
        public virtual ICollection<Card>? Cards { get; set; }
    }
}
