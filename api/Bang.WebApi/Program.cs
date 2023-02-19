using Bang.Core;
using Bang.Core.Hubs;
using Bang.Database;
using Bang.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<BangDbContext>(options =>
               options.UseInMemoryDatabase("BangDb")
);

builder.Services.RegisterCore();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.EventsType = typeof(CustomCookieAuthenticationEvents);
    });

builder.Services.AddScoped<CustomCookieAuthenticationEvents>();

builder.Services.AddSignalR();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bang API v1", Version = "v1" });
    options.AddSignalRSwaggerGen(options =>
    {
        var assemblyCore = Assembly.Load("Bang.Core");
        options.ScanAssembly(assemblyCore);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

public partial class Program { }