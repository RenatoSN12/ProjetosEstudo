using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

public partial class EditCategoryPage : ComponentBase
{
    #region Properties

    protected bool IsBusy { get; set; } = false;
    protected UpdateCategoryRequest InputModel { get; set; } = new();

    #endregion

    #region Parameters

    [Parameter] public string Id { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected ICategoryHandler Handler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        GetCategoryByIdRequest? request = null;
        
        try
        {
            request = new GetCategoryByIdRequest
            {
                Id = long.Parse(Id)
            };
        }
        catch
        {
            Snackbar.Add("Parâmetro inválido.", Severity.Error);            
        }

        if (request is null)
            return;

        try
        {
            IsBusy = true;
            var response = await Handler.GetByIdAsync(request);

            if (response is { IsSuccess: true, Data: not null })
            {
                InputModel = new UpdateCategoryRequest
                {
                    Id = response.Data.Id,
                    Title = response.Data.Title,
                    Description = response.Data.Description
                };
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

    #endregion
    
    #region Methods
    
    protected async Task OnValidSubmitAsync()
    {
        try
        {
            IsBusy = true;
            var result = await Handler.UpdateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add("Categoria atualizada.", Severity.Success);
                NavigationManager.NavigateTo("/categorias");
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
    #endregion
}