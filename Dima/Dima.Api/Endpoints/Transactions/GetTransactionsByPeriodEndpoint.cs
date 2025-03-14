using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Responses;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionsByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync)
            .WithName("Transactions: Get transactions by period.")
            .WithSummary("Consulta as transações por período.")
            .WithDescription("Consulta as transações por período.")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>?>>();
    }
    
    private static async Task<IResult> HandleAsync(ITransactionHandler handler,
        ClaimsPrincipal user,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetTransactionsByPeriodRequest()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate,
            UserId = user.Identity?.Name ?? string.Empty
        };
        
        var result = await handler.GetByPeriodAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}