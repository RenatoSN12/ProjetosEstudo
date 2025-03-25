using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Users;

namespace StockApp.Application.UseCases.Authentication.Login;

public class Handler(IUserRepository repository, IPasswordHasher passwordHasher, LoginCommandValidator validator)
    : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return Result.Failure<Response>(new Error("400",
                string.Join(Environment.NewLine, result.Errors.Select(x => x.ErrorMessage))));
        
        var spec = new GetUserByEmailSpecification(request.Email);
        var user = await repository.GetUserBySpecificationAsync(spec, cancellationToken);

        if (user == null)
            return Result.Failure<Response>(new Error("404", "Usuário ou senha incorretos."));

        var passwordValid = passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
        return !passwordValid
            ? Result.Failure<Response>(new Error("401", "Usuário ou senha incorretos."))
            : new Response(request.Email, request.Email);
    }
}