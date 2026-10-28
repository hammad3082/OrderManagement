using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
