using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Customers.Commands.CreateCustomer;

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
        public IActionResult GetById(Guid id)
        {
            return Ok();
        }
    }
}
