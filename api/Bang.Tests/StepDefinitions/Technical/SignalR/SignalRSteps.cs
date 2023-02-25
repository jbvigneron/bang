using Bang.Models.Enums;
using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions.Technical.SignalR
{
    [Binding]
    public sealed class SignalRSteps
    {
        private readonly GameDriver gameDriver;
        private readonly StateDriver stateDriver;

        private IEnumerable<string> playerNames;

        public SignalRSteps(GameDriver gameDriver, StateDriver stateDriver)
        {
            this.gameDriver = gameDriver;
            this.stateDriver = stateDriver;
        }

        [Given(@"ces joueurs souhaitent faire une partie")]
        public void GivenCesJoueursSouhaitentFaireUnePartie(Table table)
        {
            this.playerNames = table.Rows.Select(r => r["playerName"]);
        }

        [When(@"la partie est initialisée")]
        public async Task WhenUnePartieEstInitialisee()
        {
            await this.gameDriver.InitGameAsync(playerNames);
        }

        [When(@"""([^""]*)"" rejoint la partie")]
        public async Task WhenRejointLaPartie(string playerName)
        {
            await this.gameDriver.JoinGameAsync(playerName);
        }

        [Then(@"""([^""]*)"" est prêt")]
        public void ThenEstPret(string playerName)
        {
            this.stateDriver.CheckPlayerStatus(playerName, PlayerStatus.Alive);
        }

        [Then(@"la partie peut commencer")]
        public void ThenLaPartiePeutCommencer()
        {
            this.stateDriver.CheckGameStatus(GameStatus.InProgress);
        }

        [Then(@"c'est au tour du shérif")]
        public void ThenCestAuTourDuScherif()
        {
            this.stateDriver.CheckIsSheriffTurn();
        }

        [Then(@"le shérif pioche de nouvelles cartes")]
        public Task ThenLeScherifPiocheDeNouvellesCartes()
        {
            var sheriffName = this.gameDriver.GetSheriffName();
            return this.gameDriver.DrawCardsAsync(sheriffName);
        }
    }
}