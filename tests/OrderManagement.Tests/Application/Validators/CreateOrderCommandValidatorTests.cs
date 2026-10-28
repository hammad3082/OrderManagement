using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Customers.Commands.CreateOrder;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Tests.Application.Validators
{
    public class CreateOrderCommandValidatorTests
    {
        private async Task<ApplicationDbContext> GetDatabaseContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Validate_ShouldFail_WhenCustomerDoesNotExist()
        {
            // Arrange
            var context = await GetDatabaseContextAsync();
            var unitOfWork = new UnitOfWork(context);
            var validator = new CreateOrderCommandValidator(unitOfWork);
            var command = new CreateOrderCommand(Guid.NewGuid(), 100.00m);

            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Customer must exist before creating an order.");
        }

        [Fact]
        public async Task Validate_ShouldPass_WhenCustomerExists()
        {
            // Arrange
            var context = await GetDatabaseContextAsync();

            var customer = new Customer("Bob Builder", "bob@example.com", "Bengaluru", "9876543211");

            context.Customers.Add(customer);

            await context.SaveChangesAsync();

            var unitOfWork = new UnitOfWork(context);
            var validator = new CreateOrderCommandValidator(unitOfWork);
            var command = new CreateOrderCommand(customer.Id, 150.00m);

            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
