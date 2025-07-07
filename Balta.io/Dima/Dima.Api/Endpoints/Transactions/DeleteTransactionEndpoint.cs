using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Responses;
using Dima.Core.Requests.Transactions;

namespace Dima.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Transaction: Delete")
            .WithSummary("Exclui uma transação")
            .WithDescription("Exclui uma transação")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();
    }

    private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id, ClaimsPrincipal user)
    {
        var request = new DeleteTransactionRequest
        {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };

        var result = await handler.DeleteAsync(request);
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.NotFound(result);
            
    }
}