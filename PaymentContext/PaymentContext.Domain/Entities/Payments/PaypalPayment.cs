using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities.Payments
{
    public class PaypalPayment(
        Email email,
        string transactionCode,
        DateTime paidDate,
        DateTime expireDate,
        decimal total,
        decimal totalPaid,
        string payer,
        Document document,
        Address address)
        : Payment(paidDate, expireDate, total, totalPaid, payer, document, address)
    {
        public Email Email { get; private set; } = email;
        public string TransactionCode { get; private set; } = transactionCode;
    }
}