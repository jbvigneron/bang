using Bang.App.Repositories;
using Bang.Persistence.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Bang.Persistence.Database.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICardsRepository, CardsRepository>();
            services.AddScoped<ICharactersRepository, CharactersRepository>();
            services.AddScoped<IDeckRepository, DeckRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IWeaponsRepository, WeaponsRepository>();

            return services;
        }
    }
}