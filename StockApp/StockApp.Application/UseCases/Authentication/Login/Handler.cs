using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Users;

namespace StockApp.Application.UseCases.Authentication.Login;

public class Handler(IUserRepository repository, IPasswordHasher passwordHasher, LoginCommandValidator validator, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateRequest(request, cancellationToken);
        if (validationResult.IsFailure)
            return Result.Failure<Response>(validationResult.Error);

        var spec = new GetUserByEmailSpecification(request.Email);
        var user = await repository.GetUserBySpecificationAsync(spec, cancellationToken);

        if (user == null || !passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            return Result.Failure<Response>(new Error("401", "Usuário ou senha incorretos."));

        return Result.Success(new Response(CreateClaimsPrincipal(user)));
    }

    private async Task<Result> ValidateRequest(Command request, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(request, cancellationToken);
        return !result.IsValid 
            ? Result.Failure(new Error("400", string.Join("\n", result.Errors.Select(e => e.ErrorMessage)))) 
            : Result.Success();
    }
    private static ClaimsPrincipal CreateClaimsPrincipal(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Fullname.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        var identity = new ClaimsIdentity(claims, "Cookies");
        return new ClaimsPrincipal(identity);
    }
}