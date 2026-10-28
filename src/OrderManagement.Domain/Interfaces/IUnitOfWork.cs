namespace OrderManagement.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }

        IOrderRepository Orders { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
