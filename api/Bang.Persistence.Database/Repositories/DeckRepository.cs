using Bang.App.Repositories;
using Bang.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Bang.Persistence.Database.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly BangDbContext context;

        public DeckRepository(BangDbContext context)
        {
            this.context = context;
        }

        public GameDeck Get(Guid gameId) =>
            this.context.Decks
                .Include(d => d.Cards)
                .Include(d => d.Game)
                .First(d => d.GameId == gameId);

        public bool IsCardInDeck(Guid gameId, string cardName) =>
            this.context.Decks.Any(d => d.GameId == gameId && d.Cards.Any(c => c.Name == cardName));

        public void SwitchCard(PlayerHand playerHand, GameDeck deck, Card oldCard, Card newCard)
        {
            playerHand.Cards.Remove(oldCard);
            deck.Cards.Remove(newCard);

            deck.Cards.Add(oldCard);
            playerHand.Cards.Add(newCard);

            this.context.SaveChanges();
        }
    }
}