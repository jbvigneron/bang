using Bang.Domain.Enums;
using Bang.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace Bang.Domain.Entities
{
    public class Player
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public PlayerStatus Status { get; set; }

        public Character Character { get; set; }

        public int Lives { get; set; }

        public int Seat { get; set; }

        public bool IsSheriff { get; set; }

        public virtual Role Role { get; set; }

        public Weapon Weapon { get; set; }

        public Guid GameId { get; set; }

        public bool HasDrawnCards { get; set; }

        public void EnsureHasNotDrawnCards()
        {
            if (this.HasDrawnCards)
            {
                throw new PlayerException("Vous avez déjà pioché vos cartes.", this);
            }
        }

        public void EnsureHasDrawnCards()
        {
            if (!this.HasDrawnCards)
            {
                throw new PlayerException("Vous devez piocher avant de terminer votre tour.", this);
            }
        }

        public int CardsInHand { get; set; }

        public virtual ICollection<Card> CardsInGame { get; set; }
    }
}