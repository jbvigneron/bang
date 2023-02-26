using Bang.Tests.Drivers;
using Bang.WebApi.Enums;

namespace Bang.Tests.StepDefinitions.Technical.Auth
{
    [Binding]
    public class AuthSteps
    {
        private readonly GameDriver gameDriver;

        public AuthSteps(GameDriver gameDriver)
        {
            this.gameDriver = gameDriver;
        }

        [When(@"""([^""]*)"" rejoint la partie en demandant un cookie")]
        public Task WhenRejointLaPartieEnDemandantUnCookie(string playerName)
        {
            return this.gameDriver.JoinGameAsync(playerName, AuthMode.Cookie);
        }

        [When(@"""([^""]*)"" s'authentifie en demandant un token JWT")]
        public Task WhenSauthentifieEnDemandantUnTokenJWT(string playerName)
        {
            return this.gameDriver.JoinGameAsync(playerName, AuthMode.Jwt);
        }

        [Then(@"""([^""]*)"" est connecté")]
        public async Task ThenEstConnecte(string playerName)
        {
            await this.gameDriver.UpdatePlayerAsync(playerName);
            this.gameDriver.IsPlayerExisting(playerName);
        }
    }
}
