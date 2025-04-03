using StockApp.Domain.Abstractions;
using StockApp.Domain.DTOs.Requests;

namespace StockApp.Web.Services.Abstractions;

public interface IAuthService
{
    Task<Result> LoginAsync(LoginRequest request);
    Task LogoutAsync();
    Task<Result> RegisterAsync(RegisterRequest request);
}