using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities.Payments
{
    public class CreditCardPayment(
        string cardHolderName,
        string cardNumber,
        string lastTransactionNumber,
        DateTime paidDate,
        DateTime expireDate,
        decimal total,
        decimal totalPaid,
        string payer,
        Document document,
        Address address)
        : Payment(paidDate, expireDate, total, totalPaid, payer, document, address)
    {
        public string CardHolderName { get; private set; } = cardHolderName;
        public string CardNumber { get; private set; } = cardNumber;
        public string LastTransactionNumber { get; private set; } = lastTransactionNumber;
    }
}