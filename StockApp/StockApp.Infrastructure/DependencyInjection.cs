using Microsoft.Extensions.DependencyInjection;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Repositories;
using StockApp.Infrastructure.Data;
using StockApp.Infrastructure.Repositories;
using StockApp.Infrastructure.Services;

namespace StockApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IItemRepository, ItemRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        return services;
    }
}