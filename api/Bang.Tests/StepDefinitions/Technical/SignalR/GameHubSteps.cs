using Bang.Tests.Drivers.Hubs;
using System.Threading.Tasks;

namespace Bang.Tests.StepDefinitions.Technical.SignalR
{
    [Binding]
    public sealed class GameHubSteps
    {
        private readonly GameHubDriver gameHubDriver;

        public GameHubSteps(GameHubDriver gameHubDriver)
        {
            this.gameHubDriver = gameHubDriver;
        }

        [When(@"le hub du jeu est connecté")]
        public async Task WhenLeHubDuJeuEstConnecte()
        {
            await this.gameHubDriver.ConnectToHubAsync();
            await this.gameHubDriver.SubscribeToMessagesAsync();
        }

        [Then(@"un message ""([^""]*)"" est envoyé au hub du jeu")]
        public void ThenUnMessageEstEnvoyeAuHubDuJeu(string message)
        {
            this.gameHubDriver.CheckMessage(message);
        }
    }
}