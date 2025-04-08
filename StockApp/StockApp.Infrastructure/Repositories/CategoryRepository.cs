using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Categories;
using StockApp.Infrastructure.Data;

namespace StockApp.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetAllByUserAsync(GetAllCategoriesByUserSpecification specification, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = context.Categories.AsNoTracking().Where(specification.ToExpression())
            .OrderByDescending(x => x.Title);

        var categories = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return categories;
    }
    public async Task AddAsync(Category category, CancellationToken cancellationToken)
        => await context.Categories.AddAsync(category, cancellationToken);
    public async Task<int> GetTotalCount(ISpecification<Category> specification, CancellationToken cancellationToken = default)
        => await context.Categories.AsNoTracking().Where(specification.ToExpression()).CountAsync(cancellationToken);
}