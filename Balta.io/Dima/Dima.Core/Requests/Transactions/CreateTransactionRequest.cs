using System.ComponentModel.DataAnnotations;
using Dima.Core.Enums;

namespace Dima.Core.Requests.Transactions;

public class CreateTransactionRequest : Request
{
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Type is required.")]
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

    [Required(ErrorMessage = "Transaction value is required.")]
    public decimal Amount { get; set; }
    
    [Required(ErrorMessage = "Category is required.")]
    public long CategoryId { get; set; }

    [Required(ErrorMessage = "Date is required.")]
    public DateTime? PaidOrReceivedAt { get; set; }
}