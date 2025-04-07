using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Application.UseCases.Authentication.GetUserInfo;

public sealed record GetUserInfoQuery : IRequest<Result<UserDto?>>;