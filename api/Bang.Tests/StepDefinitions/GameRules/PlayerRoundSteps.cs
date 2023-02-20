using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions.GameRules
{
    [Binding]
    public class PlayerRoundSteps
    {
        private readonly GameDriver gameDriver;
        private readonly RulesDriver rulesDriver;

        public PlayerRoundSteps(GameDriver gameDriver, RulesDriver rulesDriver)
        {
            this.gameDriver = gameDriver;
            this.rulesDriver = rulesDriver;
        }

        [Given(@"une partie est initiée par ces joueurs")]
        public Task GivenUnePartieEstInitieeParCesJoueurs(Table table)
        {
            var players = table.Rows.Select(p => (p["playerName"], p["characterName"], p["role"]));
            return this.gameDriver.InitPreparedGameAsync(players);
        }

        [Given(@"les joueurs rejoignent la partie")]
        public async Task GivenLesJoueursRejoignentLaPartie(Table table)
        {
            foreach (var row in table.Rows)
            {
                await this.gameDriver.JoinGameAsync(row["playerName"]);
            }
        }

        [Given(@"""([^""]*)"" possède une carte ""([^""]*)"" dans son jeu")]
        public async Task GivenPossedeUneCarteDansSonJeu(string playerName, string cardName)
        {
            await this.gameDriver.CheckAndSwitchCardAsync(playerName, cardName);
            await this.gameDriver.UpdatePlayerCardsAsync(playerName);
        }

        [When(@"c'est au tour de ""([^""]*)""")]
        public async Task WhenCestAuTourDe(string playerName)
        {
            await this.gameDriver.DrawCardsAsync(playerName);
            await this.gameDriver.UpdatePlayerCardsAsync(playerName);
        }

        [When(@"""([^""]*)"" joue une carte ""([^""]*)""")]
        public async Task WhenJoueUneCarte(string playerName, string cardName)
        {
            await this.gameDriver.PlayCardAsync(playerName, cardName);

            await this.gameDriver.UpdateGameAsync();
            await this.gameDriver.UpdatePlayerCardsAsync(playerName);
        }

        [Then(@"""([^""]*)"" possède (.*) cartes en main")]
        public void ThenPossedeCartesEnMain(string playerName, int cardsCount)
        {
            this.rulesDriver.CheckPlayerInHandCards(playerName, cardsCount);
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