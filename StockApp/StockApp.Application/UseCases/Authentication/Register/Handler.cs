using FluentValidation.Results;
using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Enums;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Users;
using StockApp.Domain.ValueObjects;

namespace StockApp.Application.UseCases.Authentication.Register;

public class Handler(
    IUserRepository repository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork,
    RegisterCommandValidator validator) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateRequest(request, cancellationToken);
        if (!validationResult.IsSuccess)
            return validationResult;
        
        var fullnameResult = Fullname.Create(request.FirstName, request.LastName);
        if (!fullnameResult.IsSuccess)
            return Result.Failure<Response>(fullnameResult.Error);
        
        var user = new User
        {
            Fullname = fullnameResult.Value,
            Email = request.Email,
            PasswordHash = passwordHasher.HashPassword(request.Password),
            IsActive = EStatus.Active
        };

        await repository.AddAsync(user, cancellationToken);
        await unitOfWork.CommitAsync();

        return Result.Create(new Response("Usuário registrado com sucesso!"));
    }

    private async Task<Result<Response>> ValidateRequest(Command request, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
            return Result.Failure<Response>(new Error("400",
                string.Join(".", result.Errors.Select(x => x.ErrorMessage))));

        if (await EmailExists(request.Email, cancellationToken))
            return Result.Failure<Response>(new Error("400", "E-mail já está em uso."));

        return Result.Success(new Response(string.Empty));
    }

    private async Task<bool> EmailExists(string email, CancellationToken cancellationToken)
    {
        var spec = new GetUserByEmailSpecification(email);
        var user = await repository.GetUserBySpecificationAsync(spec, cancellationToken);

        return user != null;
    }
}