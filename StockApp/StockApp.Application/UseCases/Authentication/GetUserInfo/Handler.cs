using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using StockApp.Domain.Abstractions;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Users;

namespace StockApp.Application.UseCases.Authentication.GetUserInfo;

public class Handler(IHttpContextAccessor httpContextAccessor, IUserRepository repository) : IRequestHandler<Command, Result<UserInfoDto>>
{
    public async Task<Result<UserInfoDto>> Handle(Command request, CancellationToken cancellationToken)
    {
        if (httpContextAccessor.HttpContext!.User?.Identity?.IsAuthenticated is false)
            return Result.Failure<UserInfoDto>(new Error("401","O usuário não está autenticado."));        
        
        var email = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)!.Value;
        if (email == null)
            return Result.Failure<UserInfoDto>(new Error("400", "Não foi possível obter as informações do usuário."));
        
        var spec = new GetUserByEmailSpecification(email);
        var user = await repository.GetUserBySpecificationAsync(spec, cancellationToken);

        return user != null 
            ? Result.Success(new UserInfoDto(user.Fullname.FirstName,user.Fullname.LastName, user.Email)) 
            : Result.Failure<UserInfoDto>(new Error("400", "Não foi possível obter as informações do usuário."));
    }
    
}