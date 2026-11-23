using Bang.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Bang.Tests.Contexts
{
    public class GameContext
    {
        private CurrentGame? current;

        public GameContext()
        {
            this.Players = new Dictionary<string, Player>();
            this.PlayerCardsInHand = new Dictionary<string, IList<Card>>();
        }

        public CurrentGame Current
        {
            get => this.current ?? throw new InvalidOperationException("The current game has not been initialized.");
            set => this.current = value;
        }
        public IDictionary<string, Player> Players { get; }
        public IDictionary<string, IList<Card>> PlayerCardsInHand { get; }
    }
}