namespace Stock.Domain.Models;

public abstract class Entity
{
    public long Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}