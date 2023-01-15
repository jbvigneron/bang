using Bang.Core.Extensions;
using Bang.Database.Models;
using System.Net.Http.Json;

namespace Bang.Tests.StepDefinitions
{
    [Binding]
    public sealed class GameInitStepDefinitions
    {
        private readonly GameContext context;

        public GameInitStepDefinitions(GameContext context)
        {
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
            var response = await this.context.HttpClient.PostAsJsonAsync("api/game/create", this.context.PlayerNames);
            response.EnsureSuccessStatusCode();

            var game = await response.Content.ReadFromJsonAsync<Game>();
            this.context.CurrentGame = game;
        }

        [Then(@"il y a un shérif")]
        public void ThenIlYAUnSherif()
        {
            var game = this.context.CurrentGame;
            var sheriff = game.GetScheriff();
            Assert.NotNull(sheriff);
        }

        [Then(@"il y a (.*) autres personnes")]
        public void ThenIlYANAutresPersonnes(int count)
        {
            var game = this.context.CurrentGame;
            var others = game.Players.Where(p => !p.IsScheriff);
            Assert.Equal(count, others.Count());
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

        [Then(@"le schérif dévoile sa carte")]
        public void ThenLeScherifDevoileSaCarte()
        {
            var game = this.context.CurrentGame;
            var sheriff = game.GetScheriff();
            Assert.True(sheriff.IsScheriff);
        }

        [Then(@"le schérif possède une balle supplémentaire")]
        public void ThenLeScherifPossedeUneBalleSupplementaire()
        {
            var game = this.context.CurrentGame;
            var sheriff = game.GetScheriff();
            Assert.Equal(1, sheriff.Lives);
        }

    }
}