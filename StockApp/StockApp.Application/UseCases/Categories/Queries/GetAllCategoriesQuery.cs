using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Application.UseCases.Categories.Queries;

public sealed record GetAllCategoriesQuery(string Email, int PageNumber, int PageSize)
    : IRequest<PagedResult<List<CategoryDto>?>>;
