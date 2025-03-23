using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities.Payments
{
    public abstract class Payment(
        DateTime paidDate,
        DateTime expireDate,
        decimal total,
        decimal totalPaid,
        string payer,
        Document document,
        Address address)
    {
        public string Number { get; private set; } = Guid.NewGuid().ToString().Replace("-","").Substring(0,10).ToUpper();
        public DateTime PaidDate { get; private set; } = paidDate;
        public DateTime ExpireDate { get; private set; } = expireDate;
        public decimal Total { get; private set; } = total;
        public decimal TotalPaid { get; private set; } = totalPaid;
        public string Payer { get; private set; } = payer;
        public Document Document { get; private set; } = document;
        public Address Address { get; private set; } = address;
    }
}