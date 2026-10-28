using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Customers.Commands.CreateOrder;
using OrderManagement.Application.Customers.Queries.GetOrdersByCustomerId;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _mediator;

        public OrdersController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var id = await _mediator.Send(command);
            
            return Ok(id);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(Guid customerId)
        {
            var orders = await _mediator.Send(new GetOrdersByCustomerIdQuery(customerId));
            
            return Ok(orders);
        }
    }
}
