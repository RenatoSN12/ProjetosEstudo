using System.Net.Http.Json;
using StockApp.Api.DTOs.Requests;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Services.Implementations;

public class AuthService(IHttpClientFactory httpClientFactory) : IAuthService
{
    private HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Result> LoginAsync(LoginRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/auth/login", request);
        return result.IsSuccessStatusCode
            ? Result.Success()
            : Result.Failure(new Error("500", "Falha ao realizar o login, erro interno no servidor."));
    }

    public Task<Result> LogoutAsync()
    {
        
        
    }

    public Task<Result> RegisterAsync(RegisterRequest request)
    {
        throw new NotImplementedException();
    }
}