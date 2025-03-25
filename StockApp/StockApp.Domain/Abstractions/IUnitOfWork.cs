namespace StockApp.Domain.Abstractions;

public interface IUnitOfWork
{
    Task CommitAsync();
}