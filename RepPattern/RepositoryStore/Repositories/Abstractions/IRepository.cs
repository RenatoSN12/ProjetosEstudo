namespace RepositoryStore.Repositories.Abstractions;

public interface IRepository<T> where T : class
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<List<T>?> GetAllAsync(int skip = 0, int take = 25, CancellationToken cancellationToken = default);
}