using Bang.Tests.Drivers;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bang.Tests.StepDefinitions.InGame
{
    [Binding]
    public class BeginRoundSteps
    {
        private readonly GameDriver gameDriver;
        private readonly RulesDriver rulesDriver;
        private HttpResponseMessage endTurnResponse;

        public BeginRoundSteps(GameDriver gameDriver, RulesDriver rulesDriver)
        {
            this.gameDriver = gameDriver;
            this.rulesDriver = rulesDriver;
        }

        [When(@"""([^""]*)"" joue une carte bleue")]
        public async Task WhenJoueUneCarteBleue(string playerName)
        {
            await this.gameDriver.PlayRandomBlueCardAsync(playerName);

            await this.gameDriver.UpdateGameAsync();
            await this.gameDriver.UpdatePlayerCardsAsync(playerName);
        }

        [Then(@"""([^""]*)"" possède (.*) cartes en main")]
        public void ThenPossedeCartesEnMain(string playerName, int cardsCount)
        {
            this.rulesDriver.CheckPlayerInHandCards(playerName, cardsCount);
        }

        [When(@"""([^""]*)"" termine son tour en défaussant (\d+) cartes?")]
        public async Task WhenTermineSonTourEnDefaussant(string playerName, int discardCount)
        {
            await this.gameDriver.EndTurnAsync(playerName, discardCount);

            await this.gameDriver.UpdateGameAsync();
            await this.gameDriver.UpdatePlayerCardsAsync(playerName);
        }

        [When(@"""([^""]*)"" tente de terminer son tour en défaussant (\d+) cartes?")]
        public async Task WhenTenteDeTerminerSonTourEnDefaussant(string playerName, int discardCount)
        {
            this.endTurnResponse = await this.gameDriver.TryEndTurnAsync(playerName, discardCount);
        }

        [Then(@"la fin de tour est refusée")]
        public void ThenLaFinDeTourEstRefusee()
        {
            Assert.False(this.endTurnResponse.IsSuccessStatusCode);
        }
    }
}
