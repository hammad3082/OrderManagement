using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Persistence.Repositories;

namespace OrderManagement.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ICustomerRepository? _customers; 
        private IOrderRepository? _orders;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customers => _customers ??= new CustomerRepository(_context);
        public IOrderRepository Orders => _orders ??= new OrderRepository(_context);
        
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
