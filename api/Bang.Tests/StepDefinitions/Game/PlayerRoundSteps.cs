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
        public async Task GivenUnePartieEstInitieeParCesJoueurs(Table table)
        {
            var players = table.Rows.Select(p => (p["playerName"], p["characterName"], p["role"]));
            await this.gameDriver.InitPreparedGameAsync(players);

            foreach (var row in table.Rows)
            {
                await this.gameDriver.JoinGameAsync(row["playerName"]);
            }
        }

        [When(@"c'est au tour de ""([^""]*)"", il pioche 2 cartes")]
        public async Task WhenCestAuTourDeIlPiocheCartes(string playerName)
        {
            await this.gameDriver.DrawCardsAsync(playerName);
            await this.gameDriver.UpdatePlayerCardsAsync(playerName);
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