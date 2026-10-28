using MediatR;

namespace OrderManagement.Application.Customers.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<GetCustomerByIdDto?>;
}
