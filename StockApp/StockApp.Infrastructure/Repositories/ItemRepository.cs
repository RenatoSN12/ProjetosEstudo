using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Infrastructure.Data;

namespace StockApp.Infrastructure.Repositories;

public class ItemRepository(AppDbContext context) : IItemRepository
{
    public async Task CreateAsync(Item item, CancellationToken cancellationToken = default)
        => await context.Items.AddAsync(item, cancellationToken);

    public async Task<Item?> GetByIdAsync(Specification<Item> specification, CancellationToken cancellationToken = default)
        => await context.Items.Where(specification.ToExpression()).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    
}