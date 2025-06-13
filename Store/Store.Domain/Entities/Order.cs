using System.Runtime;
using Store.Domain.Enums;

namespace Store.Domain.Entities
{
    public class Order : Entity
    {
        public Order(Customer customer, decimal deliveryFee, Discount discount)
        {
            Customer = customer;
            Date = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0, 8);
            Status = EOrderStatus.WaitingPayment;
            DeliveryFee = deliveryFee;
            Discount = discount;
            _items = [];
        }

        public Customer Customer { get; private set; } = null!;
        public DateTime Date { get; private set; }
        public string Number { get; private set; } = string.Empty;
        private IList<OrderItem> _items = [];
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public decimal DeliveryFee { get; private set; }
        public Discount? Discount { get; private set; }
        public EOrderStatus Status { get; private set; }
        public void AddItem(Product product, int quantity) => _items.Add(new OrderItem(product, quantity));

        public decimal Total()
        {
            var total = Items.Sum(x => x.Total());
            total += DeliveryFee;
            total -= Discount != null ? Discount.Value() : 0;

            return total;
        }

        public void Pay(decimal amount)
        {
            if (amount == Total())
                this.Status = EOrderStatus.WaitingDelivery;
        }

        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
        }
    }
}