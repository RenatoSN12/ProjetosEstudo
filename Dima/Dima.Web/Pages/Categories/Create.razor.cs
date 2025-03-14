using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

public partial class CreateCategoryPage : ComponentBase
{
    #region Fields

    public bool IsBusy { get; set; } = false;
    public CreateCategoryRequest InputModel { get; set; } = new();

    #endregion

    #region Services

    [Inject] private ICategoryHandler Handler { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    
    #endregion
    
    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.CreateAsync(InputModel);
            if (result.IsSuccess)
                NavigationManager.NavigateTo("/categorias");
            else
                Snackbar.Add(result.Message ?? "Não foi possível criar a categoria.", Severity.Error);
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

    #endregion
    
}
    