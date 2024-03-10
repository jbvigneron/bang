using Bang.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Bang.App.Repositories
{
    public interface IPlayerRepository
    {
        Player Get(Guid id);

        PlayerHand GetHand(Guid playerId);

        void FillPlayerHand(GameDeck gameDeck, Player player, bool canStartGame);

        IEnumerable<Card> DrawCards(Player player);

        void PlayBlueCard(PlayerHand playerHand, Card card);

        void PlayBrownCard(PlayerHand playerHand, Card card);

        Weapon PlayWeaponCard(PlayerHand playerHand, Card card);

        void SwitchCard(PlayerHand playerHand, PlayerHand otherHand, Card oldCard, Card newCard);
    }
}