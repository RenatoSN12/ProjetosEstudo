using Dima.Core.Handlers;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class LogoutPage : ComponentBase
{
    #region Dependencies
    
    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    [Inject] public IAccountHandler Handler { get; set; } = null!;
    
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject] public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    
    public bool IsBusy { get; set; }

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        if (await AuthenticationStateProvider.CheckAuthenticatedAsync())
        {
            await Handler.LogoutAsync();
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            AuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }
        await base.OnInitializedAsync();
    }

    #endregion
}