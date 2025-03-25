using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using StockApp.Api.Common;
using StockApp.Application.UseCases.Authentication.Login;

namespace StockApp.Api.Endpoints.Login;

public class LoginEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", HandleAsync);
    }

    private static async Task<IResult> HandleAsync(HttpContext context, ISender sender, Command command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error.Message);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, result.Value.Email)
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await context.SignInAsync("Cookies", principal);

            return Results.Ok("Login efetuado com sucesso");
        }
        catch
        {
            return Results.BadRequest("Não foi possível efetuar o login.");
        }

    }
}