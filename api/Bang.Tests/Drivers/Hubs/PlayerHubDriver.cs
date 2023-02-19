﻿using Bang.Core.Constants;
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
        private readonly HttpClientFactoryContext httpClientFactoryContext;

        private readonly IDictionary<string, IList<string>> messages = new Dictionary<string, IList<string>>();
        private readonly IDictionary<string, HubConnection> connections = new Dictionary<string, HubConnection>();

        public PlayerHubDriver(BrowsersContext browserContext, GameContext gameContext, HttpClientFactoryContext httpClientFactoryContext)
        {
            browsersContext = browserContext;
            this.gameContext = gameContext;
            this.httpClientFactoryContext = httpClientFactoryContext;
        }

        public async Task ConnectToHubAsync(string playerName)
        {
            messages.Add(playerName, new List<string>());

            var server = httpClientFactoryContext.Factory.Server;
            var cookies = browsersContext.Cookies[playerName];
            var connection = SignalRHelper.ConnectToProtectedHub(server, "http://localhost/PlayerHub", cookies);

            connection.On<IList<Card>>(HubMessages.Player.DeckReady, cards =>
            {
                messages[playerName].Add(HubMessages.Player.DeckReady);
                gameContext.PlayerCards[playerName] = cards;
            });

            connection.On(HubMessages.Player.YourTurn, () =>
            {
                messages[playerName].Add(HubMessages.Player.YourTurn);
            });

            connection.On<IList<Card>>(HubMessages.Player.NewCards, cards =>
            {
                messages[playerName].Add(HubMessages.Player.NewCards);

                foreach (var card in cards)
                {
                    gameContext.PlayerCards[playerName].Add(card);
                }
            });

            await connection.StartAsync();
            connections.Add(playerName, connection);
        }

        public Task SubscribeToMessagesAsync(string playerName) =>
            connections[playerName].InvokeAsync("Subscribe");

        public async Task CheckMessageAsync(string playerName, string message)
        {
            await Task.Delay(1500);
            Assert.Contains(message, messages[playerName]);
        }
    }
}