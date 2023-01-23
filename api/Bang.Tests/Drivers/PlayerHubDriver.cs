using Bang.Core.Constants;
using Bang.Database.Models;
using Bang.Tests.Contexts;
using Bang.Tests.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Bang.Tests.Drivers
{
    public class PlayerHubDriver
    {
        private readonly BrowsersContext browsersContext;
        private readonly GameContext gameContext;
        private readonly HttpClientFactoryContext httpClientFactoryContext;

        private readonly IDictionary<string, IList<string>> messages = new Dictionary<string, IList<string>>();
        private readonly IDictionary<string, HubConnection> connections = new Dictionary<string, HubConnection>();

        public PlayerHubDriver(BrowsersContext browserContext, GameContext gameContext, HttpClientFactoryContext httpClientFactoryContext)
        {
            this.browsersContext = browserContext;
            this.gameContext = gameContext;
            this.httpClientFactoryContext = httpClientFactoryContext;
        }

        public async Task ConnectToHubAsync(string playerName)
        {
            this.messages.Add(playerName, new List<string>());

            var server = this.httpClientFactoryContext.Factory.Server;
            var cookies = this.browsersContext.Cookies[playerName];
            var connection = SignalRHelper.ConnectToProtectedHub(server, "http://localhost/PlayerHub", cookies);

            connection.On<IList<Card>>(HubMessages.Player.PlayerDeckReady, cards =>
            {
                this.messages[playerName].Add(HubMessages.Player.PlayerDeckReady);
                this.gameContext.PlayerCards[playerName] = cards;
            });

            await connection.StartAsync();
            this.connections.Add(playerName, connection);
        }

        public Task SubscribeToMessagesAsync(string playerName) =>
            this.connections[playerName].InvokeAsync("Subscribe");

        public void CheckMessage(string playerName, string message) =>
            Assert.Contains(message, this.messages[playerName]);
    }
}
