using MediatR;

namespace OrderManagement.Application.Customers.Commands.CreateOrder
{
    public record CreateOrderCommand(
        Guid CustomerId,
        decimal TotalAmount
    ) : IRequest<Guid>;
}