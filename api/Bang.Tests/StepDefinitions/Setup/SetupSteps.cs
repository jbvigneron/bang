using Bang.Tests.Drivers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Bang.Tests.StepDefinitions.Setup
{
    [Binding]
    public class SetupSteps
    {
        private readonly GameDriver gameDriver;
        private readonly RulesDriver rulesDriver;

        private List<string> playerNames = new();
        private HttpResponseMessage creationResponse = default!;

        public IEnumerable<string> PlayerNames => this.playerNames;

        public SetupSteps(GameDriver gameDriver, RulesDriver rulesDriver)
        {
            this.gameDriver = gameDriver;
            this.rulesDriver = rulesDriver;
        }

        [Given(@"ces joueurs veulent jouer")]
        public void GivenCesJoueursVeulentJouer(Table table)
        {
            this.playerNames = table.Rows.Select(r => r["playerName"]).ToList();
        }

        [Given(@"ces joueurs supplémentaires veulent jouer")]
        public void GivenCesJoueursSupplementairesVeulentJouer(Table table)
        {
            this.playerNames.AddRange(table.Rows.Select(r => r["playerName"]));
        }

        [When(@"la partie se prépare")]
        public Task WhenLaPartieSePrepare()
        {
            return this.gameDriver.InitGameAsync(this.playerNames);
        }

        [When(@"la partie est créée")]
        public async Task WhenLaPartieEstCreee()
        {
            this.creationResponse = await this.gameDriver.TryInitGameAsync(this.playerNames);
        }

        [Then(@"la création est refusée pour données invalides")]
        public async Task ThenLaCreationEstRefuseePourDonneesInvalides()
        {
            var body = await this.creationResponse.Content.ReadAsStringAsync();
            Assert.True(this.creationResponse.StatusCode == HttpStatusCode.BadRequest, body);
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

        [Then(@"il y a un renégat")]
        public void ThenIlYAUnRenegat()
        {
            this.rulesDriver.CheckHasOneRenegade();
        }

        [Then(@"il y a (.*) hors-la-loi")]
        public void ThenIlYAHorsLaLoi(int count)
        {
            this.rulesDriver.CheckOutlawsCount(count);
        }

        [Then(@"il y a (.*) adjoint\w? au shérif")]
        public void ThenIlYAAdjointsAuSherif(int count)
        {
            this.rulesDriver.CheckDeputiesCount(count);
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
            this.rulesDriver.CheckPlayerLivesAndCardsCount(playerName);
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