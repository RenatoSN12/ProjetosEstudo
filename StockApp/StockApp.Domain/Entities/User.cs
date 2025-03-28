using StockApp.Domain.Enums;
using StockApp.Domain.ValueObjects;

namespace StockApp.Domain.Entities;

public class User
{
    public long Id { get; set; }
    
    public Fullname Fullname { get; set; } = null!;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public EStatus IsActive { get; set; } = EStatus.Active;
}