using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Responses;
using Dima.Core.Requests.Transactions;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions: Get by Id")
            .WithSummary("Consulta uma transação pelo id.")
            .WithDescription("Consulta uma transação pelo id.")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();
    }

    private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id, ClaimsPrincipal user)
    {
        var request = new GetTransactionByIdRequest
        {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}