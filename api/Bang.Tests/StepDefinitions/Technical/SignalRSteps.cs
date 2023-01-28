using Bang.Database.Enums;
using Bang.Tests.Drivers;

namespace Bang.Tests.StepDefinitions.Technical
{
    [Binding]
    public sealed class SignalRSteps
    {
        private readonly GameDriver gameDriver;
        private readonly RulesDriver rulesDriver;
        private readonly StatusDriver stateDriver;

        private readonly PublicHubDriver publicHubDriver;
        private readonly GameHubDriver gameHubDriver;
        private readonly PlayerHubDriver playerHubDriver;

        private IEnumerable<string> playerNames;

        public SignalRSteps(
            GameDriver gameDriver,
            RulesDriver rulesDriver,
            StatusDriver stateDriver,
            PublicHubDriver publicHubDriver,
            GameHubDriver gameHubDriver,
            PlayerHubDriver playerHubDriver)
        {
            this.gameDriver = gameDriver;
            this.rulesDriver = rulesDriver;
            this.stateDriver = stateDriver;

            this.publicHubDriver = publicHubDriver;
            this.gameHubDriver = gameHubDriver;
            this.playerHubDriver = playerHubDriver;
        }

        [Given(@"ces joueurs souhaitent faire une partie")]
        public void GivenCesJoueursSouhaitentFaireUnePartie(Table table)
        {
            this.playerNames = table.Rows.Select(r => r["playerName"]);
        }

        [When(@"une partie est initialisée")]
        public async Task WhenUnePartieEstInitialisee()
        {
            await Task.WhenAll(
                this.publicHubDriver.ConnectToHubAsync(),
                this.gameHubDriver.ConnectToHubAsync()
            );

            await this.gameDriver.InitGameAsync(this.playerNames);
        }

        [When(@"""([^""]*)"" rejoint la partie")]
        public async Task WhenRejointLaPartie(string playerName)
        {
            await this.gameHubDriver.SubscribeToMessagesAsync();
            await this.gameDriver.JoinGameAsync(playerName);

            await this.playerHubDriver.ConnectToHubAsync(playerName);
            await this.playerHubDriver.SubscribeToMessagesAsync(playerName);
        }

        [Then(@"un message ""([^""]*)"" est envoyé au hub public")]
        public void ThenUnMessageAuHubPublic(string message)
        {
            this.publicHubDriver.CheckMessage(message);
        }

        [Then(@"le jeu contient (.*) cartes")]
        public void ThenLeJeuContientCartes(int count)
        {
            this.rulesDriver.CheckGameDeckCount(count);
        }

        [Then(@"un message ""([^""]*)"" est envoyé au hub du jeu")]
        public Task ThenUnMessageEstEnvoyeAuHubDuJeu(string message)
        {
            return this.gameHubDriver.CheckMessageAsync(message);
        }

        [Then(@"""([^""]*)"" est prêt")]
        public void ThenEstPret(string playerName)
        {
            this.stateDriver.CheckPlayerStatus(playerName, PlayerStatus.Alive);
        }

        [Then(@"la partie peut commencer")]
        public void ThenLaPartiePeutCommencer()
        {
            this.stateDriver.CheckGameStatus(GameStatus.InProgress);
        }

        [Then(@"un message ""([^""]*)"" est envoyé au schérif")]
        public async Task ThenUnMessageEstEnvoyeAuScherif(string message)
        {
            var scheriffName = this.gameDriver.GetScheriffName();
            await this.playerHubDriver.CheckMessageAsync(scheriffName, message);
        }

        [Then(@"c'est au tour du schérif")]
        public void ThenCestAuTourDuScherif()
        {
            this.stateDriver.CheckIsScheriffTurn();
        }

        [Then(@"un message ""([^""]*)"" est envoyé à ""([^""]*)""")]
        public Task ThenUnMessageEstEnvoyeA(string message, string playerName)
        {
            return this.playerHubDriver.CheckMessageAsync(playerName, message);
        }

        [Then(@"le schérif pioche de nouvelles cartes")]
        public Task ThenLeScherifPiocheDeNouvellesCartes()
        {
            var scheriffName = this.gameDriver.GetScheriffName();
            return this.gameDriver.DrawCardsAsync(scheriffName);
        }
    }
}