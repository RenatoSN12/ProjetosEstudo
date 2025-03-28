using System.Security.Claims;

namespace StockApp.Application.UseCases.Authentication.Login;

public sealed record Response(ClaimsPrincipal? Identity);



    
