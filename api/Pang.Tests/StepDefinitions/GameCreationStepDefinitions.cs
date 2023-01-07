using Pang.Database.Models;
using Pang.Tests.Helpers;
using System.Net.Http.Json;

namespace Pang.Tests.StepDefinitions
{
    [Binding]
    public sealed class GameCreationStepDefinitions
    {
        private readonly HttpClient httpClient;
        private readonly GameContext context;

        public GameCreationStepDefinitions(TestWebApplicationFactory<Program> factory, GameContext context)
        {
            this.httpClient = factory.CreateClient();
            this.context = context;
        }

        [Given(@"ces joueurs veulent lancer une partie")]
        public void GivenLesJoueursSuivantsVeulentJouer(Table table)
        {
            this.context.PlayerNames = table.Rows.Select(r => r["playerName"]);
        }

        [When(@"la partie s'initialise")]
        public async Task WhenJinitialiseLaPartie()
        {
            var response = await this.httpClient.PostAsJsonAsync("api/game/create", this.context.PlayerNames);
            response.EnsureSuccessStatusCode();

            var game = await response.Content.ReadFromJsonAsync<Game>();
            this.context.CurrentGame = game;
        }

        [Then(@"la composition des rôles est la suivante")]
        public void ThenJaiLaCompositionSuivante(Table table)
        {
            var players = this.context.CurrentGame.Players;

            foreach (var row in table.Rows)
            {
                var role = Enum.Parse<PlayerRole>(row["roleName"]);
                var expectedCount = int.Parse(row["count"]);

                var actualCount = players.Count(p => p.Role == role);
                Assert.Equal(expectedCount, actualCount);
            }
        }
    }
}