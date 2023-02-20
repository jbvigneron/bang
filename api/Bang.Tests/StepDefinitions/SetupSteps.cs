using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions
{
    [Binding]
    public sealed class SetupSteps
    {
        private readonly GameDriver gameDriver;
        private readonly RulesDriver rulesDriver;

        private List<string> playerNames = new();

        public SetupSteps(GameDriver gameDriver, RulesDriver rulesDriver)
        {
            this.gameDriver = gameDriver;
            this.rulesDriver = rulesDriver;
        }

        [Given(@"ces joueurs veulent jouer")]
        public void GivenCesJoueursVeulentJouer(Table table)
        {
            playerNames = table.Rows.Select(r => r["playerName"]).ToList();
        }

        [Given(@"ces joueurs supplémentaires veulent jouer")]
        public void GivenCesJoueursSupplementairesVeulentJouer(Table table)
        {
            playerNames.AddRange(table.Rows.Select(r => r["playerName"]));
        }

        [When(@"la partie se prépare")]
        public Task WhenLaPartieSePrepare()
        {
            return gameDriver.InitGameAsync(playerNames);
        }

        [When(@"""([^""]*)"" pioche une carte personnage")]
        public Task WhenPiocheUneCartePersonnage(string playerName)
        {
            return gameDriver.JoinGameAsync(playerName);
        }

        [Then(@"il y a un shérif")]
        public void ThenIlYAUnSherif()
        {
            rulesDriver.CheckHasOneSheriff();
        }

        [Then(@"il y a un renégat")]
        public void ThenIlYAUnRenegat()
        {
            rulesDriver.CheckHasOneRenegade();
        }

        [Then(@"il y a (.*) hors-la-loi")]
        public void ThenIlYAHorsLaLoi(int count)
        {
            rulesDriver.CheckOutlawsCount(count);
        }

        [Then(@"il y a (.*) adjoint\w? au shérif")]
        public void ThenIlYAAdjointsAuSherif(int count)
        {
            rulesDriver.CheckDeputiesCount(count);
        }

        [Then(@"le shérif dévoile sa carte")]
        public void ThenLeScherifDevoileSaCarte()
        {
            rulesDriver.CheckIsSheriffUnveiled();
        }

        [Then(@"un personnage est attribué à ""([^""]*)""")]
        public async Task ThenUnPersonnageEstAttribueA(string playerName)
        {
            await gameDriver.UpdateGameAsync();
            rulesDriver.CheckPlayerHasACharacter(playerName);
        }

        [Then(@"le nombre de vies de ""([^""]*)"" lui est attribué selon son personnage et son rôle")]
        public async Task ThenLeNombreDeViesDeLuiEstAttribueSelonSonPersonnageEtSonRole(string playerName, Table table)
        {
            await gameDriver.UpdateGameAsync();
            rulesDriver.CheckPlayerLives(playerName, table);
        }

        [Then(@"l arme principale de ""([^""]*)"" est ""([^""]*)"" d'une portée de (.*)")]
        public async Task ThenLArmePrincipaleDeEstDunePorteeDe(string playerName, string weaponName, int weaponRange)
        {
            await gameDriver.UpdateGameAsync();
            rulesDriver.CheckWeapon(playerName, weaponName, weaponRange);
        }

        [Then(@"""([^""]*)"" possède autant de cartes qu'il a de points de vie")]
        public async Task ThenPossedeAutantDeCartesQuilADePointsDeVies(string playerName)
        {
            await gameDriver.UpdateGameAsync();
            await gameDriver.UpdatePlayerCardsAsync(playerName);
            rulesDriver.CheckPlayerLivesAndCardsCount(playerName);
        }

        [Then(@"c'est au shérif de commencer")]
        public async Task ThenCEstAuSherifDeCommencer()
        {
            await gameDriver.UpdateGameAsync();
            rulesDriver.CheckFirstPlayerIsTheScheriff();
        }

        [Then(@"la pioche comporte (.*) cartes")]
        public async Task ThenLaPiocheComporteCartes(int count)
        {
            await gameDriver.UpdateGameAsync();
            rulesDriver.CheckGameDeckCount(count);
        }
    }
}