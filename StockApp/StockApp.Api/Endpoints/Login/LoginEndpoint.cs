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
        app.MapPost("/login", HandleAsync)
            .WithSummary("Authentication: Login")
            .WithName("Login")
            .WithDescription("Logs the user in")
            .Produces<Response>();
    }

    private static async Task<IResult> HandleAsync(HttpContext context,ClaimsPrincipal user , ISender sender, Command command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);
            
            await context.SignInAsync("Cookies", result.Value.Identity!, new AuthenticationProperties
            {
                IsPersistent = true
            });
            
            return Results.Ok("Login efetuado com sucesso");
        }
        catch
        {
            return Results.BadRequest("Não foi possível efetuar o login.");
        }

    }
}