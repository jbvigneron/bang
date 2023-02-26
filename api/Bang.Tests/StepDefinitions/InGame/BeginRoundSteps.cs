using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions.GameRules
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

        [When(@"""([^""]*)"" joue une carte")]
        public async Task WhenJoueUneCarte(string playerName)
        {
            await this.gameDriver.PlayRandomCardAsync(playerName);

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