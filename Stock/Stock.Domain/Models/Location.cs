using Stock.Domain.Enums;

namespace Stock.Domain.Models;

public class Location : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EStatus IsActive { get; set; } = EStatus.Active;
}