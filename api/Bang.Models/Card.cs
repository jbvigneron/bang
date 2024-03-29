﻿using Bang.Models.Enums;

namespace Bang.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public CardKind Kind { get; set; }
        public string? Name { get; set; }
        public CardType Type { get; set; }
        public string? Description { get; set; }
        public CardValue Value { get; set; }
        public CardSymbol Symbol { get; set; }
        public int? Range { get; set; }
        public bool RequireOpponent { get; set; }
    }
}
