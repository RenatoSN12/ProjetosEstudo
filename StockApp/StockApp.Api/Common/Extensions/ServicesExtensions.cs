using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StockApp.Application;
using StockApp.Application.UseCases.Authentication.Login;
using StockApp.Application.UseCases.Authentication.Register;
using StockApp.Domain;
using StockApp.Infrastructure;
using StockApp.Infrastructure.Data;

namespace StockApp.Api.Common.Extensions;

public static class ServicesExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddAuthorization();
        services.AddApplication();
        services.AddInfrastructure();
    }

    // public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services.AddDbContext<AppDbContext>(x =>
    //     {
    //         x.UseSqlServer(Configuration.ConnectionString, b
    //             => b.MigrationsAssembly("StockApp.Api"));
    //     });
    // }

    public static void AddAuthenticationSetup(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.SlidingExpiration = true;
            });
    }

    public static void AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
    }

    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(n => n.FullName);
        });
    }
    
    public static void AddCrossOrigin(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy(ApiConfiguration.CorsPolicyName,
            policy => policy
                .WithOrigins([Configuration.BackendUrl, Configuration.FrontendUrl])
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()));
    }
}
