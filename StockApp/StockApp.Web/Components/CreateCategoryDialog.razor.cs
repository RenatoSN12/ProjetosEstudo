using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Domain.DTOs.Requests.Categories;
using StockApp.Web.Extensions;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Components;

public partial class CreateCategoryDialogBase : ComponentBase
{
    #region Parameters

    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public CreateCategoryDto Model { get; set; } = new();

    #endregion

    #region Properties

    protected bool IsBusy { get; set; }
    
    #endregion
    
    #region Services

    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] public ICategoryService CategoryService { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    
    #endregion

    protected async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await CategoryService.CreateCategoryAsync(Model);
            if (result.IsSuccess)
                NavigationManager.NavigateTo("/categories");
            else
            {
                foreach (var error in result.Error.Message.SplitErrors())
                {
                    Snackbar.Add(error, Severity.Error);
                }
            }
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    protected void Cancel()
    {
        MudDialog.Close(DialogResult.Cancel());
    }
}