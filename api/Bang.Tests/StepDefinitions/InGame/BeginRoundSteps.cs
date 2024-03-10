using Bang.Tests.Drivers;
using System.Threading.Tasks;

namespace Bang.Tests.StepDefinitions.InGame
{
    [Binding]
    public class BeginRoundSteps
    {
        private readonly GameDriver gameDriver;
        private readonly RulesDriver rulesDriver;

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
    }
}