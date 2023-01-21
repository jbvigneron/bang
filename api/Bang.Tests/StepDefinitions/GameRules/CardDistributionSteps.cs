using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions.GameRules
{
    [Binding]
    public class CardDistributionSteps
    {
        private readonly GameDriver gameDriver;
        private readonly RulesDriver rulesDriver;

        public CardDistributionSteps(GameDriver gameDriver, RulesDriver rulesDriver)
        {
            this.gameDriver = gameDriver;
            this.rulesDriver = rulesDriver;
        }

        [When(@"la partie est prête pour ces joueurs")]
        public async Task WhenLaPartieEstPretePourCesJoueurs(Table table)
        {
            var playerNames = table.Rows.Select(r => r["playerName"]).ToList();
            await this.gameDriver.InitGameAsync(playerNames);
            playerNames.ForEach(async p => await this.gameDriver.JoinGameAsync(p));
        }

        [Then(@"""([^""]*)"" possède autant de cartes qu'il a de points de vie")]
        public async Task ThenPossedeAutantDeCartesQuilADePointsDeVies(string playerName)
        {
            await this.gameDriver.UpdateGameAsync();
            this.rulesDriver.CheckNumberOfCards(playerName);
        }
    }
}
