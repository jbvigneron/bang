using Bang.App.Extensions;
using Bang.App.Hubs;
using Bang.Persistence.Database;
using Bang.Persistence.Database.Extensions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .Enrich.FromLogContext()
    .Enrich.WithCorrelationId()
    .Enrich.WithExceptionDetails()
    .Enrich.WithDemystifiedStackTraces()
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddControllers();

    builder.Services.AddProblemDetails(opts =>
    {
        opts.IncludeExceptionDetails = (context, ex) =>
        {
            var environment = context.RequestServices.GetRequiredService<IHostEnvironment>();
            return environment.IsDevelopment();
        };
    });

    builder.Services.AddDbContext<BangDbContext>(options =>
                   options.UseInMemoryDatabase("BangDb")
    );

    builder.Services.AddApp();
    builder.Services.AddRepositories();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    string cookieName = "auth";
                    context.Token = context.Request.Cookies[cookieName];

                    return Task.CompletedTask;
                }
            };
        });

    builder.Services.AddSignalR();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bang API v1", Version = "v1" });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    var app = builder.Build();

    app.UseProblemDetails();
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();

        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapHub<PublicHub>("/PublicHub");
    app.MapHub<GameHub>("/GameHub");
    app.MapHub<PlayerHub>("/PlayerHub");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program
{ }