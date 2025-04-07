using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Application.UseCases.Authentication.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<Result<UserDto>>;