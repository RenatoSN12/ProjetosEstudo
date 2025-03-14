using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Responses;
using Dima.Core.Requests.Transactions;

namespace Dima.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Transaction: Update")
            .WithSummary("Atualiza uma transação")
            .WithDescription("Atualiza uma descrição")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();
    }

    private static async Task<IResult> HandleAsync(long id, ITransactionHandler handler,
        UpdateTransactionRequest request, ClaimsPrincipal user)
    {
        request.Id = id;
        request.UserId = user.Identity?.Name ?? string.Empty;

        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}