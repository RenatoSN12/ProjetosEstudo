using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions;

public partial class ListTransactionsPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<Transaction> Transactions { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;
    public int CurrentYear { get; set; } = DateTime.Today.Year;
    public int CurrentMonth { get; set; } = DateTime.Today.Month;

    public int[] Years { get; set; } =
    [
        DateTime.Today.Year,
        DateTime.Today.AddYears(-1).Year,
        DateTime.Today.AddYears(-2).Year,
        DateTime.Today.AddYears(-3).Year
    ];

    #endregion

    #region Services

    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public ITransactionHandler Handler { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync() => await GetTransactionsAsync();

    #endregion

    #region Methods

    public Func<Transaction, bool> Filter => transaction =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        return transaction.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
               transaction.Title.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase);
    };

    private async Task GetTransactionsAsync()
    {
        try
        {
            IsBusy = true;

            var request = new GetTransactionsByPeriodRequest()
            {
                StartDate = DateTime.Now.GetFirstDay(CurrentYear, CurrentMonth),
                EndDate = DateTime.Now.GetLastDay(CurrentYear, CurrentMonth),
                PageNumber = 1,
                PageSize = 1000,
            };

            var result = await Handler.GetByPeriodAsync(request);
            if (result.IsSuccess)
                Transactions = result.Data ?? [];
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

    private async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            IsBusy = true;
            
            var result = await Handler.DeleteAsync(new DeleteTransactionRequest
            {
                Id = id
            });

            if (result.IsSuccess)
            {
                Snackbar.Add($"Lançamento '{title}' foi removido.", Severity.Success);
                Transactions.RemoveAll(x=>x.Id == id);
            }
            else
            {
                Snackbar.Add(result.Message ?? "Não foi possível deletar a categoria.", Severity.Error);
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

    protected async Task OnSearchAsync()
    {
        await GetTransactionsAsync();
        StateHasChanged();
    }

    protected async Task OnDeleteButtonClickedAsync(long id, string title)
    {
        var result = await DialogService.ShowMessageBox("Atenção!",
            "Deseja realmente excluir o lançamento?",
            "Confirmar", "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, title);
    }

    #endregion
}