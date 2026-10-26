using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Domain.Entities
{
    internal class CustomerProfile
    {
        public Guid CustomerId { get; private set; }
        public string Address { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;

        public Customer Customer { get; private set; } = null!;

        private CustomerProfile() { }

        public CustomerProfile(Guid customerId, string address, string phoneNumber)
        {
            CustomerId = customerId;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }
}
