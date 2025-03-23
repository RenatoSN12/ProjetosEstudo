using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = [];
            
            AddNotifications(name,document,email);
        }

        private readonly List<Subscription> _subscriptions;
        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.ToArray();
        public Name Name { get; set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; } = null!;

        public void AddSubscription(Subscription subscription)
        {
            foreach(var sub in Subscriptions)
                sub.Inactivate();

            _subscriptions.Add(subscription);    
        }


    }
}