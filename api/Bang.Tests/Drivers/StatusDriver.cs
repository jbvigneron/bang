using Bang.Database.Enums;
using Bang.Tests.Contexts;

namespace Bang.Tests.Drivers
{
    public class StatusDriver
    {
        private readonly GameContext gameContext;

        public StatusDriver(GameContext gameContext)
        {
            this.gameContext = gameContext;
        }

        public void CheckGameStatus(GameStatus status)
        {
            Assert.Equal(status, this.gameContext.Current.GameStatus);
        }

        public void CheckPlayerStatus(string playerName, PlayerStatus status)
        {
            var newPlayer = this.gameContext.Current.Players.Single(p => p.Name == playerName);
            Assert.Equal(status, newPlayer.Status);
        }
    }
}
