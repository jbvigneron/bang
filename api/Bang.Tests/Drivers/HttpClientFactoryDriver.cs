using Bang.Database;
using Bang.Tests.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Bang.Tests.Drivers
{
    public class HttpClientFactoryDriver
    {
        public HttpClientFactoryDriver()
        {
            Factory = new TestWebApplicationFactory<Program>();

            using (var scope = Factory.Services.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<BangDbContext>();
                dbContext!.Database.EnsureCreated();
            }
        }

        public TestWebApplicationFactory<Program>? Factory { get; }
    }
}
