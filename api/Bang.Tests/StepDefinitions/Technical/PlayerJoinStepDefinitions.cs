using Bang.Core.Constants;
using Bang.Database.Enums;
using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions.Technical
{
    [Binding]
    public sealed class PlayerJoinStepDefinitions
    {
        private readonly GameDriver gameDriver;
        private readonly SignalRDriver signalRDriver;
        private readonly StatusDriver stateDriver;

        public PlayerJoinStepDefinitions(GameDriver gameDriver, SignalRDriver eventsDriver, StatusDriver stateDriver)
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
            this.stateDriver.CheckPlayerStatus(emitter, PlayerStatusEnum.Alive);
        }

        [Then(@"la partie peut commencer")]
        public void ThenLaPartiePeutCommencer()
        {
            this.signalRDriver.CheckMessages(EventNames.GameReady);
            this.stateDriver.CheckGameStatus(GameStatusEnum.InProgress);
        }
    }
}