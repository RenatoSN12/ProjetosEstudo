namespace StockApp.Domain.Abstractions.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}