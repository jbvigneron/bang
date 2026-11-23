using Bang.Domain.Enums;
using Bang.Tests.Contexts;
using System.Linq;

namespace Bang.Tests.Drivers
{
    public class StateDriver
    {
        private readonly GameContext gameContext;

        public StateDriver(GameContext gameContext)
        {
            this.gameContext = gameContext;
        }

        public void CheckGameStatus(GameStatus status)
        {
            Assert.Equal(status, this.gameContext.Current.Status);
        }

        public void CheckPlayerStatus(string playerName, PlayerStatus status)
        {
            var newPlayer = this.gameContext.Current.Players.Single(p => p.Name == playerName);
            Assert.Equal(status, newPlayer.Status);
        }

        public void CheckIsPlayerTurn(string playerName)
        {
            Assert.Equal(playerName, this.gameContext.Current.CurrentPlayerName);
        }
    }
}