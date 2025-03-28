using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;

namespace StockApp.Infrastructure.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync() => await context.SaveChangesAsync();
}