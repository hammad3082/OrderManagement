using Azure.Core;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;
using Xunit;

namespace OrderManagement.Tests.Infrastructure
{
    public class OrderRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDatabaseContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }

        [Fact]
        public async Task Customer_WithProfile_CanAddOrdersSuccessfully()
        {
            // Arrange
            var context = await GetDatabaseContextAsync();

            var customer = new Customer("Alice Smith", "alice@example.com", "123 MG Road, Bengaluru", "9876543210");
          
            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            var unitOfWork = new UnitOfWork(context);

            // Act 
            var fetchedCustomer = await unitOfWork.Customers.GetCustomerWithProfileAndOrdersAsync(customer.Id);

            fetchedCustomer.Should().NotBeNull();
            fetchedCustomer!.Profile.Should().NotBeNull();
            fetchedCustomer.Profile!.Address.Should().Be("123 MG Road, Bengaluru");

            var order = fetchedCustomer.AddOrder(250.00m);
            await unitOfWork.Orders.AddAsync(order);
            await unitOfWork.SaveChangesAsync();

            // Assert
            var orders = await unitOfWork.Orders.GetOrdersByCustomerIdAsync(customer.Id);
            orders.Should().ContainSingle();
            orders.First().TotalAmount.Should().Be(250.00m);
        }
    }
}
