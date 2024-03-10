using Bang.App.Repositories;
using Bang.Domain.Entities;
using Bang.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bang.Persistence.Database.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly BangDbContext context;

        public PlayerRepository(BangDbContext context)
        {
            this.context = context;
        }

        public Player Get(Guid id) =>
            this.context.Players
                .Include(p => p.Character)
                .Include(p => p.Role)
                .Include(p => p.Weapon)
                .Single(g => g.Id == id);

        public PlayerHand GetHand(Guid playerId) =>
            this.context.PlayersHands
                .Include(d => d.Cards)
                .Include(d => d.Player)
                    .ThenInclude(p => p.CardsInGame)
                .Single(p => p.PlayerId == playerId);

        public void FillPlayerHand(GameDeck gameDeck, Player player, bool canStartGame)
        {
            var hand = new PlayerHand
            {
                Player = player,
                Cards = new List<Card>()
            };

            for (int i = 1; i <= hand.Player.Lives; i++)
            {
                var card = gameDeck.Cards.First();

                hand.Cards.Add(card);
                hand.Player.CardsInHand++;

                gameDeck.Cards.Remove(card);
                gameDeck.Game.DeckCount--;
            }

            this.context.PlayersHands.Add(hand);

            if (canStartGame)
            {
                gameDeck.Game.Status = GameStatus.InProgress;
            }

            this.context.SaveChanges();
        }

        public IEnumerable<Card> DrawCards(Player player)
        {
            var hand = this.context.PlayersHands
                .Include(d => d.Cards)
                .Single(p => p.PlayerId == player.Id);

            var deck = this.context.Decks
                .Include(d => d.Cards)
                .Include(d => d.Game)
                .Single(d => d.GameId == player.GameId);

            for (var i = 1; i <= 2; i++)
            {
                var card = deck.Cards.First();

                hand.Cards.Add(card);
                player.CardsInHand++;

                deck.Cards.Remove(card);
                deck.Game.DeckCount--;
            }

            player.HasDrawnCards = true;
            this.context.SaveChanges();

            return hand.Cards;
        }

        public void PlayBlueCard(PlayerHand playerHand, Card card)
        {
            if (card.Type != CardType.Blue)
            {
                throw new InvalidOperationException("La carte sélectionnée n'est pas une carte bleue");
            }

            playerHand.Cards.Remove(card);
            playerHand.Player.CardsInGame.Add(card);

            this.context.SaveChanges();
        }

        public void PlayBrownCard(PlayerHand playerHand, Card card)
        {
            var discardPile = this.context.DiscardPiles
                .Include(d => d.Cards)
                .Single(g => g.GameId == playerHand.Player.GameId);

            playerHand.Cards.Remove(card);
            discardPile.Cards.Add(card);

            this.context.SaveChanges();
        }

        public Weapon PlayWeaponCard(PlayerHand playerHand, Card card)
        {
            if (card.Type != CardType.Weapon)
            {
                throw new InvalidOperationException("La carte sélectionnée n'est pas une carte de type Arme");
            }

            playerHand.Cards.Remove(card);
            playerHand.Player.CardsInGame.Add(card);

            var weapon = this.context.Weapons.Single(w => w.Id.ToString() == card.Kind.ToString());
            playerHand.Player.Weapon = weapon;

            this.context.SaveChanges();

            return weapon;
        }

        public void SwitchCard(PlayerHand playerHand, PlayerHand otherHand, Card oldCard, Card newCard)
        {
            playerHand.Cards.Remove(oldCard);
            otherHand.Cards.Remove(newCard);

            otherHand.Cards.Add(oldCard);
            playerHand.Cards.Add(newCard);

            this.context.SaveChanges();
        }
    }
}