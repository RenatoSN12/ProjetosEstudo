using MediatR;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Application.UseCases.Categories.Commands;

public sealed class CreateCategoryCommand(string userId, string title, string description)
    : IRequest<Result<CategoryDto>>
{
    public string UserId { get; set; } = userId;
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
}