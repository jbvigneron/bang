using Bang.Core.Constants;
using Bang.Database.Models;
using Bang.Tests.Contexts;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Net.Http.Headers;

namespace Bang.Tests.Drivers
{
    public class SignalRDriver
    {
        private readonly BrowserContext browserContext;
        private readonly GameContext gameContext;

        public SignalRDriver(BrowserContext browserContext, GameContext gameContext)
        {
            this.browserContext = browserContext;
            this.gameContext = gameContext;
        }

        public async Task ListenGameHub(string playerName)
        {
            var server = browserContext.HttpClientFactory.Server;
            var httpClient = browserContext.HttpClients[playerName];

            var connection = new HubConnectionBuilder()
                .WithUrl(
                    "http://localhost/GameHub",
                    options =>
                    {
                        options.HttpMessageHandlerFactory = _ => server.CreateHandler();

                        options.Headers = this.browserContext.Cookies[playerName]
                            .ToDictionary(
                            _ => HeaderNames.Cookie,
                            value => value
                        );
                    })
                .Build();

            this.browserContext.SignalRMessages.Add(playerName, new List<string>());

            await connection.StartAsync();

            connection.On<Player>(EventNames.PlayerReady, player =>
            {
                this.browserContext.SignalRMessages[playerName].Add(EventNames.PlayerReady);

                for (int i = 0; i < gameContext.Current.Players.Count; i++)
                {
                    if (this.gameContext.Current.Players[i].Id == player.Id)
                    {
                        this.gameContext.Current.Players[i] = player;
                    }
                }
            });

            connection.On<Game>(EventNames.GameReady, g =>
            {
                this.browserContext.SignalRMessages[playerName].Add(EventNames.GameReady);
                this.gameContext.Current = g;
            });
        }

        public void CheckPlayerMessages(string playerName, string message, int count)
        {
            var events = this.browserContext.SignalRMessages[playerName];
            Assert.Equal(count, events.Count(e => e == message));
        }

        public void CheckMessages(string message)
        {
            var events = this.browserContext.SignalRMessages.Values.SelectMany(e => e);
            Assert.Contains(events, e => e == message);
        }
    }
}
