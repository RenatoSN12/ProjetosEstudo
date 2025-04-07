using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;

namespace StockApp.Domain.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<int> GetTotalCount(ISpecification<T> specification, CancellationToken cancellationToken = default);
}