using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;

namespace StockApp.Application.UseCases.Authentication.Logout;

public sealed record LogoutCommand : IRequest<Result>;