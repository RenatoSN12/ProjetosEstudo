using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Domain.DTOs.Requests.Authentication;
using StockApp.Web.Extensions;
using StockApp.Web.Security;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Pages.Authentication;

public partial class RegisterPage : ComponentBase
{
    #region "Properties"
    protected bool IsBusy { get; set; } = false;
    protected RegisterRequest InputModel { get; set; } = new();
    
    protected InputType PasswordInput = InputType.Password;
    protected InputType ConfirmPasswordInput = InputType.Password;
    protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
    protected string ConfirmPasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    #endregion

    #region Dependencies

    [Inject] protected ISnackbar Snackbar { get; set; } = null!;

    [Inject] protected IAuthService Handler { get; set; } = null!;

    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

    [Inject] protected ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
            NavigationManager.NavigateTo("/");
    }

    #endregion

    #region Methods

    protected async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            if (InputModel.Password != InputModel.ConfirmPassword)
            {
                Snackbar.Add($"Os campos de senha não coincidem.", Severity.Error);
                return;
            }
            
            var result = await Handler.RegisterAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add("Registro realizado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/");
            }
            else
            {
                foreach (var error in result.Error.Message?.SplitErrors()!)
                    Snackbar.Add(error, Severity.Error);
            }
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

    private void TogglePasswordVisibility(ref InputType inputType, ref string icon)
    {
        if (inputType == InputType.Password)
        {
            inputType = InputType.Text;
            icon = Icons.Material.Filled.Visibility;
        }
        else
        {
            inputType = InputType.Password;
            icon = Icons.Material.Filled.VisibilityOff;
        }
    }

    protected void OnShowPasswordButtonClick()
    {
        TogglePasswordVisibility(ref PasswordInput, ref PasswordInputIcon);
    }

    protected void OnShowConfirmPasswordButtonClick()
    {
        TogglePasswordVisibility(ref ConfirmPasswordInput, ref ConfirmPasswordInputIcon);
    }

    #endregion
}