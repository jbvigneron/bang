namespace Bang.Core.Constants
{
    public static class HubMessages
    {
        public static class Public
        {
            public const string GameCreated = "GameCreated";
        }

        public static class Game
        {
            public const string PlayerJoin = "PlayerJoin";
            public const string DeckUpdated = "DeckUpdated";
            public const string AllPlayerJoined = "AllPlayerJoined";
            public const string PlayerTurn = "PlayerTurn";
            public const string CardsDrawn = "CardsDrawn";
            public const string CardPlaced = "CardPlaced";
            public const string WeaponChanged = "WeaponChanged";
            public const string CardDiscarded = "CardDiscarded";
        }

        public static class Player
        {
            public const string YourTurn = "YourTurn";
            public const string CardsInHand = "CardsInHand";
        }
    }
}
