using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using StockApp.Domain.Abstractions;

namespace StockApp.Application.UseCases.Authentication.GetUserEmail;

public class Handler(IHttpContextAccessor httpContextAccessor) : IRequestHandler<Command, Result<Response>>
{
    public Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var userEmail = httpContextAccessor.HttpContext?.User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value;
        return userEmail is null
            ? Task.FromResult(Result.Failure<Response>(new Error("404",
                "Não foi encontrado um e-mail cadastrado para o usuário logado.")))
            : Task.FromResult(Result.Success(new Response(userEmail)));
    }
}