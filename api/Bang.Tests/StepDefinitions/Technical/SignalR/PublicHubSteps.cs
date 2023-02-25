using Bang.Tests.Drivers.Technical.Hubs;

namespace Bang.Tests.StepDefinitions.Technical.SignalR
{
    [Binding]
    public sealed class PublicHubSteps
    {
        private readonly PublicHubDriver publicHubDriver;

        public PublicHubSteps(PublicHubDriver publicHubDriver)
        {
            this.publicHubDriver = publicHubDriver;
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