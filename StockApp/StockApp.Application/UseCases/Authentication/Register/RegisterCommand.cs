using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Application.UseCases.Authentication.Register;

public sealed record RegisterCommand(string FirstName, string LastName,string Email, string Password) : IRequest<Result<UserDto>>;
