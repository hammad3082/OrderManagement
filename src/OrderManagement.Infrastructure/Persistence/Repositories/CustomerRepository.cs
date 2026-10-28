using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetCustomerWithProfileAndOrdersAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .Include(c => c.Profile)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);
        }
        public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            await _context.Customers.AddAsync(customer, cancellationToken);
        }
    }
}
