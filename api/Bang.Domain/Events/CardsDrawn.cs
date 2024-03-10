using Bang.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Bang.Domain.Events
{
    public sealed class CardsDrawn : INotification
    {
        public CardsDrawn(CurrentGame game, Player player, IEnumerable<Card> cards)
        {
            this.Game = game;
            this.Player = player;
            this.Cards = cards;
        }

        public CurrentGame Game { get; }
        public Player Player { get; }
        public IEnumerable<Card> Cards { get; }
    }
}