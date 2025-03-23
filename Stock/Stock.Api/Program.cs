using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stock.Api.Data;
using Stock.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/forbidden";
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

Configuration.ConnectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(Configuration.ConnectionString);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.Run();
