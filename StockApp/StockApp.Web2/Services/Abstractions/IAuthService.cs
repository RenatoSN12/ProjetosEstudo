using StockApp.Api.DTOs.Requests;

namespace StockApp.Web.Services.Abstractions;

public interface IAuthService
{
    Task LoginAsync(LoginRequest request);
    Task<Result> LogoutAsync();
    Task<Result> RegisterAsync(RegisterRequest request);
}