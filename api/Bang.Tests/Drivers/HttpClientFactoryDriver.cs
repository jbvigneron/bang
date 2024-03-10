using Bang.Persistence.Database;
using Bang.Tests.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Bang.Tests.Drivers
{
    public class HttpClientFactoryDriver
    {
        public HttpClientFactoryDriver()
        {
            this.Factory = new TestWebApplicationFactory<Program>();

            using (var scope = this.Factory.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<BangDbContext>();
                dbContext.Database.EnsureCreated();
            }
        }

        public TestWebApplicationFactory<Program> Factory { get; }
    }
}