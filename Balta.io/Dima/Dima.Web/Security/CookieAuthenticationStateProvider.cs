using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Dima.Core.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Security;

public class CookieAuthenticationStateProvider(IHttpClientFactory clientFactory)
    : AuthenticationStateProvider, ICookieAuthenticationStateProvider
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
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        var userInfo = await GetUser();
        if(userInfo is null)
            return new AuthenticationState(user);

        var claims = await GetClaims(userInfo);
        
        var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
        user = new ClaimsPrincipal(id);
        
        _isAuthenticated = true;
        return new AuthenticationState(user);
    }

    public void NotifyAuthenticationStateChanged() =>
        base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    private async Task<User?> GetUser()
    {
        try
        {
            return await _client.GetFromJsonAsync<User?>("v1/identity/manage/info");
        }
        catch
        {
            return null;
        }
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Email, user.Email)
        };

        claims.AddRange(user.Claims
            .Where(x =>
                x.Key != ClaimTypes.Name &&
                x.Key != ClaimTypes.Email)
            .Select(r => new Claim(r.Key, r.Value)));

        RoleClaim[]? roles;

        try
        {
            roles = await _client.GetFromJsonAsync<RoleClaim[]>("v1/identity/roles");
        }
        catch
        {
            return claims;
        }

        claims.AddRange(from role in roles ?? []
            where !string.IsNullOrEmpty(role.Type) &&
                  !string.IsNullOrEmpty(role.Value)
            select new Claim(ClaimTypes.Role, role.Value));

        return claims;
    }
}