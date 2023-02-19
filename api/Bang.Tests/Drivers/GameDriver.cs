using Bang.Core.Extensions;
using Bang.Models;
using Bang.Tests.Contexts;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Json;

namespace Bang.Tests.Drivers
{
    public class GameDriver
    {
        private readonly BrowsersContext browserContext;
        private readonly GameContext gameContext;
        private readonly HttpClientFactoryContext httpClientFactoryContext;

        public GameDriver(BrowsersContext browserContext, GameContext gameContext, HttpClientFactoryContext httpClientFactoryContext)
        {
            this.browserContext = browserContext;
            this.gameContext = gameContext;
            this.httpClientFactoryContext = httpClientFactoryContext;
        }

        public async Task InitGameAsync(IEnumerable<string> playerNames)
        {
            var client = this.httpClientFactoryContext.Factory!.CreateClient();

            var response = await client.PostAsJsonAsync("api/game", playerNames);
            response.EnsureSuccessStatusCode();

            var gameId = await response.Content.ReadFromJsonAsync<Guid>();
            this.gameContext.Current = await client.GetFromJsonAsync<Game>($"api/game/{gameId}");

            browserContext.HttpClients = playerNames.ToDictionary(
                name => name,
                _ => this.httpClientFactoryContext.Factory.CreateClient()
            );
        }

        public async Task JoinGameAsync(string playerName)
        {
            var gameId = this.gameContext.Current.Id;
            var client = this.browserContext.HttpClients[playerName];
            var result = await client.PostAsJsonAsync($"api/game/{gameId}", playerName);
            result.EnsureSuccessStatusCode();

            result.Headers.TryGetValues(HeaderNames.SetCookie, out IEnumerable<string> cookies);
            this.browserContext.Cookies[playerName] = cookies;
        }

        public async Task DrawCardsAsync(string playerName)
        {
            var client = this.browserContext.HttpClients[playerName];
            var result = await client.PostAsync($"api/player/cards/draw", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task UpdateGameAsync()
        {
            var gameId = this.gameContext.Current.Id;
            var client = this.httpClientFactoryContext.Factory!.CreateClient();
            this.gameContext.Current = await client.GetFromJsonAsync<Game>($"api/game/{gameId}");
        }

        public async Task UpdatePlayerCardsAsync(string playerName)
        {
            var client = this.browserContext.HttpClients[playerName];
            var cards = await client.GetFromJsonAsync<IList<Card>>("api/player/cards");
            this.gameContext.PlayerCards[playerName] = cards;
        }

        public string GetSheriffName()
        {
            return this.gameContext.Current.GetSheriff().Name;
        }
    }
}
