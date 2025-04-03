using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Application.UseCases.Authentication.GetUserInfo;

public sealed record Command : IRequest<Result<UserInfoDto>>;