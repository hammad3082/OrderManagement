using OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerWithProfileAndOrdersAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    }
}
