using StockApp.Domain.Abstractions;
using StockApp.Domain.Enums;

namespace StockApp.Domain.Entities;

public class Category : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EStatus IsActive { get; set; } = EStatus.Active;
}