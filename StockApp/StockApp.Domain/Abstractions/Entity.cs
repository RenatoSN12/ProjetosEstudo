namespace StockApp.Domain.Abstractions;

public abstract class Entity
{
    public long Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}