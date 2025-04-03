using MediatR;
using Microsoft.AspNetCore.Http;
using StockApp.Application.Extensions;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Repositories;

namespace StockApp.Application.UseCases.Categories.Queries;

public class GetAllCategoriesQueryHandler(ICategoryRepository repository, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    public Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var spec = httpContextAccessor.HttpContext.GetUserEmail();
        var categories = await repository.GetAllByUserAsync()
    }
}