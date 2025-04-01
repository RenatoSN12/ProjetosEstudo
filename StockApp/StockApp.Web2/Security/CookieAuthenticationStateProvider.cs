using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using StockApp.Domain.Entities;

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
            var userEmail = await _client.GetFromJsonAsync<string>("v1/user/email");
            if (!string.IsNullOrEmpty(userEmail))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, userEmail) };
                var identity = new ClaimsIdentity(claims, "Cookies");
                _isAuthenticated = true;
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
        }
        catch (Exception ex)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public void NotifyAuthenticationStateChanged() =>
        base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    
}