using Bang.Tests.Drivers;
using Bang.Tests.Drivers.Technical.Hubs;

namespace Bang.Tests.StepDefinitions.Technical.SignalR
{
    [Binding]
    public sealed class PlayerHubSteps
    {
        private readonly GameDriver gameDriver;
        private readonly PlayerHubDriver playerHubDriver;

        public PlayerHubSteps(
            GameDriver gameDriver,
            PlayerHubDriver playerHubDriver)
        {
            this.gameDriver = gameDriver;
            this.playerHubDriver = playerHubDriver;
        }

        [When(@"le hub du jeu du shérif est connecté")]
        public async Task WhenLeHubDuJeuDuSherifEstConnecte()
        {
            var sheriffName = gameDriver.GetSheriffName();
            await this.playerHubDriver.ConnectToHubAsync(sheriffName);
            await this.playerHubDriver.SubscribeToMessagesAsync(sheriffName);
        }

        [When(@"le hub de ""([^""]*)"" est connecté")]
        public async Task WhenLeHubDeEstConnecte(string playerName)
        {
            await this.playerHubDriver.ConnectToHubAsync(playerName);
            await this.playerHubDriver.SubscribeToMessagesAsync(playerName);
        }

        [Then(@"un message ""([^""]*)"" est envoyé au shérif")]
        public void ThenUnMessageEstEnvoyeAuScherif(string message)
        {
            var sheriffName = gameDriver.GetSheriffName();
            this.playerHubDriver.CheckMessage(sheriffName, message);
        }

        [Then(@"un message ""([^""]*)"" est envoyé à ""([^""]*)""")]
        public void ThenUnMessageEstEnvoyeA(string message, string playerName)
        {
            this.playerHubDriver.CheckMessage(playerName, message);
        }
    }
}