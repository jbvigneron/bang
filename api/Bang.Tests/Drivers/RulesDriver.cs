using Bang.Core.Extensions;
using Bang.Models.Enums;
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
            var sheriff = this.gameContext.Current.GetSheriff();
            Assert.NotNull(sheriff);
        }

        public void CheckHasOneRenegade()
        {
            var renegade = this.gameContext.Current.Players.SingleOrDefault(p => p.Role!.Id == RoleKind.Renegade);
            Assert.NotNull(renegade);
        }

        public void CheckOutlawsCount(int count)
        {
            var outlaws = this.gameContext.Current.Players.Where(p => p.Role!.Id == RoleKind.Outlaw);
            Assert.Equal(count, outlaws.Count());
        }

        public void CheckDeputiesCount(int count)
        {
            var deputies = this.gameContext.Current.Players.Where(p => p.Role!.Id == RoleKind.DeputySheriff);
            Assert.Equal(count, deputies.Count());
        }

        public void CheckIsSheriffUnveiled()
        {
            var sheriff = this.gameContext.Current.GetSheriff();
            Assert.True(sheriff.IsSheriff);
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

            if (player!.IsSheriff)
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
            var sheriff = this.gameContext.Current.GetSheriff();
            Assert.Equal(sheriff.Name, this.gameContext.Current.CurrentPlayerName);
        }

        public void CheckGameDeckCount(int count)
        {
            Assert.Equal(count, this.gameContext.Current.DeckCount);
        }

        public void CheckPlayerDeckCount(string playerName)
        {
            var playerDeckCount = this.gameContext.PlayerCards[playerName].Count();

            var player = this.gameContext.Current.Players.Single(p => p.Name == playerName);
            var expected = player.Lives;
            Assert.Equal(expected, playerDeckCount);
        }

        public void CheckPlayerDeckCountAfterDraw(string playerName, int count)
        {
            var playerDeckCount = this.gameContext.PlayerCards[playerName].Count();

            var player = this.gameContext.Current.Players.Single(p => p.Name == playerName);
            var expected = player.Lives + count;
            Assert.True(playerDeckCount >= expected);
        }
    }
}
