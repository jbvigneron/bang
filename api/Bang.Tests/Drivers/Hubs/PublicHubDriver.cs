using Bang.Core.Constants;
using Bang.Models;
using Bang.Tests.Contexts;
using Bang.Tests.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Bang.Tests.Drivers.Hubs
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
            var server = httpClientFactoryContext.Factory.Server;
            var connection = SignalRHelper.ConnectToOpenHub(server, "http://localhost/PublicHub");

            connection.On<Game>(HubMessages.Public.NewGame, game =>
            {
                messages.Add(HubMessages.Public.NewGame);
                gameContext.Current = game;
            });

            await connection.StartAsync();
        }

        public void CheckMessage(string message) =>
            Assert.Contains(message, messages);
    }
}
