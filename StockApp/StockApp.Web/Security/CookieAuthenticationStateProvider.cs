using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Web.Security;

public class CookieAuthenticationStateProvider(IHttpClientFactory clientFactory) : AuthenticationStateProvider, ICookieAuthenticationStateProvider
{
    private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
    private bool _isAuthenticated = false;
    
    public async Task<bool> CheckAuthenticatedAsync()
    {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _isAuthenticated = false;
        try
        {
            var result = await _client.GetFromJsonAsync<Result<UserDto?>>("api/user/info");
            if (result is not null)
            {
                var claims = new[]
                {
                   new Claim(ClaimTypes.Name, $"{result.Value!.Firstname} {result.Value!.Lastname}"),
                   new Claim(ClaimTypes.Email, result.Value.Email)
                };
                
                var identity = new ClaimsIdentity(claims, "Cookies");
                _isAuthenticated = true;
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
        }
        catch
        {
            _isAuthenticated = false;
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        _isAuthenticated = false;
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public void NotifyAuthenticationStateChanged() =>
        base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    
}