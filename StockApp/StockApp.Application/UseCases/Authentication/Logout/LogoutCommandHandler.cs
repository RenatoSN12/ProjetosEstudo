using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;

namespace StockApp.Application.UseCases.Authentication.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    public Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        // Não faz nada, criado somente para seguir o padrão CQRS.
        return Task.FromResult(Result.Success());
    }
}