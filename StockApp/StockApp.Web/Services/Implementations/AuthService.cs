using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using StockApp.Web.DTOs.Requests;
using StockApp.Web.DTOs.Responses;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Services.Implementations;

public class AuthService(IHttpClientFactory httpClientFactory) : IAuthService
{
    private HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/auth/login", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Login realizado com sucesso")
            : new Response<string>("Não foi possível realizar o login", statusCode: 500);
    }

    public async Task LogoutAsync()
    {  
        var emptyContent = new StringContent("", Encoding.UTF8, "application/json"); 
        await _httpClient.PostAsync("v1/auth/logout", emptyContent);
    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
       var result = await _httpClient.PostAsJsonAsync("v1/auth/register", request);
       
       if (result.IsSuccessStatusCode)
           return new Response<string>("Login realizado com sucesso");
       
       var errorResponse = await ErrorManager.ExtractErrorResponse(result);
       return new Response<string>(errorResponse.Message, int.Parse(errorResponse.Code));
    }
}