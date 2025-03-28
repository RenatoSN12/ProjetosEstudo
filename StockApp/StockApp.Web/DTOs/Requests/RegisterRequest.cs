using System.Text.Json.Serialization;

namespace StockApp.Web.DTOs.Requests;

public record RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    [JsonIgnore]
    public string ConfirmPassword { get; set; } = string.Empty;
}