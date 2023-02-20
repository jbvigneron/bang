using Bang.Core.Extensions;
using Bang.Models;
using Bang.Tests.Contexts;
using Bang.WebApi.Models;
using Bang.WebApi.Requests;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Json;
using TechTalk.SpecFlow.CommonModels;

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

            var request = new CreateGameRequest(playerNames);
            var response = await client.PostAsJsonAsync("api/games", request);
            response.EnsureSuccessStatusCode();

            var createdGame = response.Headers.Location;
            this.gameContext.Current = await client.GetFromJsonAsync<Game>(createdGame!);

            browserContext.HttpClients = playerNames.ToDictionary(
                name => name,
                _ => this.httpClientFactoryContext.Factory.CreateClient()
            );
        }

        public async Task InitPreparedGameAsync(IEnumerable<(string PlayerName, string CharacterName, string RoleName)> players)
        {
            var client = this.httpClientFactoryContext.Factory!.CreateClient();

            var characters = await client.GetFromJsonAsync<IEnumerable<Character>>("api/characters");
            var roles = await client.GetFromJsonAsync<IEnumerable<Role>>("api/roles");

            var request = new CreateGameRequest(
                players.Select(p => new CreateGameRequest.PlayersInfos
                {
                    Name = p.PlayerName,
                    CharacterId = characters!.Single(c => c.Name == p.CharacterName).Id,
                    RoleId = roles!.Single(r => r.Name == p.RoleName).Id
                })
            );

            var response = await client.PostAsJsonAsync("api/games", request);
            response.EnsureSuccessStatusCode();

            var createdGame = response.Headers.Location;
            this.gameContext.Current = await client.GetFromJsonAsync<Game>(createdGame!);

            browserContext.HttpClients = players.ToDictionary(
                player => player.PlayerName,
                _ => this.httpClientFactoryContext.Factory.CreateClient()
            );
        }

        public async Task JoinGameAsync(string playerName)
        {
            var gameId = this.gameContext.Current.Id;

            var client = this.browserContext.HttpClients![playerName];
            var result = await client.PostAsJsonAsync($"api/games/{gameId}", playerName);
            result.EnsureSuccessStatusCode();

            result.Headers.TryGetValues(HeaderNames.SetCookie, out IEnumerable<string> cookies);
            this.browserContext.Cookies[playerName] = cookies!;
        }

        public async Task DrawCardsAsync(string playerName)
        {
            var client = this.browserContext.HttpClients![playerName];
            var result = await client.PostAsync("api/cards/draw", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task CheckAndSwitchCardAsync(string playerName, string newCardName)
        {
            var cards = this.gameContext.CardsInHand[playerName];
            var playerHasTheCard = cards.Any(c => c.Name == newCardName);

            if (!playerHasTheCard)
            {
                var cardToSwitch = cards.First(c => c.Name != newCardName);

                var client = this.browserContext.HttpClients![playerName];
                var request = new SwitchCardRequest(cardToSwitch, newCardName);
                var result = await client.PostAsJsonAsync("api/cards/switch", request);
                result.EnsureSuccessStatusCode();
            }
        }

        public async Task PlayCardAsync(string playerName, string cardName)
        {
            var cards = this.gameContext.CardsInHand[playerName];
            var card = cards.First(b => b.Name == cardName);

            var client = this.browserContext.HttpClients![playerName];
            var request = new PlayCardRequest(card);
            var result = await client.PostAsJsonAsync("api/cards/play", request);
            result.EnsureSuccessStatusCode();
        }

        public async Task PlayCardAsync(string playerName, string cardName, string opponentName)
        {
            var cards = this.gameContext.CardsInHand[playerName];
            var card = cards.First(b => b.Name == cardName);

            var opponent = this.gameContext.Current.Players.First(p => p.Name == opponentName);

            var client = this.browserContext.HttpClients![playerName];
            var request = new PlayCardRequest(card, opponent.Id);
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
            var client = this.browserContext.HttpClients![playerName];
            var cards = await client.GetFromJsonAsync<IList<Card>>("api/cards/mine");
            this.gameContext.CardsInHand[playerName] = cards!;
        }
    }
}
