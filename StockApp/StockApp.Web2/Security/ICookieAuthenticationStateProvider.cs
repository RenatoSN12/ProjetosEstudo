using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace StockApp.Web.Security;

public interface ICookieAuthenticationStateProvider
{
    Task<bool> CheckAuthenticatedAsync();
    Task<AuthenticationState> GetAuthenticationStateAsync();
    void NotifyAuthenticationStateChanged();
}