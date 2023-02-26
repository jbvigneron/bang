using Bang.Core.Constants;
using Bang.Models;
using Bang.Tests.Contexts;
using Bang.Tests.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Bang.Tests.Drivers.Hubs
{
    public class PublicHubDriver
    {
        private readonly GameContext gameContext;
        private readonly HttpClientFactoryDriver httpClientFactoryContext;

        private HubConnection? connection;
        private readonly IList<string> messages = new List<string>();

        public PublicHubDriver(GameContext gameContext, HttpClientFactoryDriver httpClientFactoryContext)
        {
            this.gameContext = gameContext;
            this.httpClientFactoryContext = httpClientFactoryContext;
        }

        public async Task ConnectToHubAsync()
        {
            var server = this.httpClientFactoryContext.Factory!.Server;
            this.connection = HubHelper.ConnectToOpenHub(server, "http://localhost/PublicHub");

            this.connection.On<Game>(HubMessages.Public.GameCreated, game =>
            {
                messages.Add(HubMessages.Public.GameCreated);
                gameContext.Current = game;
            });

            await this.connection.StartAsync();
        }

        public void CheckMessage(string message) =>
            HubHelper.CheckMessages(this.messages, message);
    }
}
