using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Domain.Entities
{
    internal class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }

        public Customer Customer { get; private set; } = null!;

        private Order() { }

        public Order(Guid customerId, decimal totalAmount)
        {
            if (totalAmount <= 0)
                throw new ArgumentException("Order total amount must be greater than zero.", nameof(totalAmount));

            Id = Guid.NewGuid();
            CustomerId = customerId;
            OrderDate = DateTime.UtcNow;
            TotalAmount = totalAmount;
        }
    }
}
