using System.Text.Json.Serialization;

namespace StockApp.Web.Services;

public class ErrorReturn
{
    public ErrorReturn(){}

    [JsonConstructor]
    public ErrorReturn(string message, string code)
    {
        Code = code;
        Message = message;
    }
    [JsonPropertyName("code")] public string Code { get; init; } = string.Empty;
    [JsonPropertyName("message")] public string Message { get; init; } = string.Empty;
}
