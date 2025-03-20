using Microsoft.EntityFrameworkCore;
using RepositoryStore.Repositories.Abstractions;

namespace RepositoryStore.Repositories;

public abstract class Repository<T>(DbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _entities = context.Set<T>();
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _entities.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _entities.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _entities.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _entities.FindAsync(id, cancellationToken);
    }

    public async Task<List<T>?> GetAllAsync(int skip = 0, int take = 25, CancellationToken cancellationToken = default)
    {
        return await _entities.Skip(skip).Take(take).AsNoTracking().ToListAsync(cancellationToken);
    }
}

