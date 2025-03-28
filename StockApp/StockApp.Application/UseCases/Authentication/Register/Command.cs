using MediatR;
using StockApp.Domain.Abstractions;

namespace StockApp.Application.UseCases.Authentication.Register;

public sealed record Command(string FirstName, string LastName,string Email, string Password) : IRequest<Result<Response>>;
