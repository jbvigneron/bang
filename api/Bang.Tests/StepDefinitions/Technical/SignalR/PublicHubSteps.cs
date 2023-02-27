using Bang.Tests.Drivers;
using Bang.Tests.Drivers.Hubs;

namespace Bang.Tests.StepDefinitions.Technical.SignalR
{
    [Binding]
    public sealed class PublicHubSteps
    {
        private readonly PublicHubDriver publicHubDriver;
        private readonly GameDriver gameDriver;

        private IEnumerable<string>? playerNames;

        public PublicHubSteps(PublicHubDriver publicHubDriver, GameDriver gameDriver)
        {
            this.publicHubDriver = publicHubDriver;
            this.gameDriver = gameDriver;
        }

        [Given(@"ces joueurs souhaitent faire une partie")]
        public void GivenCesJoueursSouhaitentFaireUnePartie(Table table)
        {
            this.playerNames = table.Rows.Select(r => r["playerName"]);
        }

        [When(@"la partie est initialisée")]
        public Task WhenUnePartieEstInitialisee()
        {
            return this.gameDriver.InitGameAsync(this.playerNames!);
        }

        [When(@"le hub public est connecté")]
        public Task WhenLeHubPublicEstConnecte()
        {
            return this.publicHubDriver.ConnectToHubAsync();
        }

        [Then(@"un message ""([^""]*)"" est envoyé au hub public")]
        public void ThenUnMessageAuHubPublic(string message)
        {
            this.publicHubDriver.CheckMessage(message);
        }
    }
}