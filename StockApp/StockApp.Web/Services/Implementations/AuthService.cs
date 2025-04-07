using System.Net.Http.Json;
using System.Text;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Requests;
using StockApp.Domain.DTOs.Responses;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Services.Implementations;

public class AuthService(IHttpClientFactory httpClientFactory) : IAuthService
{
    private HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Result> LoginAsync(LoginRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/login", request);
        return result.IsSuccessStatusCode
            ? Result.Success("Login realizado com sucesso")
            : Result.Failure(new Error("500", "Não foi possível realizar o login"));
    }

    public async Task LogoutAsync()
    {  
        var emptyContent = new StringContent("", Encoding.UTF8, "application/json"); 
        await _httpClient.PostAsync("api/auth/logout", emptyContent);
    }

    public async Task<Result> RegisterAsync(RegisterRequest request)
    {
       var result = await _httpClient.PostAsJsonAsync("api/auth/register", request);
       
       if (result.IsSuccessStatusCode)
           return Result.Success("Login realizado com sucesso");
       
       var errorResponse = await ErrorManager.ExtractErrorResponse(result);
       return Result.Failure(new Error(errorResponse.Code, errorResponse.Message));
    }
}