using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Repositories;

public interface IItemRepository : IRepository<Item>
{
    Task CreateAsync(Item item, CancellationToken cancellationToken = default);
    Task<Item?> GetByIdAsync(Specification<Item> specification, CancellationToken cancellationToken = default);
}