using System.Security.Claims;

namespace StockApp.Api.Common.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserEmail(this HttpContext client)
        => client.User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
}