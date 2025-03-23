using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities.Payments
{
    public class BoletoPayment(
        string barcode,
        Email email,
        string boletoNumber,
        DateTime paidDate,
        DateTime expireDate,
        decimal total,
        decimal totalPaid,
        string payer,
        Document document,
        Address address)
        : Payment(paidDate, expireDate, total, totalPaid, payer, document, address)
    {
        public string Barcode { get; private set; } = barcode;
        public Email Email { get; private set; } = email;
        public string BoletoNumber { get; private set; } = boletoNumber;
    }
}