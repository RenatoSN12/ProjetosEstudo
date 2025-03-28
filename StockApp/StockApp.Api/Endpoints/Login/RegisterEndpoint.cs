using MediatR;
using StockApp.Api.Common;
using StockApp.Application.UseCases.Authentication.Register;
using StockApp.Domain.Repositories;

namespace StockApp.Api.Endpoints.Login;

public class RegisterEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", HandleAsync)
            .WithSummary("Authentication: Login")
            .WithName("Register")
            .WithDescription("Register a new user")
            .Produces<Response>();
    }

    private static async Task<IResult> HandleAsync(ISender sender, Command command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await sender.Send(command, cancellationToken);
            return result.IsFailure
                ? Results.BadRequest(result.Error)
                : Results.Created(string.Empty, result.Value);
        }
        catch
        {
            return Results.BadRequest();
        }
    }
}