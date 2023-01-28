namespace Bang.Core.Constants
{
    public static class HubMessages
    {
        public class Public
        {
            public const string NewGame = "NewGame";
        }

        public class Game
        {
            public const string GameDeckReady = "DeckReady";
            public const string PlayerJoin = "PlayerJoin";
            public const string DeckUpdated = "DeckUpdated";
            public const string AllPlayerJoined = "AllPlayerJoined";
            public const string PlayerTurn = "PlayerTurn";
            public const string CardsDrawn = "CardsDrawn";
        }

        public class Player
        {
            public const string DeckReady = "DeckReady";
            public const string YourTurn = "YourTurn";
            public const string NewCards = "NewCards";
        }
    }
}
