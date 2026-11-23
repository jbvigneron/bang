using Bang.App.Behaviors;
using FluentValidation;
﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bang.App.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApp(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}