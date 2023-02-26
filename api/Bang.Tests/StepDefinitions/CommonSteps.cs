using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions
{
    [Binding]
    public sealed class CommonSteps
    {
        private readonly AdminDriver adminDriver;
        private readonly GameDriver gameDriver;

        public CommonSteps(AdminDriver adminDriver, GameDriver gameDriver)
        {
            this.adminDriver = adminDriver;
            this.gameDriver = gameDriver;
        }

        [Given(@"une partie est créée avec ces joueurs")]
        public Task GivenUnePartieEstInitieeParCesJoueurs(Table table)
        {
            var players = table.Rows.Select(p => (p["playerName"], p["characterName"], p["role"]));
            return this.adminDriver.InitPreparedGameAsync(players);
        }

        [Given(@"""([^""]*)"" possède une carte ""([^""]*)"" dans son jeu")]
        public async Task GivenPossedeUneCarteDansSonJeu(string playerName, string cardName)
        {
            await this.adminDriver.ForceCardInPlayerHand(playerName, cardName);
            await this.gameDriver.UpdatePlayerCardsAsync(playerName);
        }

        [When(@"""([^""]*)"" rejoint la partie")]
        public Task WhenRejointLaPartie(string playerName)
        {
            return this.gameDriver.JoinGameAsync(playerName);
        }

        [Given(@"tous les joueurs ont rejoint la partie")]
        public Task GivenTousLesJoueursOntRejointLaPartie()
        {
            return this.gameDriver.AllJoinGameAsync();
        }

        [When(@"c'est au tour de ""([^""]*)"", il pioche 2 cartes")]
        public async Task WhenCestAuTourDeIlPiocheCartes(string playerName)
        {
            await this.gameDriver.DrawCardsAsync(playerName);
            await this.gameDriver.UpdatePlayerCardsAsync(playerName);
        }
    }
}