using MediatR;
using StockApp.Domain.Abstractions;

namespace StockApp.Application.UseCases.Authentication.Logout;

public class Handler() : IRequestHandler<Command, Result<Response>>
{
    public Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Success(new Response()));
    }
}