using Bang.Core.Admin.Commands;
using Bang.Core.Admin.Models;
using Bang.Models;
using Bang.Tests.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Bang.Tests.Drivers
{
    public class AdminDriver
    {
        private readonly GameContext gameContext;
        private readonly BrowsersContext browsersContext;

        private readonly HttpClientFactoryDriver httpClientFactoryContext;
        private readonly HttpClient client;
        private const string jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJodHRwczovL3Rlc3QuY29tIiwiYXVkIjoiaHR0cHM6Ly90ZXN0LmNvbSIsInJvbGUiOiJJbnRlZ3JhdGlvblRlc3RzIn0.5hBBgFTAgQ2PQozK6l8PPmQjrn55GT8jH91X3S63uHY";

        public AdminDriver(GameContext gameContext, BrowsersContext browsersContext, HttpClientFactoryDriver httpClientFactoryContext)
        {
            this.gameContext = gameContext;
            this.browsersContext = browsersContext;

            this.httpClientFactoryContext = httpClientFactoryContext;
            this.client = this.httpClientFactoryContext.Factory!.CreateClient();
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwt);
        }

        public async Task InitPreparedGameAsync(IEnumerable<(string PlayerName, string CharacterName, string RoleName)> players)
        {
            var characters = await this.client.GetFromJsonAsync<IEnumerable<Character>>("api/characters");
            var roles = await this.client.GetFromJsonAsync<IEnumerable<Role>>("api/roles");

            var request = new CreatePreparedGameCommand(
                players.Select(p => new PlayersInfos
                {
                    Name = p.PlayerName,
                    CharacterId = characters!.Single(c => c.Name == p.CharacterName).Id,
                    RoleId = roles!.Single(r => r.Name == p.RoleName).Id
                })
            );

            var response = await this.client.PostAsJsonAsync("api/admin/games", request);
            response.EnsureSuccessStatusCode();

            var createdGame = response.Headers.Location;
            this.gameContext.Current = await this.client.GetFromJsonAsync<Game>(createdGame);

            this.browsersContext.HttpClients = players.ToDictionary(
                player => player.PlayerName,
                _ => this.httpClientFactoryContext.Factory.CreateClient()
            );
        }

        public async Task ForceCardInPlayerHand(string playerName, string cardName)
        {
            var playerId = this.gameContext.Current.Players.Single(p => p.Name == playerName).Id;
            var cards = this.gameContext.PlayerCardsInHand[playerName];
            var playerHasTheCard = cards.Any(c => c.Name == cardName);

            if (!playerHasTheCard)
            {
                var cardToChangeId = cards.First(c => c.Name != cardName).Id;

                var request = new ChangeCardCommand(playerId, cardToChangeId, cardName);
                var response = await this.client.PostAsJsonAsync("api/admin/cards/change", request);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
