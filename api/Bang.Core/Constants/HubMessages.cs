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
            public const string GameDeckReady = "GameDeckReady";
            public const string PlayerJoin = "PlayerJoin";
            public const string GameDeckUpdated = "GameDeckUpdated";
            public const string AllPlayerJoined = "AllPlayerJoined";
            public const string PlayerTurn = "PlayerTurn";
        }

        public class Player
        {
            public const string PlayerDeckReady = "PlayerDeckReady";
        }
    }
}
