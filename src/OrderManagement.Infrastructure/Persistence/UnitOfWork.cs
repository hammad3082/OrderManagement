using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Persistence.Repositories;
using System.Collections.Concurrent;

namespace OrderManagement.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ICustomerRepository? _customers;
    
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customers => _customers ??= new CustomerRepository(_context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
