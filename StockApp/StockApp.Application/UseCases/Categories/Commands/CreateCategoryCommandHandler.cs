using MediatR;
using Microsoft.AspNetCore.Http;
using StockApp.Application.Extensions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Entities;
using StockApp.Domain.Enums;
using StockApp.Domain.Repositories;

namespace StockApp.Application.UseCases.Categories.Commands;

public sealed class CreateCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
{
    public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = new Category
            {
                Description = request.Description,
                Status = EStatus.Active,
                Title = request.Title,
                UserId = request.UserId
            };

            await repository.AddAsync(category, cancellationToken);
            await unitOfWork.CommitAsync();

            return Result<CategoryDto>.Success(new CategoryDto(category.Id, category.Title));
        }
        catch
        {
            return Result<CategoryDto>.Failure(new Error("500", "Ocorreu um erro inesperado ao criar a categoria."));

        }
    }
}

