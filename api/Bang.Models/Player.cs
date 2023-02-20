using Bang.Models.Enums;

namespace Bang.Models
{
    public class Player
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public PlayerStatus Status { get; set; }

        public Character? Character { get; set; }

        public int Lives { get; set; }

        public bool IsSheriff { get; set; }

        public virtual Role? Role { get; set; }

        public Weapon? Weapon { get; set; }

        public Guid GameId { get; set; }

        public bool HasDrawnCards { get; set; }

        public int CardsInHand { get; set; }

        public virtual ICollection<Card> CardsInGame { get; set; }
    }
}
