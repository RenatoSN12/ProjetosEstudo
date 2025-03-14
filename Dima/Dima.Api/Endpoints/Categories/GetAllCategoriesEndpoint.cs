using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All Categories.")
            .WithSummary("Consulta todas as categorias.")
            .WithDescription("Consulta todas as categorias.")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();
    }
    
    private static async Task<IResult> HandleAsync(ICategoryHandler handler,
        ClaimsPrincipal user,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            UserId = user.Identity?.Name ?? string.Empty,
        };
        
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}