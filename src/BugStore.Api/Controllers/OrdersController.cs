using BugStore.Requests.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/v1/orders/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var request = new GetOrderByIdRequest(id);
            var response = await _mediator.Send(request);

            return response is not null
                ? Ok(response)
                : NotFound("Ordem não encontrado.");
        }

        [HttpPost("/v1/orders")]
        public async Task<IActionResult> PostAsync([FromBody] CreateOrderRequest request)
        {
            var response = await _mediator.Send(request);
            return Created($"/v1/orders/{response.Id}", response);
        }

    }
}