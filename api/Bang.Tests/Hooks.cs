using Bang.Database;
using Bang.Tests.Contexts;
using Bang.Tests.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Bang.Tests
{
    [Binding]
    public class Hooks
    {
        private readonly TestWebApplicationFactory<Program> factory;
        private readonly BrowsersContext browser;

        public Hooks(TestWebApplicationFactory<Program> factory, BrowsersContext browser)
        {
            this.factory = factory;
            this.browser = browser;

            using (var scope = factory.Services.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<BangDbContext>();
                dbContext!.Database.EnsureCreated();
            }
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            this.browser.HttpClientFactory = this.factory;
        }
    }
}
