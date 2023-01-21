using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions.GameRules
{
    [Binding]
    public sealed class GamePreparationSteps
    {
        private readonly GameDriver gameDriver;
        private readonly RulesDriver rulesDriver;

        private IEnumerable<string> playerNames;

        public GamePreparationSteps(GameDriver gameDriver, RulesDriver rulesDriver)
        {
            this.gameDriver = gameDriver;
            this.rulesDriver = rulesDriver;
        }

        [Given(@"les joueurs suivants veulent jouer")]
        public void GivenCesJoueursVeulentJouer(Table table)
        {
            this.playerNames = table.Rows.Select(r => r["playerName"]);
        }

        [When(@"la partie se prépare")]
        public Task WhenLaPartieSePrepare()
        {
            return this.gameDriver.InitGameAsync(this.playerNames);
        }

        [When(@"""([^""]*)"" pioche une carte personnage")]
        public Task WhenPiocheUneCartePersonnage(string playerName)
        {
            return this.gameDriver.JoinGameAsync(playerName);
        }

        [Then(@"il y a un shérif")]
        public void ThenIlYAUnSherif()
        {
            this.rulesDriver.CheckHasOneSheriff();
        }

        [Then(@"il y a (.*) autres personnes avec un autre rôle")]
        public void ThenIlYAAutresPersonnesAvecUnAutreRole(int count)
        {
            this.rulesDriver.CheckAllOthersRoles(count);
        }

        [Then(@"le shérif dévoile sa carte")]
        public void ThenLeScherifDevoileSaCarte()
        {
            this.rulesDriver.CheckIsSheriffUnveiled();
        }

        [Then(@"un personnage est attribué à ""([^""]*)""")]
        public async Task ThenUnPersonnageEstAttribueA(string playerName)
        {
            var player = await this.gameDriver.GetPlayerInfoAsync(playerName);
            this.rulesDriver.CheckPlayerHasACharacter(player);
        }

        [Then(@"le nombre de vies de ""([^""]*)"" lui est attribué selon son personnage et son rôle")]
        public async Task ThenLeNombreDeViesDeLuiEstAttribueSelonSonPersonnageEtSonRole(string playerName, Table table)
        {
            var player = await this.gameDriver.GetPlayerInfoAsync(playerName);
            var expectedLives = int.Parse(table.Rows.Single(r => r["characterName"] == player!.Character!.Name)["lives"]);
            this.rulesDriver.CheckPlayerLives(player, expectedLives);
        }

        [Then(@"l arme principale de ""([^""]*)"" est ""([^""]*)"" d'une portée de (.*)")]
        public async Task ThenLArmePrincipaleDeEstDunePorteeDe(string playerName, string weaponName, int weaponRange)
        {
            var player = await this.gameDriver.GetPlayerInfoAsync(playerName);
            this.rulesDriver.CheckWeapon(player, weaponName, weaponRange);
        }

        [Then(@"c'est au shérif de commencer")]
        public async Task ThenCEstAuSherifDeCommencer()
        {
            await this.gameDriver.UpdateGameAsync();
            this.rulesDriver.CheckFirstPlayerIsTheScheriff();
        }

        [Then(@"la pioche comporte (.*) cartes")]
        public void ThenLaPiocheComporteCartes(int count)
        {
            this.rulesDriver.CheckDeckCount(count);
        }

    }
}