using MediatR;
using StockApp.Domain.Abstractions;

namespace StockApp.Application.UseCases.Authentication.GetUserEmail;

public sealed record Command : IRequest<Result<Response>>;