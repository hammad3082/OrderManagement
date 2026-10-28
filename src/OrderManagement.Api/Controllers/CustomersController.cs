using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Customers.Commands.CreateCustomer;
using OrderManagement.Application.Customers.Queries.GetCustomerById;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ISender _mediator;

        public CustomersController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery(id));

            if (customer == null) return NotFound();

            return Ok(customer);
        }
    }
}
