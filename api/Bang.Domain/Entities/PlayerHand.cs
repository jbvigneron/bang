using System;
using System.Collections.Generic;

namespace Bang.Domain.Entities
{
    public class PlayerHand
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}