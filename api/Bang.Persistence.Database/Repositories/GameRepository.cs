using Bang.App.Repositories;
using Bang.Domain.Entities;
using Bang.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bang.Persistence.Database.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly BangDbContext context;

        public GameRepository(BangDbContext context)
        {
            this.context = context;
        }

        public CurrentGame Get(Guid id) =>
            this.context.Games
                .Include(g => g.Players)
                    .ThenInclude(p => p.Character)
                .Include(g => g.Players)
                    .ThenInclude(p => p.Weapon)
                .Include(g => g.Players)
                    .ThenInclude(p => p.Role)
                .Include(g => g.Players)
                    .ThenInclude(p => p.CardsInGame)
                .First(g => g.Id == id);

        public CurrentGame Create(IEnumerable<Player> players, Player firstPlayer)
        {
            var cards = this.GetCardsOrderedRandomly();

            var game = new CurrentGame
            {
                Status = GameStatus.WaitingForPlayers,
                Players = players.ToArray(),
                CurrentPlayerName = firstPlayer.Name,
                DeckCount = cards.Count()
            };

            this.context.Games.Add(game);
            this.CreateDeckPile(game, cards);
            this.CreateDiscardPile(game, cards);
            this.context.SaveChanges();

            return game;
        }

        public CurrentGame Create(IEnumerable<(string Name, RoleKind RoleId, CharacterKind CharacterId)> players)
        {
            var cards = this.GetCardsOrderedRandomly();

            var game = new CurrentGame
            {
                Id = Guid.NewGuid(),
                Status = GameStatus.WaitingForPlayers,
                Players = players.Select(player =>
                {
                    var character = this.context.Characters.First(c => c.Id == player.CharacterId);
                    var role = this.context.Roles.First(r => r.Id == player.RoleId);
                    var isSheriff = role.Id == RoleKind.Sheriff;

                    return new Player
                    {
                        Name = player.Name,
                        Role = role,
                        IsSheriff = isSheriff,
                        Character = character,
                        Lives = character.Lives + (isSheriff ? 1 : 0),
                        Status = PlayerStatus.NotReady,
                        Weapon = this.context.Weapons.First(w => w.Id == WeaponKind.Colt45)
                    };
                }).ToArray(),
                CurrentPlayerName = players.Single(info => info.RoleId == RoleKind.Sheriff).Name,
                DeckCount = cards.Count,
            };

            this.context.Games.Add(game);
            this.CreateDeckPile(game, cards);
            this.CreateDiscardPile(game, cards);
            this.context.SaveChanges();

            return game;
        }

        private ICollection<Card> GetCardsOrderedRandomly() =>
            this.context.Cards
                .OrderBy(c => Guid.NewGuid())
                .ToArray();

        private void CreateDeckPile(CurrentGame game, ICollection<Card> cards)
        {
            var deck = new GameDeck
            {
                Game = game,
                Cards = cards
            };

            this.context.Decks.Add(deck);
        }

        private void CreateDiscardPile(CurrentGame game, ICollection<Card> cards)
        {
            var discard = new GameDiscard
            {
                Game = game,
                Cards = cards
            };

            this.context.DiscardPiles.Add(discard);
        }
    }
}