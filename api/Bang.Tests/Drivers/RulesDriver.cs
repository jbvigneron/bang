using Bang.Core.Extensions;
using Bang.Database.Models;
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

        public void CheckPlayerHasACharacter(Player player)
        {
            Assert.NotNull(player.Character);
        }

        public void CheckPlayerLives(Player player, int expectedLives)
        {
            if (player!.IsScheriff)
            {
                Assert.Equal(expectedLives + 1, player.Lives);
            }
            else
            {
                Assert.Equal(expectedLives, player.Lives);
            }
        }

        public void CheckWeapon(Player player, string weaponName, int weaponRange)
        {
            Assert.Equal(weaponName, player!.Weapon!.Name);
            Assert.Equal(weaponRange, player.Weapon.Range);
        }

        public void CheckFirstPlayerIsTheScheriff()
        {
            var scheriff = this.gameContext.Current.GetScheriff();
            Assert.Equal(scheriff.Name, this.gameContext.Current.CurrentPlayerName);
        }
    }
}
