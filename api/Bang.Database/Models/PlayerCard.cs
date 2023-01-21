namespace Bang.Database.Models
{
    public class PlayerCard
    {
        public Guid Id { get; set; }
        public virtual Card Card { get; set; }
        public Guid CardId { get; set; }
    }
}
