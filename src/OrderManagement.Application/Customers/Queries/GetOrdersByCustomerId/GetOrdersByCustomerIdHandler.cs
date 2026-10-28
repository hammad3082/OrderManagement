using MediatR;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Customers.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdHandler : IRequestHandler<GetOrdersByCustomerIdQuery, List<GetOrdersByCustomerIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersByCustomerIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetOrdersByCustomerIdDto>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetOrdersByCustomerIdAsync(request.CustomerId, cancellationToken);

            return orders.
                Select(
                    o => new GetOrdersByCustomerIdDto(
                        o.Id,
                        o.CustomerId,
                        o.OrderDate,
                        o.TotalAmount
                    )
                ).ToList();
        }
    }
}
