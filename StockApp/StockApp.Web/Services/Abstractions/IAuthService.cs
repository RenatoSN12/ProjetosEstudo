using StockApp.Web.DTOs.Requests;
using StockApp.Web.DTOs.Responses;

namespace StockApp.Web.Services.Abstractions;

public interface IAuthService
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task LogoutAsync();
    Task<Response<string>> RegisterAsync(RegisterRequest request);
}