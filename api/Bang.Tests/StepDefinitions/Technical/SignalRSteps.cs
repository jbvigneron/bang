using Bang.Core.Constants;
using Bang.Database.Enums;
using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions.Technical
{
    [Binding]
    public sealed class SignalRSteps
    {
        private readonly GameDriver gameDriver;
        private readonly SignalRDriver signalRDriver;
        private readonly StatusDriver stateDriver;

        public SignalRSteps(GameDriver gameDriver, SignalRDriver eventsDriver, StatusDriver stateDriver)
        {
            this.gameDriver = gameDriver;
            this.signalRDriver = eventsDriver;
            this.stateDriver = stateDriver;
        }

        [Given(@"une partie est initialisée avec ces joueurs")]
        public Task GivenUnePartieEstInitialiseeAvecCesJoueurs(Table table)
        {
            var playerNames = table.Rows.Select(r => r["playerName"]);
            return this.gameDriver.InitGameAsync(playerNames);
        }

        [When(@"""([^""]*)"" rejoint la partie")]
        public async Task WhenRejointLaPartie(string playerName)
        {
            await this.gameDriver.JoinGameAsync(playerName);
            await this.signalRDriver.ListenGameHub(playerName);
        }

        [Then(@"""([^""]*)"" est averti que ""([^""]*)"" est présent")]
        public void ThenEstAvertiQueEstPresent(string receiver, string emitter)
        {
            this.signalRDriver.CheckPlayerMessages(receiver, EventNames.PlayerReady, 1);
            this.stateDriver.CheckPlayerStatus(emitter, PlayerStatus.Alive);
        }

        [Then(@"tous les joueurs sont avertis")]
        public void ThenTousLesJoueursSontAvertis()
        {
            this.signalRDriver.CheckMessages(EventNames.GameReady);
        }


        [Then(@"la partie peut commencer")]
        public void ThenLaPartiePeutCommencer()
        {
            this.stateDriver.CheckGameStatus(GameStatus.InProgress);
        }
    }
}