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

        [Given(@"une partie est lancée avec ces joueurs")]
        public async Task GivenUnePartieEstLanceeAvecCesJoueurs(Table table)
        {
            var playerNames = table.Rows.Select(r => r["playerName"]);
            await this.gameDriver.InitGameAsync(playerNames);

            foreach (var playerName in playerNames)
            {
                await this.gameDriver.JoinGameAsync(playerName);
            }
        }

        [When(@"c'est au tour du schérif")]
        public async Task WhenCestAuTourDuScherif()
        {
            var scheriffName = this.gameDriver.GetScheriffName();
            await this.gameDriver.DrawCardsAsync(scheriffName);
            await this.gameDriver.UpdatePlayerCardsAsync(scheriffName);
        }

        [Then(@"le schérif pioche au moins (.*) cartes")]
        public void ThenPiocheAuMoinsCartes(int cardsCount)
        {
            var scheriffName = this.gameDriver.GetScheriffName();
            this.rulesDriver.CheckPlayerDeckCountAfterDraw(scheriffName, cardsCount);
        }
    }
}
