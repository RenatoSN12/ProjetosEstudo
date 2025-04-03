using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Services.Implementations;

public class CategoryService : ICategoryService
{
    public Task<Result<Category>> GetAllCategoriesAsync()
    {
        
    }
}