using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Responses;

namespace Dima.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Categories: Get by Id")
            .WithSummary("Consulta uma categoria.")
            .WithDescription("Consulta uma categoria.")
            .WithOrder(4)
            .Produces<Response<Category?>>();
    }

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new GetCategoryByIdRequest
        {
            Id = id,
            UserId = "natosouza12@gmail.com"
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
    
}