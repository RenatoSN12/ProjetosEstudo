using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class LoginPage : ComponentBase
{
    #region Dependencies
    
    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    [Inject] public IAccountHandler Handler { get; set; } = null!;
    
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject] public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    #endregion
    
    #region Properties

    protected bool IsBusy { get; set; } = false;
    protected LoginRequest InputModel { get; set; } = new();
    
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
                Snackbar.Add(result.Message ?? "Não foi possível concluir o registro do usuário.", Severity.Error);                
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