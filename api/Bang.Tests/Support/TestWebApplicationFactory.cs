using Bang.Database;
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
using System.Security.Cryptography;
using System.Text;

namespace Bang.Tests.Support
{
    public class TestWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        private static readonly string jwtKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        static TestWebApplicationFactory()
        {
            Environment.SetEnvironmentVariable("Jwt__Key", jwtKey);
        }

        public string CreateIntegrationTestToken()
        {
            var configuration = this.Services.GetRequiredService<IConfiguration>();
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] { new Claim("role", Roles.IntegrationTests) };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
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