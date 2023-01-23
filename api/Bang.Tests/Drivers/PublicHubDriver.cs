using Bang.Core.Constants;
using Bang.Database.Models;
using Bang.Tests.Contexts;
using Bang.Tests.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Bang.Tests.Drivers
{
    public class PublicHubDriver
    {
        private readonly BrowsersContext browsersContext;
        private readonly GameContext gameContext;
        private readonly HttpClientFactoryContext httpClientFactoryContext;

        private readonly IList<string> messages = new List<string>();

        public PublicHubDriver(BrowsersContext browsersContext, GameContext gameContext, HttpClientFactoryContext httpClientFactoryContext)
        {
            this.browsersContext = browsersContext;
            this.gameContext = gameContext;
            this.httpClientFactoryContext = httpClientFactoryContext;
        }

        public async Task ConnectToHubAsync()
        {
            var server = this.httpClientFactoryContext.Factory.Server;
            var connection = SignalRHelper.ConnectToOpenHub(server, "http://localhost/PublicHub");

            connection.On<Game>(HubMessages.Public.NewGame, game =>
            {
                this.messages.Add(HubMessages.Public.NewGame);
                this.gameContext.Current = game;
            });

            await connection.StartAsync();
        }

        public void CheckMessage(string message) =>
            Assert.Contains(message, this.messages);
    }
}
