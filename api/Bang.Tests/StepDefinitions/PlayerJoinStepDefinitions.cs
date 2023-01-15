using Bang.Database.Models;
using Bang.Tests.Helpers;
using System.Net.Http.Json;

namespace Bang.Tests.StepDefinitions
{
    [Binding]
    public sealed class PlayerJoinStepDefinitions
    {
        private readonly GameContext context;

        public PlayerJoinStepDefinitions(TestWebApplicationFactory<Program> factory, GameContext context)
        {
            this.context = context;
        }

        [When(@"""([^""]*)"" veut rejoindre la partie")]
        public async Task WhenVeutRejoindreLaPartie(string playerName)
        {
            var gameId = this.context.CurrentGame.Id;
            var result = await this.context.HttpClient.PostAsJsonAsync($"api/game/join/{gameId}", playerName);
            result.EnsureSuccessStatusCode();

            this.context.CurrentPlayer = await result.Content.ReadFromJsonAsync<Player>();
        }

        [Then(@"un personnage lui est attribué")]
        public void ThenUnPersonnageLuiEstAttribue()
        {
            var player = this.context.CurrentPlayer;
            Assert.NotNull(player.Character);
        }

        [Then(@"son nombre de vies lui est attribué selon son personnage")]
        public void ThenSonNombreDeViesLuiEstAttribueSelonSonPersonnage(Table table)
        {
            var player = this.context.CurrentPlayer;
            var lives = int.Parse(table.Rows.Single(r => r["characterName"] == player.Character.Name)["lives"]);

            if (player.IsScheriff)
            {
                Assert.Equal(lives + 1, player.Lives);
            }
            else
            {
                Assert.Equal(lives, player.Lives);
            }
        }

        [Then(@"le joueur est armé avec ""([^""]*)"" d'une portée de (.*)")]
        public void ThenLeJoueurEstArmeAvecDunePorteeDe(string weaponName, int weaponRange)
        {
            var player = this.context.CurrentPlayer;
            Assert.Equal(weaponName, player.Weapon.Name);
            Assert.Equal(weaponRange, player.Weapon.Range);
        }

    }
}