using MediatR;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetCustomerByIdDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customers.GetCustomerWithProfileAndOrdersAsync(request.Id, cancellationToken);

            if (customer == null) return null;

            return new GetCustomerByIdDto(
                customer.Id,
                customer.Name,
                customer.Email,
                customer.Profile != null ? new CustomerProfileDto(customer.Profile.Address, customer.Profile.PhoneNumber) : null,
                customer.Orders.Select(o => new OrderDto(o.Id, o.OrderDate, o.TotalAmount)).ToList()
            );
        }
    }
}
