namespace OrderManagement.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;


        public CustomerProfile? Profile { get; private set; }
        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        private Customer() { }

        public Customer(string name, string email, string address, string phoneNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Profile = new CustomerProfile(Id, address, phoneNumber);
        }

        public Order AddOrder(decimal totalAmount)
        {
            var order = new Order(Id, totalAmount);
            _orders.Add(order);
            return order;
        }
    }
}
