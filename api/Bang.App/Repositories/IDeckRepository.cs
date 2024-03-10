using Bang.Domain.Entities;
using System;

namespace Bang.App.Repositories
{
    public interface IDeckRepository
    {
        GameDeck Get(Guid gameId);

        bool IsCardInDeck(Guid gameId, string cardName);

        void SwitchCard(PlayerHand playerHand, GameDeck deck, Card oldCard, Card newCard);
    }
}