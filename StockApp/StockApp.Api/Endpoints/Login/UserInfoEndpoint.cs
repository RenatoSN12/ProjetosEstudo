using MediatR;
using StockApp.Api.Common;
using StockApp.Application.UseCases.Authentication.GetUserInfo;
using StockApp.Domain.Abstractions;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Entities;

namespace StockApp.Api.Endpoints.Login;

public class UserInfoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/info", HandleAsync)
            .WithSummary("Authentication: Get user email")
            .WithName("GetUserEmail")
            .WithDescription("Returns user email")
            .Produces<Response<UserInfoDto>>();
    }

    private static async Task<IResult> HandleAsync(ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new Command(), cancellationToken);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}