using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Domain.DTOs.Requests;
using StockApp.Web.Security;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Pages.Authentication;

public partial class LoginPage : ComponentBase
{

    #region "Properties"
    protected bool IsBusy { get; set; } = false;
    protected LoginRequest InputModel { get; set; } = new();

    #endregion

    #region Dependencies
    
    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    [Inject] public IAuthService Handler { get; set; } = null!;
    
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject] public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    #endregion    

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        
        if(user.Identity is { IsAuthenticated: true })
            NavigationManager.NavigateTo("/");
    }

    #endregion

    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.LoginAsync(InputModel);
            if (result.IsSuccess)
            {
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                NavigationManager.NavigateTo("/");
            }
            else
                Snackbar.Add(result.Error.Message, Severity.Error);                
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
    

}