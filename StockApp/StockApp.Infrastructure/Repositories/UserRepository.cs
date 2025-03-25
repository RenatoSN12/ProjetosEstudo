using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Infrastructure.Data;

namespace StockApp.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> GetUserBySpecificationAsync(Specification<User> specification, CancellationToken cancellationToken = default)
        => await context.Users.Where(specification.ToExpression()).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        => await context.Users.AddAsync(user, cancellationToken);
}