using MediatR;
using StockApp.Domain.Abstractions;

namespace StockApp.Application.UseCases.Authentication.Login;

public sealed record Command(string Email, string Password) : IRequest<Result<Response>>;