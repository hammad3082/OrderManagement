using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Customers.Commands.CreateCustomer;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Tests.Application.Validators
{
    public class CreateCustomerCommandValidatorTests
    {
        private async Task<ApplicationDbContext> GetDatabaseContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateCustomerCommandValidator_ShouldFail_WhenEmailIsInvalid()
        {
            // Arrange
            var context = await GetDatabaseContextAsync();
            var unitOfWork = new UnitOfWork(context);

            // Assuming you have a CreateCustomerCommandValidator
            var validator = new CreateCustomerCommandValidator();

            var command = new CreateCustomerCommand("John Doe", "invalid-email-format", "Bengaluru", "9876543211");

            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Email");
        }
    }
}
