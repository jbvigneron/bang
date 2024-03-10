using Bang.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Bang.Domain.Entities
{
    public class CurrentGame
    {
        public Guid Id { get; set; }
        public GameStatus Status { get; set; }
        public string CurrentPlayerName { get; set; }
        public virtual IList<Player> Players { get; set; }
        public int DeckCount { get; set; }
        public virtual IEnumerable<GameDiscard> DiscardPile { get; set; }
    }
}