using System.Text.Json.Serialization;
using MudBlazor;

namespace StockApp.Web.DTOs.Responses;

public class Response<T>(string? message = "", int statusCode = 200, T? data = default(T))
{
    [JsonIgnore]
    public bool IsSuccess => statusCode is >= 200 and <= 299;

    public int? StatusCode { get; set; } = statusCode;
    public string? Message { get; set; } = message;
    public T? Data { get; set; } = data;
}

