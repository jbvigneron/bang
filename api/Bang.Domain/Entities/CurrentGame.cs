using Bang.Domain.Enums;
using Bang.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Player GetNextPlayer(Player currentPlayer)
        {
            var seatedPlayers = this.Players.OrderBy(p => p.Seat).ToList();
            var currentIndex = seatedPlayers.FindIndex(p => p.Id == currentPlayer.Id);

            for (var offset = 1; offset <= seatedPlayers.Count; offset++)
            {
                var candidate = seatedPlayers[(currentIndex + offset) % seatedPlayers.Count];

                if (candidate.Lives > 0)
                {
                    return candidate;
                }
            }

            throw new GameException("Aucun joueur vivant ne peut jouer le tour suivant.", this);
        }
    }
}