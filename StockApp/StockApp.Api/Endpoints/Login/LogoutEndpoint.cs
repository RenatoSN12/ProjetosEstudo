using MediatR;
using Microsoft.AspNetCore.Authentication;
using StockApp.Api.Common;
using StockApp.Application.UseCases.Authentication.Logout;

namespace StockApp.Api.Endpoints.Login;

public class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/logout", HandleAsync)
            .RequireAuthorization()
            .WithSummary("Authentication: Logout")
            .WithName("Logout")
            .WithDescription("Logs the user out")
            .Produces<Response>();
    }

    private static async Task<IResult> HandleAsync(HttpContext httpContext, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new Command(), cancellationToken);
        await httpContext.SignOutAsync("Cookies");

        return Results.Ok("Logout realizado com sucesso");
    }
}