using Bang.Database.Enums;
using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions.Technical
{
    [Binding]
    public sealed class SignalRSteps
    {
        private readonly GameDriver gameDriver;
        private readonly SignalRDriver signalRDriver;
        private readonly RulesDriver rulesDriver;
        private readonly StatusDriver stateDriver;

        private IEnumerable<string> playerNames;

        public SignalRSteps(GameDriver gameDriver, SignalRDriver eventsDriver, RulesDriver rulesDriver, StatusDriver stateDriver)
        {
            this.gameDriver = gameDriver;
            this.signalRDriver = eventsDriver;
            this.rulesDriver = rulesDriver;
            this.stateDriver = stateDriver;
        }

        [Given(@"ces joueurs souhaitent faire une partie")]
        public void GivenCesJoueursSouhaitentFaireUnePartie(Table table)
        {
            this.playerNames = table.Rows.Select(r => r["playerName"]);
        }

        [When(@"une partie est initialisée")]
        public async Task WhenUnePartieEstInitialisee()
        {
            await this.signalRDriver.ConnectToPublicHub();
            await this.gameDriver.InitGameAsync(this.playerNames);
        }

        [When(@"""([^""]*)"" rejoint la partie")]
        public async Task WhenRejointLaPartie(string playerName)
        {
            await this.gameDriver.JoinGameAsync(playerName);
            await this.signalRDriver.ConnectToInGameHub(playerName);
        }

        [Then(@"un message ""([^""]*)"" est envoyé au hub public")]
        public void ThenUnMessageAuHubPublic(string message)
        {
            this.signalRDriver.CheckPublicMessage(message);
        }

        [Then(@"le jeu contient (.*) cartes")]
        public void ThenLeJeuContientCartes(int count)
        {
            this.rulesDriver.CheckDeckCount(count);
        }

        [Then(@"""([^""]*)"" reçoit un message ""([^""]*)""")]
        public void ThenRecoitUnMessage(string receiver, string message)
        {
            this.signalRDriver.CheckInGameMessage(receiver, message);
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

        [Then(@"c'est au tour du schérif")]
        public void ThenCestAuTourDuScherif()
        {
            this.stateDriver.CheckIsScheriffTurn();
        }
    }
}