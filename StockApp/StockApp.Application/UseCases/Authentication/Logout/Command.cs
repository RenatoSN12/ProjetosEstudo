using MediatR;
using StockApp.Domain.Abstractions;

namespace StockApp.Application.UseCases.Authentication.Logout;

public sealed record Command : IRequest<Result<Response>>;