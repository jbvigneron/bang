using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bang.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCore(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
