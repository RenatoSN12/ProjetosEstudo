using MediatR;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Application.UseCases.Categories.Queries;

public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
{
    
}