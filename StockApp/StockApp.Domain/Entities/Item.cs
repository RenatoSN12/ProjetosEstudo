using StockApp.Domain.Abstractions;
using StockApp.Domain.Enums;

namespace StockApp.Domain.Entities;

public class Item : Entity
{
    public string CustomId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public EStatus IsActive { get; set; } = EStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<ItemStock> Inventories { get; set; } = [];
}