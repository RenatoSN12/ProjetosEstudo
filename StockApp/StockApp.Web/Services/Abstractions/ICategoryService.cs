using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Entities;

namespace StockApp.Web.Services.Abstractions;

public interface ICategoryService
{
    Task<PagedResult<List<CategoryDto>?>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
}