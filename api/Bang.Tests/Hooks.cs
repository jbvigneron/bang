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
        private readonly HttpClientFactoryContext driver;

        public Hooks(TestWebApplicationFactory<Program> factory, HttpClientFactoryContext driver)
        {
            this.factory = factory;

            using (var scope = factory.Services.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<BangDbContext>();
                dbContext!.Database.EnsureCreated();
            }

            this.driver = driver;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            this.driver.Factory = this.factory;
        }
    }
}
