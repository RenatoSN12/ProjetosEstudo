using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

public partial class ListCategoriesPage : ComponentBase
{
    #region Fields

    protected bool IsBusy { get; set; } = false;
    protected List<Category> Categories { get; set; } = [];

    public string SearchTerm { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected ICategoryHandler Handler { get; set; } = null!;

    [Inject] protected IDialogService DialogService { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        try
        {
            IsBusy = true;
            var request = new GetAllCategoriesRequest();
            var result = await Handler.GetAllAsync(request);

            Categories = result.Data ?? [];
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

    #region Methods

    public async void OnDeleteButtonClickedAsync(long id, string title)
    {
        var result = await DialogService.ShowMessageBox(
            "Atenção!",
            $"Deseja excluir a categoria {title}?",
            "Confirmar",
            cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, title);

        StateHasChanged();
    }

    private async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            await Handler.DeleteAsync(new DeleteCategoryRequest { Id = id });
            Categories.RemoveAll(c => c.Id == id);
            Snackbar.Add($"Categoria {title} excluida com sucesso!", Severity.Success);
        }
        catch
        {
            Snackbar.Add("Erro inesperado. Não foi possível excluir a categoria.", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    public Func<Category, bool> Filter => category =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        if (category.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (category.Title.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (category.Description is not null &&
            category.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };

    #endregion
}