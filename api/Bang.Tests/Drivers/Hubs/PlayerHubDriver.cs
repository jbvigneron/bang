using Bang.Core.Constants;
using Bang.Models;
using Bang.Tests.Contexts;
using Bang.Tests.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Bang.Tests.Drivers.Hubs
{
    public class PlayerHubDriver
    {
        private readonly BrowsersContext browsersContext;
        private readonly GameContext gameContext;
        private readonly HttpClientFactoryDriver httpClientFactoryContext;

        private readonly IDictionary<string, IList<string>> messages = new Dictionary<string, IList<string>>();
        private readonly IDictionary<string, HubConnection> connections = new Dictionary<string, HubConnection>();

        public PlayerHubDriver(BrowsersContext browsersContext, GameContext gameContext, HttpClientFactoryDriver httpClientFactoryContext)
        {
            this.browsersContext = browsersContext;
            this.gameContext = gameContext;
            this.httpClientFactoryContext = httpClientFactoryContext;
        }

        public async Task ConnectToHubAsync(string playerName)
        {
            this.messages.Add(playerName, new List<string>());

            var server = this.httpClientFactoryContext.Factory!.Server;
            var cookies = this.browsersContext.Cookies[playerName];
            var connection = HubHelper.ConnectToProtectedHub(server, "http://localhost/PlayerHub", cookies);

            connection.On<IList<Card>>(HubMessages.Player.CardsInHand, cards =>
            {
                this.messages[playerName].Add(HubMessages.Player.CardsInHand);
                this.gameContext.PlayerCardsInHand[playerName] = cards;
            });

            connection.On(HubMessages.Player.YourTurn, () =>
            {
                this.messages[playerName].Add(HubMessages.Player.YourTurn);
            });

            await connection.StartAsync();
            this.connections.Add(playerName, connection);
        }

        public Task SubscribeToMessagesAsync(string playerName) =>
            this.connections[playerName].InvokeAsync("Subscribe");

        public void CheckMessage(string playerName, string message) =>
            HubHelper.CheckMessages(this.messages[playerName], message);
    }
}
