using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Categories;
using StockApp.Infrastructure.Data;

namespace StockApp.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public Task<List<Category>> GetAllByUserAsync(GetAllCategoriesByUserSpecification specification,
        CancellationToken cancellationToken = default)
        => context.Categories.AsNoTracking().Where(specification.ToExpression()).OrderByDescending(x => x.Title)
            .ToListAsync(cancellationToken);
}