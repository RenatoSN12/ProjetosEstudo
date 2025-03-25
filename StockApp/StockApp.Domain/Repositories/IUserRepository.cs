using StockApp.Domain.Entities;
using StockApp.Domain.Abstractions;

namespace StockApp.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserBySpecificationAsync(Specification<User> specification, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
}