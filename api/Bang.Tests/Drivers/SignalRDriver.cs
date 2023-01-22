using Bang.Core.Constants;
using Bang.Database.Models;
using Bang.Tests.Contexts;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Net.Http.Headers;

namespace Bang.Tests.Drivers
{
    public class SignalRDriver
    {
        private readonly BrowsersContext browsersContext;
        private readonly GameContext gameContext;

        public SignalRDriver(BrowsersContext browserContext, GameContext gameContext)
        {
            this.browsersContext = browserContext;
            this.gameContext = gameContext;
        }

        public async Task ConnectToPublicHub()
        {
            var server = browsersContext.HttpClientFactory.Server;
            var httpClient = server.CreateClient();

            var connection = new HubConnectionBuilder()
                .WithUrl(
                    "http://localhost/PublicHub",
                    options => options.HttpMessageHandlerFactory = _ => server.CreateHandler())
                .Build();

            await connection.StartAsync();

            connection.On<Game>(HubMessages.NewGame, game =>
            {
                this.browsersContext.PublicHubMessages.Add(HubMessages.NewGame);
                this.gameContext.Current = game;
            });

            connection.On<Game>(HubMessages.DeckReady, game =>
            {
                this.browsersContext.PublicHubMessages.Add(HubMessages.DeckReady);
                this.gameContext.Current = game;
            });

            connection.On<Game>(HubMessages.AllPlayerJoined, game =>
            {
                this.browsersContext.PublicHubMessages.Add(HubMessages.AllPlayerJoined);
                this.gameContext.Current = game;
            });

            connection.On<string>(HubMessages.ItsYourTurn, playerName =>
            {
                this.browsersContext.PublicHubMessages.Add(HubMessages.ItsYourTurn);
                this.gameContext.Current.CurrentPlayerName = playerName;
            });
        }

        public async Task ConnectToInGameHub(string playerName)
        {
            var server = browsersContext.HttpClientFactory.Server;
            var httpClient = browsersContext.HttpClients[playerName];

            var connection = new HubConnectionBuilder()
                .WithUrl(
                    "http://localhost/InGameHub",
                    options =>
                    {
                        options.HttpMessageHandlerFactory = _ => server.CreateHandler();

                        options.Headers = this.browsersContext.Cookies[playerName]
                            .ToDictionary(
                            _ => HeaderNames.Cookie,
                            value => value
                        );
                    })
                .Build();

            this.browsersContext.InGameHubMessages.Add(playerName, new List<string>());

            await connection.StartAsync();

            connection.On<Player>(HubMessages.PlayerJoin, player =>
            {
                this.browsersContext.InGameHubMessages[playerName].Add(HubMessages.PlayerJoin);

                for (int i = 0; i < gameContext.Current.Players.Count; i++)
                {
                    if (this.gameContext.Current.Players[i].Id == player.Id)
                    {
                        this.gameContext.Current.Players[i] = player;
                    }
                }
            });
        }

        public void CheckPublicMessage(string message)
        {
            var events = this.browsersContext.PublicHubMessages;
            Assert.Contains(message, events);
        }

        public void CheckInGameMessage(string playerName, string message)
        {
            var events = this.browsersContext.InGameHubMessages[playerName];
            Assert.Contains(message, events);
        }
    }
}
