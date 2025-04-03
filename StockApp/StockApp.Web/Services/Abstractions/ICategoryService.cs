using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Web.Services.Abstractions;

public interface ICategoryService
{
    Task<Result<Category>> GetAllCategoriesAsync();
}