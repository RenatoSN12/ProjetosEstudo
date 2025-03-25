using MediatR;
using StockApp.Domain.Abstractions;

namespace StockApp.Application.UseCases.Items.GetById;

public sealed record Command(long id) : IRequest<Result<Response>>
{
    
}