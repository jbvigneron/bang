using Bang.Core.Constants;
using Bang.Core.Extensions;
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

            var connection = this.ConnectToOpenHub("http://localhost/PublicHub");
            await connection.StartAsync();

            connection.On<Game>(HubMessages.NewGame, game =>
            {
                this.browsersContext.PublicHubMessages.Add(HubMessages.NewGame);
                this.gameContext.Current = game;
            });

            connection.On<Game>(HubMessages.GameDeckReady, game =>
            {
                this.browsersContext.PublicHubMessages.Add(HubMessages.GameDeckReady);
                this.gameContext.Current = game;
            });

            connection.On<Game>(HubMessages.AllPlayerJoined, game =>
            {
                this.browsersContext.PublicHubMessages.Add(HubMessages.AllPlayerJoined);
                this.gameContext.Current = game;
            });
        }

        public async Task ConnectToGameHub(string playerName)
        {
            var server = browsersContext.HttpClientFactory.Server;
            var httpClient = browsersContext.HttpClients[playerName];

            this.browsersContext.GameHubMessages.Add(playerName, new List<string>());

            var connection = this.ConnectToProtectedHub("http://localhost/GameHub", playerName);
            await connection.StartAsync();

            connection.On<Player>(HubMessages.PlayerJoin, player =>
            {
                this.browsersContext.GameHubMessages[playerName].Add(HubMessages.PlayerJoin);

                for (int i = 0; i < gameContext.Current.Players.Count; i++)
                {
                    if (this.gameContext.Current.Players[i].Id == player.Id)
                    {
                        this.gameContext.Current.Players[i] = player;
                    }
                }
            });
        }

        public async Task ConnectToPlayerHub(string playerName)
        {
            var server = browsersContext.HttpClientFactory.Server;
            var httpClient = browsersContext.HttpClients[playerName];

            this.browsersContext.PlayerHubMessages.Add(playerName, new List<string>());

            var connection = this.ConnectToProtectedHub("http://localhost/PlayerHub", playerName);
            await connection.StartAsync();

            connection.On<string>(HubMessages.ItsYourTurn, name =>
            {
                this.browsersContext.PlayerHubMessages[playerName].Add(HubMessages.ItsYourTurn);
                this.gameContext.Current.CurrentPlayerName = name;
            });

            connection.On<IList<Card>>(HubMessages.PlayerDeckReady, cards =>
            {
                this.browsersContext.PlayerHubMessages[playerName].Add(HubMessages.PlayerDeckReady);
                this.gameContext.PlayerCards[playerName] = cards;
            });
        }

        public void CheckPublicMessage(string message)
        {
            var events = this.browsersContext.PublicHubMessages;
            Assert.Contains(message, events);
        }

        public void CheckGameMessage(string playerName, string message)
        {
            var events = this.browsersContext.GameHubMessages[playerName];
            Assert.Contains(message, events);
        }

        public void CheckPlayerMessage(string playerName, string message)
        {
            var events = this.browsersContext.GameHubMessages[playerName];
            Assert.Contains(message, events);
        }

        public void CheckScheriffMessage(string message)
        {
            var scheriff = this.gameContext.Current.GetScheriff();
            var events = this.browsersContext.PlayerHubMessages[scheriff.Name];
            Assert.Contains(message, events);
        }

        private HubConnection ConnectToOpenHub(string url)
        {
            var server = browsersContext.HttpClientFactory.Server;

            return new HubConnectionBuilder()
                .WithUrl(url, options => options.HttpMessageHandlerFactory = _ => server.CreateHandler())
                .Build();
        }

        private HubConnection ConnectToProtectedHub(string url, string playerName)
        {
            var server = browsersContext.HttpClientFactory.Server;

            return new HubConnectionBuilder()
                .WithUrl(url, options =>
                {
                    options.HttpMessageHandlerFactory = _ => server.CreateHandler();

                    options.Headers = this.browsersContext.Cookies[playerName]
                        .ToDictionary(
                        _ => HeaderNames.Cookie,
                        value => value
                    );
                })
                .Build();
        }
    }
}
