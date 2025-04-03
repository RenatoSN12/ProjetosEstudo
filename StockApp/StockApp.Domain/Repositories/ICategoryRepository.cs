using StockApp.Domain.Entities;
using StockApp.Domain.Specification.Categories;
using StockApp.Domain.Specification.Items;

namespace StockApp.Domain.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllByUserAsync(GetAllCategoriesByUserSpecification specification, CancellationToken cancellationToken = default);
}