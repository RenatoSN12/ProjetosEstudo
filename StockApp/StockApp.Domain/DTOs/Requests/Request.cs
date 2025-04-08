namespace StockApp.Domain.DTOs.Requests;

public abstract class Request
{
    public string UserId { get; set; } = string.Empty;
}