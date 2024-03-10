using Bang.Domain.Enums;
using Bang.Tests.Drivers;
using Bang.Tests.Drivers.Hubs;
using System.Threading.Tasks;

namespace Bang.Tests.StepDefinitions.Technical.SignalR
{
    [Binding]
    public sealed class PlayerHubSteps
    {
        private readonly PlayerHubDriver playerHubDriver;
        private readonly StateDriver stateDriver;

        public PlayerHubSteps(PlayerHubDriver playerHubDriver, StateDriver stateDriver)
        {
            this.playerHubDriver = playerHubDriver;
            this.stateDriver = stateDriver;
        }

        [When(@"le hub de ""([^""]*)"" est connecté")]
        public async Task WhenLeHubDeEstConnecte(string playerName)
        {
            await this.playerHubDriver.ConnectToHubAsync(playerName);
            await this.playerHubDriver.SubscribeToMessagesAsync(playerName);
        }

        [Then(@"un message ""([^""]*)"" est envoyé à ""([^""]*)""")]
        public void ThenUnMessageEstEnvoyeA(string message, string playerName)
        {
            this.playerHubDriver.CheckMessage(playerName, message);
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

        [Then(@"c'est au tour de ""([^""]*)""")]
        public void ThenCestAuTourDe(string playerName)
        {
            this.stateDriver.CheckIsPlayerTurn(playerName);
        }
    }
}