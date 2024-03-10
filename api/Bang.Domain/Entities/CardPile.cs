using System;
using System.Collections.Generic;

namespace Bang.Domain.Entities
{
    public abstract class CardPile
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public virtual CurrentGame Game { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}