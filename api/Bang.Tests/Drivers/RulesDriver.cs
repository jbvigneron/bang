using Bang.Core.Extensions;
using Bang.Tests.Contexts;

namespace Bang.Tests.Drivers
{
    public class RulesDriver
    {
        private readonly GameContext gameContext;

        public RulesDriver(GameContext gameContext)
        {
            this.gameContext = gameContext;
        }

        public void CheckHasOneSheriff()
        {
            var sheriff = this.gameContext.Current.GetScheriff();
            Assert.NotNull(sheriff);
        }

        public void CheckAllOthersRoles(int othersCount)
        {
            var others = this.gameContext.Current.Players.Where(p => !p.IsScheriff);
            Assert.Equal(othersCount, others.Count());
        }

        public void CheckIsSheriffUnveiled()
        {
            var sheriff = this.gameContext.Current.GetScheriff();
            Assert.True(sheriff.IsScheriff);
        }

        public void CheckPlayerHasACharacter(string playerName)
        {
            var player = this.gameContext.Current.Players.Single(p => p.Name == playerName);
            Assert.NotNull(player.Character);
        }

        public void CheckPlayerLives(string playerName, Table table)
        {
            var player = this.gameContext.Current.Players.Single(p => p.Name == playerName);
            var expectedLives = int.Parse(table.Rows.Single(r => r["characterName"] == player!.Character!.Name)["lives"]);

            if (player!.IsScheriff)
            {
                Assert.Equal(expectedLives + 1, player.Lives);
            }
            else
            {
                Assert.Equal(expectedLives, player.Lives);
            }
        }

        public void CheckWeapon(string playerName, string weaponName, int weaponRange)
        {
            var player = this.gameContext.Current.Players.Single(p => p.Name == playerName);
            Assert.Equal(weaponName, player!.Weapon!.Name);
            Assert.Equal(weaponRange, player.Weapon.Range);
        }

        public void CheckFirstPlayerIsTheScheriff()
        {
            var scheriff = this.gameContext.Current.GetScheriff();
            Assert.Equal(scheriff.Name, this.gameContext.Current.CurrentPlayerName);
        }

        public void CheckGameDeckCount(int count)
        {
            Assert.Equal(count, this.gameContext.Current.DeckCount);
        }

        public void CheckPlayerDeckCount(string playerName)
        {
            var numberOfCards = this.gameContext.PlayerCards[playerName].Count();

            var player = this.gameContext.Current.Players.Single(p => p.Name == playerName);
            Assert.Equal(player.Lives, numberOfCards);
        }
    }
}
