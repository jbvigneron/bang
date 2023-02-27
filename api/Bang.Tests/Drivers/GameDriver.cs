using Bang.Models;
using Bang.Tests.Contexts;
using Bang.WebApi.Enums;
using Bang.WebApi.Models;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Bang.Tests.Drivers
{
    public class GameDriver
    {
        private readonly BrowsersContext browsersContext;
        private readonly GameContext gameContext;
        private readonly HttpClientFactoryDriver httpClientFactoryContext;

        public GameDriver(BrowsersContext browserContext, GameContext gameContext, HttpClientFactoryDriver httpClientFactoryContext)
        {
            this.browsersContext = browserContext;
            this.gameContext = gameContext;
            this.httpClientFactoryContext = httpClientFactoryContext;
        }

        public async Task InitGameAsync(IEnumerable<string> playerNames)
        {
            var client = this.httpClientFactoryContext.Factory!.CreateClient();
            var response = await client.PostAsJsonAsync("api/games", playerNames);
            response.EnsureSuccessStatusCode();

            var createdGame = response.Headers.Location;
            this.gameContext.Current = await client.GetFromJsonAsync<Game>(createdGame!);

            this.browsersContext.HttpClients = playerNames.ToDictionary(
                name => name,
                _ => this.httpClientFactoryContext.Factory.CreateClient()
            );
        }

        public async Task JoinGameAsync(string playerName, AuthMode? authMode = AuthMode.Cookie)
        {
            var gameId = this.gameContext.Current!.Id;

            var client = this.browsersContext.HttpClients![playerName];
            var response = await client.PostAsJsonAsync($"api/games/{gameId}?authMode={authMode}", playerName);
            response.EnsureSuccessStatusCode();

            if (authMode == AuthMode.Jwt)
            {
                var jwt = await response.Content.ReadAsStringAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }
            else
            {
                IEnumerable<string> cookies = response.Headers.GetValues(HeaderNames.SetCookie);
                this.browsersContext.Cookies[playerName] = cookies!;
            }
        }

        public async Task AllJoinGameAsync()
        {
            foreach (var playerName in this.browsersContext.HttpClients!.Keys)
            {
                await this.JoinGameAsync(playerName);
            }
        }

        public async Task DrawCardsAsync(string playerName)
        {
            var client = this.browsersContext.HttpClients![playerName];
            var response = await client.PostAsync("api/cards/draw", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task PlayCardAsync(string playerName, string cardName)
        {
            var cards = this.gameContext.PlayerCardsInHand[playerName];
            var card = cards.First(b => b.Name == cardName);

            var client = this.browsersContext.HttpClients![playerName];
            var request = new PlayCardRequest(card.Id);
            var response = await client.PostAsJsonAsync("api/cards/play", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task PlayCardAsync(string playerName, string cardName, string opponentName)
        {
            var cards = this.gameContext.PlayerCardsInHand[playerName];
            var card = cards.First(b => b.Name == cardName);

            var opponent = this.gameContext.Current!.Players!.FirstOrDefault(p => p.Name == opponentName);

            var client = this.browsersContext.HttpClients![playerName];
            var request = new PlayCardRequest(card.Id, opponent!.Id);
            var response = await client.PostAsJsonAsync("api/cards/play", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task PlayRandomCardAsync(string playerName)
        {
            var cards = this.gameContext.PlayerCardsInHand[playerName];
            var card = cards.OrderBy(c => Guid.NewGuid()).First(c => !c.RequireOpponent);

            var client = this.browsersContext.HttpClients![playerName];
            var request = new PlayCardRequest(card.Id);
            var response = await client.PostAsJsonAsync("api/cards/play", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateGameAsync()
        {
            var gameId = this.gameContext.Current!.Id;
            var client = this.httpClientFactoryContext.Factory!.CreateClient();
            this.gameContext.Current = await client.GetFromJsonAsync<Game>($"api/games/{gameId}");
        }

        public async Task UpdatePlayerAsync(string playerName)
        {
            var client = this.browsersContext.HttpClients![playerName];
            var player = await client.GetFromJsonAsync<Player>("api/players/me");
            this.gameContext.Players![playerName] = player!;
        }

        public async Task UpdatePlayerCardsAsync(string playerName)
        {
            var client = this.browsersContext.HttpClients![playerName];
            var cards = await client.GetFromJsonAsync<IList<Card>>("api/cards/mine");
            this.gameContext.PlayerCardsInHand[playerName] = cards!;
        }
        public void IsPlayerExisting(string playerName) =>
            Assert.True(this.gameContext.Players!.ContainsKey(playerName));
    }
}
