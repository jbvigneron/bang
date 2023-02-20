using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions.GameRules
{
    [Binding]
    public class WeaponCardsSteps
    {
        private readonly GameDriver gameDriver;
        private readonly RulesDriver rulesDriver;

        public WeaponCardsSteps(GameDriver gameDriver, RulesDriver rulesDriver)
        {
            this.gameDriver = gameDriver;
            this.rulesDriver = rulesDriver;
        }

        [Given(@"""([^""]*)"" possède une carte ""([^""]*)"" dans son jeu")]
        public async Task GivenPossedeUneCarteDansSonJeu(string playerName, string cardName)
        {
            await this.gameDriver.CheckAndSwitchCardAsync(playerName, cardName);
            await this.gameDriver.UpdatePlayerCardsAsync(playerName);
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