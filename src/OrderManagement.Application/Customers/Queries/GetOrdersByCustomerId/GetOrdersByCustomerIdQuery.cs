using MediatR;

namespace OrderManagement.Application.Customers.Queries.GetOrdersByCustomerId
{
    public record GetOrdersByCustomerIdQuery(
        Guid CustomerId
    ) : IRequest<List<GetOrdersByCustomerIdDto>>;
}
