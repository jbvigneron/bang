﻿using Bang.Database.Enums;

namespace Bang.Database.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public GameStatus GameStatus { get; set; }
        public string? CurrentPlayerName { get; set; }
        public virtual IList<Player> Players { get; set; }
        public int DeckCount { get; set; }
        public virtual IEnumerable<GameDiscard> DiscardPile { get; set; }
    }
}