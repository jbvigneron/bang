using Bang.Persistence.Database;
using Bang.WebApi.Constants;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Bang.Tests.Support
{
    public class TestWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        public string CreateIntegrationTestToken()
        {
            var configuration = this.Services.GetRequiredService<IConfiguration>();
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is not configured.")));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] { new Claim("role", Roles.IntegrationTests) };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer is not configured."),
                audience: configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience is not configured."),
                claims: claims,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BangDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<BangDbContext>(options =>
                {
                    options.UseInMemoryDatabase("BangDb");
                });
            });

            return base.CreateHost(builder);
        }
    }
}