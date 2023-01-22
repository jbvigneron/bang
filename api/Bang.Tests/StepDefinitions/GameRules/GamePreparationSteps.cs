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
            await this.gameDriver.UpdateGameAsync();
            this.rulesDriver.CheckPlayerHasACharacter(playerName);
        }

        [Then(@"le nombre de vies de ""([^""]*)"" lui est attribué selon son personnage et son rôle")]
        public async Task ThenLeNombreDeViesDeLuiEstAttribueSelonSonPersonnageEtSonRole(string playerName, Table table)
        {
            await this.gameDriver.UpdateGameAsync();
            this.rulesDriver.CheckPlayerLives(playerName, table);
        }

        [Then(@"l arme principale de ""([^""]*)"" est ""([^""]*)"" d'une portée de (.*)")]
        public async Task ThenLArmePrincipaleDeEstDunePorteeDe(string playerName, string weaponName, int weaponRange)
        {
            await this.gameDriver.UpdateGameAsync();
            this.rulesDriver.CheckWeapon(playerName, weaponName, weaponRange);
        }

        [Then(@"""([^""]*)"" possède autant de cartes qu'il a de points de vie")]
        public async Task ThenPossedeAutantDeCartesQuilADePointsDeVies(string playerName)
        {
            await this.gameDriver.UpdateGameAsync();
            await this.gameDriver.UpdatePlayerCardsAsync(playerName);
            this.rulesDriver.CheckPlayerDeckCount(playerName);
        }

        [Then(@"c'est au shérif de commencer")]
        public async Task ThenCEstAuSherifDeCommencer()
        {
            await this.gameDriver.UpdateGameAsync();
            this.rulesDriver.CheckFirstPlayerIsTheScheriff();
        }

        [Then(@"la pioche comporte (.*) cartes")]
        public async Task ThenLaPiocheComporteCartes(int count)
        {
            await this.gameDriver.UpdateGameAsync();
            this.rulesDriver.CheckGameDeckCount(count);
        }
    }
}