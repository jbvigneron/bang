using Bang.Tests.Drivers;
using System.Threading.Tasks;

namespace Bang.Tests.StepDefinitions.InGame
{
    [Binding]
    public class PlayerWeaponCardSteps
    {
        private readonly GameDriver gameDriver;
        private readonly RulesDriver rulesDriver;

        public PlayerWeaponCardSteps(GameDriver gameDriver, RulesDriver rulesDriver)
        {
            this.gameDriver = gameDriver;
            this.rulesDriver = rulesDriver;
        }

        [When(@"""([^""]*)"" joue une carte ""([^""]*)""")]
        public async Task WhenJoueUneCarte(string playerName, string cardName)
        {
            await this.gameDriver.PlayCardAsync(playerName, cardName);

            await this.gameDriver.UpdateGameAsync();
            await this.gameDriver.UpdatePlayerCardsAsync(playerName);
        }

        [Then(@"""([^""]*)"" place sa carte ""([^""]*)"" devant lui")]
        public void ThenPlaceSaCarteDevantLui(string playerName, string cardName)
        {
            this.rulesDriver.CheckPlayerInGameCards(playerName, cardName);
        }

        [Then(@"""([^""]*)"" est armé d'une ""([^""]*)"" ayant une portée de (.*)")]
        public void ThenEstArmeDuneAyantUnePorteeDe(string playerName, string weaponName, int weaponRange)
        {
            this.rulesDriver.CheckWeapon(playerName, weaponName, weaponRange);
        }
    }
}