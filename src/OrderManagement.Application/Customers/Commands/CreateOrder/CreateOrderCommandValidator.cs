using FluentValidation;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Customers.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("Order total must be greater than zero.");

            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .MustAsync(async (customerId, cancellationToken) =>
                {
                    return await unitOfWork.Customers.ExistsAsync(customerId, cancellationToken);
                }).WithMessage("Customer must exist before creating an order.");
        }
    }
}
