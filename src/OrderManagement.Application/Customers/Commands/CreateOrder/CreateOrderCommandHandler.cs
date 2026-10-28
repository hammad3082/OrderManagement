using MediatR;
using Microsoft.Extensions.Logging;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Customers.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateOrderCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId, cancellationToken);

            //if (customer == null)
            //    throw new KeyNotFoundException($"Customer with ID {request.CustomerId} was not found.");

            // Safe to call directly because FluentValidation already guaranteed the customer exists
            var order = customer!.AddOrder(request.TotalAmount);

            await _unitOfWork.Orders.AddAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully created Order {OrderId} for Customer {CustomerId}", order.Id, request.CustomerId);
            return order.Id;
        }
    }
}
