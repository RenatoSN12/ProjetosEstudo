using System.Net.Http.Headers;
using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Items;

namespace StockApp.Application.UseCases.Items.GetById;

public sealed class Handler(IItemRepository repository)
: IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var spec = new GetProductByIdSpecification(request.id);
        var item = await repository.GetByIdAsync(spec, cancellationToken);

        return item is null
            ? Result.Failure<Response>(new Error("404","Produto não encontrado."))
            : Result.Success<Response>(new Response(item.Id, item.Title));
    }
}