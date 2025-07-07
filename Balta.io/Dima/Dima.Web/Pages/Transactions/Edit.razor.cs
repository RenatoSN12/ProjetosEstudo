using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions;

public partial class EditTransactionPage : ComponentBase
{
    #region Fields

    [Parameter] public string Id { get; set; } = string.Empty;
    protected bool IsBusy { get; set; } = false;
    protected UpdateTransactionRequest InputModel { get; set; } = new();
    public List<Category> Categories { get; set; } = [];

    #endregion
    
    #region Services

    [Inject] private ITransactionHandler TransactionHandler { get; set; } = null!;
    [Inject] private ICategoryHandler CategoryHandler { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    
    #endregion

    #region Methods

    private async Task GetCategoriesAsync()
    {
        try
        {
            IsBusy = true;

            var request = new GetAllCategoriesRequest();
            var result = await CategoryHandler.GetAllAsync(request);

            if (result.IsSuccess)
            {
                Categories = result.Data ?? [];
                InputModel.CategoryId = Categories.FirstOrDefault()?.Id ?? 0;
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
    private async Task GetTransactionByIdAsync()
    {
        GetTransactionByIdRequest? request = null;

        try
        {
            request = new GetTransactionByIdRequest { Id = long.Parse(Id) };
        }
        catch
        {
            Snackbar.Add("Parâmetro inválido", Severity.Error);
        }

        if (request is null)
            return;

        try
        {
            IsBusy = true;

            var result = await TransactionHandler.GetByIdAsync(request);

            if (result is { IsSuccess: true, Data: not null })
            {
                InputModel = new UpdateTransactionRequest
                {
                    CategoryId = result.Data.CategoryId,
                    PaidOrReceivedAt = result.Data.PaidOrReceivedAt,
                    Title = result.Data.Title,
                    Type = result.Data.Type,
                    Amount = result.Data.Amount,
                    Id = result.Data.Id,
                };

                InputModel.CategoryId = Categories.FirstOrDefault()?.Id ?? 0;
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
    protected async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await TransactionHandler.UpdateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message ?? "Lançamento atualizado.", Severity.Success);
                NavigationManager.NavigateTo("/lancamentos/historico");
            }
            else
            {
                Snackbar.Add(result.Message ?? "Não foi possível atualizar a categoria.", Severity.Error);
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

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        
        await GetTransactionByIdAsync();
        await GetCategoriesAsync();
        
        IsBusy = false;
    }

    #endregion
}