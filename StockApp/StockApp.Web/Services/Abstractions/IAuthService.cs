using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Requests.Authentication;

namespace StockApp.Web.Services.Abstractions;

public interface IAuthService
{
    Task<Result> LoginAsync(LoginRequest request);
    Task LogoutAsync();
    Task<Result> RegisterAsync(RegisterRequest request);
}