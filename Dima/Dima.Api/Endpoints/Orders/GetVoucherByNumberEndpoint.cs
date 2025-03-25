using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Orders;

public class GetVoucherByCodeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{code}", HandleAsync)
            .WithName("Voucher: Get By Code")
            .WithSummary("Recupera um voucher")
            .WithDescription("Recupera um voucher")
            .WithOrder(4)
            .Produces<Response<Voucher?>>();

    private static async Task<IResult> HandleAsync(
        IVoucherHandler handler,
        string code)
    {
        var request = new GetVoucherByCodeRequest()
        {
            Code = code
        };

        var result = await handler.GetByCodeAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}