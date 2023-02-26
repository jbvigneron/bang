using Bang.Core.Extensions;
using Bang.Models;
using Bang.Tests.Contexts;
using Bang.WebApi.Models;
using Microsoft.Net.Http.Headers;
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

        public async Task JoinGameAsync(string playerName)
        {
            var gameId = this.gameContext.Current.Id;

            var client = this.browsersContext.HttpClients![playerName];
            var result = await client.PostAsJsonAsync($"api/games/{gameId}", playerName);
            result.EnsureSuccessStatusCode();

            result.Headers.TryGetValues(HeaderNames.SetCookie, out IEnumerable<string> cookies);
            this.browsersContext.Cookies[playerName] = cookies!;
        }

        public async Task AllJoinGameAsync()
        {
            var gameId = this.gameContext.Current.Id;

            foreach (var client in this.browsersContext.HttpClients)
            {
                var result = await client.Value.PostAsJsonAsync($"api/games/{gameId}", client.Key);
                result.EnsureSuccessStatusCode();

                result.Headers.TryGetValues(HeaderNames.SetCookie, out IEnumerable<string> cookies);
                this.browsersContext.Cookies[client.Key] = cookies!;
            }
        }

        public async Task DrawCardsAsync(string playerName)
        {
            var client = this.browsersContext.HttpClients![playerName];
            var result = await client.PostAsync("api/cards/draw", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task PlayCardAsync(string playerName, string cardName)
        {
            var cards = this.gameContext.CardsInHand[playerName];
            var card = cards.First(b => b.Name == cardName);

            var client = this.browsersContext.HttpClients![playerName];
            var request = new PlayCardRequest(card.Id);
            var result = await client.PostAsJsonAsync("api/cards/play", request);
            result.EnsureSuccessStatusCode();
        }

        public async Task PlayCardAsync(string playerName, string cardName, string opponentName)
        {
            var cards = this.gameContext.CardsInHand[playerName];
            var card = cards.First(b => b.Name == cardName);

            var opponent = this.gameContext.Current.Players.FirstOrDefault(p => p.Name == opponentName);

            var client = this.browsersContext.HttpClients![playerName];
            var request = new PlayCardRequest(card.Id, opponent.Id);
            var result = await client.PostAsJsonAsync("api/cards/play", request);
            result.EnsureSuccessStatusCode();
        }

        public async Task PlayRandomCardAsync(string playerName)
        {
            var cards = this.gameContext.CardsInHand[playerName];
            var card = cards.OrderBy(c => Guid.NewGuid()).First(c => !c.RequireOpponent);

            var client = this.browsersContext.HttpClients![playerName];
            var request = new PlayCardRequest(card.Id);
            var result = await client.PostAsJsonAsync("api/cards/play", request);
            result.EnsureSuccessStatusCode();
        }

        public string GetSheriffName()
        {
            return this.gameContext.Current.GetSheriff().Name;
        }

        public async Task UpdateGameAsync()
        {
            var gameId = this.gameContext.Current.Id;
            var client = this.httpClientFactoryContext.Factory!.CreateClient();
            this.gameContext.Current = await client.GetFromJsonAsync<Game>($"api/games/{gameId}");
        }

        public async Task UpdatePlayerCardsAsync(string playerName)
        {
            var client = this.browsersContext.HttpClients![playerName];
            var cards = await client.GetFromJsonAsync<IList<Card>>("api/cards/mine");
            this.gameContext.CardsInHand[playerName] = cards!;
        }
    }
}
