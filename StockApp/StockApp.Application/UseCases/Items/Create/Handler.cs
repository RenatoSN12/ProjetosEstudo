using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;

namespace StockApp.Application.UseCases.Items.Create;

public class Handler(IItemRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var product = new Item
        {
            Title = request.Title
        };
        
        await repository.CreateAsync(product, cancellationToken);
        await unitOfWork.CommitAsync();
        
        return Result.Success(new Response("Produto criado com sucesso"));
    }
}