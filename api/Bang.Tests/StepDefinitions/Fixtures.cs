using Bang.Database;
using Bang.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Bang.Tests.StepDefinitions
{
    [Binding]
    public class Fixtures
    {
        private readonly TestWebApplicationFactory<Program> factory;
        private readonly GameContext gameContext;

        public Fixtures(TestWebApplicationFactory<Program> factory, GameContext gameContext)
        {
            this.factory = factory;
            this.gameContext = gameContext;

            var scope = factory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var dbContext = scope.ServiceProvider.GetService<BangDbContext>();
            dbContext.Database.EnsureCreated();
        }

        [BeforeScenario]
        public void BeforeTestRun()
        {
            this.gameContext.HttpClient = factory.CreateClient();
        }
    }
}
