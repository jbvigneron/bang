using Bang.Database.Models;
using Bang.Tests.Contexts;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Json;

namespace Bang.Tests.Drivers
{
    public class GameDriver
    {
        private readonly BrowsersContext browserContext;
        private readonly GameContext gameContext;

        public GameDriver(BrowsersContext browserContext, GameContext gameContext)
        {
            this.browserContext = browserContext;
            this.gameContext = gameContext;
        }

        public async Task InitGameAsync(IEnumerable<string> playerNames)
        {
            var client = this.browserContext.HttpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync("api/game", playerNames);
            response.EnsureSuccessStatusCode();

            var gameId = await response.Content.ReadFromJsonAsync<Guid>();
            this.gameContext.Current = await client.GetFromJsonAsync<Game>($"api/game/{gameId}");

            browserContext.HttpClients = playerNames.ToDictionary(
                name => name,
                _ => browserContext.HttpClientFactory.CreateClient()
            );
        }

        public async Task JoinGameAsync(string playerName)
        {
            var gameId = this.gameContext.Current.Id;
            var client = this.browserContext.HttpClients[playerName];
            var result = await client.PostAsJsonAsync($"api/game/{gameId}", playerName);
            result.EnsureSuccessStatusCode();

            result.Headers.TryGetValues(HeaderNames.SetCookie, out IEnumerable<string> cookies);
            this.browserContext.Cookies[playerName] = cookies.ToList();
        }

        public async Task UpdateGameAsync()
        {
            var gameId = this.gameContext.Current.Id;
            var client = this.browserContext.HttpClientFactory.CreateClient();
            this.gameContext.Current = await client.GetFromJsonAsync<Game>($"api/game/{gameId}");
        }

        public Task<Player> GetPlayerInfoAsync(string playerName)
        {
            var client = this.browserContext.HttpClients[playerName];
            return client.GetFromJsonAsync<Player>("api/player/me");
        }
    }
}
