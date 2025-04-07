using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Users;

namespace StockApp.Application.UseCases.Authentication.GetUserInfo;

public class GetUserInfoQueryHandler(IHttpContextAccessor httpContextAccessor,
    IUserRepository repository) : IRequestHandler<GetUserInfoQuery, Result<UserDto?>>
{
    public async Task<Result<UserDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        if (httpContextAccessor.HttpContext!.User?.Identity?.IsAuthenticated is false)
            return Result.Failure<UserDto>(new Error("401","O usuário não está autenticado."));        
        
        var email = httpContextAccessor.HttpContext?.User!.FindFirst(ClaimTypes.Email)!.Value;
        if (email == null)
            return Result.Failure<UserDto>(new Error("400", "Não foi possível obter as informações do usuário."));
        
        var spec = new GetUserByEmailSpecification(email);
        var user = await repository.GetUserBySpecificationAsync(spec, cancellationToken);

        return user != null 
            ? Result.Success(new UserDto(user.Fullname.FirstName,user.Fullname.LastName, user.Email)) 
            : Result.Failure<UserDto>(new Error("400", "Não foi possível obter as informações do usuário."));
    }
    
}