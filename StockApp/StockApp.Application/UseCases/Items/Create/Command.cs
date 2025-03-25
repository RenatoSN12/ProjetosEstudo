using MediatR;
using StockApp.Domain.Abstractions;

namespace StockApp.Application.UseCases.Items.Create;

public sealed record Command(string Title) : IRequest<Result<Response>>;
