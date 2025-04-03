using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace StockApp.Application.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserEmail(this HttpContext client)
        => client.User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
}