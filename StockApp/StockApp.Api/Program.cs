using FluentValidation;
using FluentValidation.AspNetCore;
using StockApp.Application;
using StockApp.Domain;
using StockApp.Infrastructure;
using StockApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StockApp.Api;
using StockApp.Api.Common;
using StockApp.Api.Endpoints;
using StockApp.Application.UseCases.Authentication.Login;
using StockApp.Application.UseCases.Authentication.Register;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddCrossOrigin();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(Configuration.ConnectionString, b
        => b.MigrationsAssembly("StockApp.Api"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });

builder.Services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddAuthorization();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(ApiConfiguration.CorsPolicyName);

app.MapEndpoints();
app.UseAuthentication();
app.UseAuthorization();

app.Run();