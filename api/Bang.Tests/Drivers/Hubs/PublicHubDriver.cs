using Bang.Domain.Constants;
using Bang.Domain.Entities;
using Bang.Tests.Contexts;
using Bang.Tests.Helpers;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bang.Tests.Drivers.Hubs
{
    public class PublicHubDriver
    {
        private readonly GameContext gameContext;
        private readonly HttpClientFactoryDriver httpClientFactoryContext;

        private readonly IList<string> messages = new List<string>();

        public PublicHubDriver(GameContext gameContext, HttpClientFactoryDriver httpClientFactoryContext)
        {
            this.gameContext = gameContext;
            this.httpClientFactoryContext = httpClientFactoryContext;
        }

        public async Task ConnectToHubAsync()
        {
            var server = this.httpClientFactoryContext.Factory.Server;
            var connection = HubHelper.ConnectToOpenHub(server, "http://localhost/PublicHub");

            connection.On<CurrentGame>(Events.Public.GameCreated, game =>
            {
                this.messages.Add(Events.Public.GameCreated);
                this.gameContext.Current = game;
            });

            await connection.StartAsync();
        }

        public void CheckMessage(string message) =>
            HubHelper.CheckMessages(this.messages, message);
    }
}