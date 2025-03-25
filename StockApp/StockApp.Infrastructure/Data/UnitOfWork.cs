using StockApp.Domain.Abstractions;

namespace StockApp.Infrastructure.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync() => await context.SaveChangesAsync();
}