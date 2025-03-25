using StockApp.Domain.Enums;

namespace StockApp.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public EStatus IsActive { get; set; } = EStatus.Active;
}