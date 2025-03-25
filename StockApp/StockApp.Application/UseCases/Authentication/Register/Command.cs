using MediatR;
using StockApp.Domain.Abstractions;

namespace StockApp.Application.UseCases.Authentication.Register;

public sealed record Command(string Username, string Email, string Password) : IRequest<Result<Response>>;
