using Bang.Models.Enums;
using System;
using System.Collections.Generic;

namespace Bang.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public GameStatus Status { get; set; }
        public string CurrentPlayerName { get; set; }
        public virtual IList<Player> Players { get; set; }
        public int DeckCount { get; set; }
        public virtual IEnumerable<GameDiscard> DiscardPile { get; set; }
    }
}