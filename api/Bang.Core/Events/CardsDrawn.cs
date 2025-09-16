using Bang.Models;
using MediatR;
using System.Collections.Generic;

namespace Bang.Core.Events
{
    public class CardsDrawn : INotification
    {
        public CardsDrawn(Game game, Player player, IEnumerable<Card> cards)
        {
            this.Game = game;
            this.Player = player;
            this.Cards = cards;
        }

        public Game Game { get; }
        public Player Player { get; }
        public IEnumerable<Card> Cards { get; }
    }
}
