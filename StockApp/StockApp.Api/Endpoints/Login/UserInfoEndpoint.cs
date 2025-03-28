using System.Security.Claims;
using MediatR;
using StockApp.Api.Common;
using StockApp.Application.UseCases.Authentication.GetUserEmail;

namespace StockApp.Api.Endpoints.Login;

public class UserInfoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/email", HandleAsync)
            .WithSummary("Authentication: Get user email")
            .WithName("GetUserEmail")
            .WithDescription("Returns user email")
            .Produces<Response>();
    }

    private static async Task<IResult> HandleAsync(ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new Command(), cancellationToken);
        return result.IsSuccess 
            ? Results.Ok(result.Value.email) 
            : Results.BadRequest(result.Error);
    }
}