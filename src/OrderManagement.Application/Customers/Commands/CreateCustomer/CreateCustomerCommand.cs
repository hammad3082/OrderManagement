using MediatR;

namespace OrderManagement.Application.Customers.Commands.CreateCustomer
{
    public record CreateCustomerCommand(
        string Name,
        string Email,
        string Address, 
        string PhoneNumber
    ) : IRequest<Guid>;
}
