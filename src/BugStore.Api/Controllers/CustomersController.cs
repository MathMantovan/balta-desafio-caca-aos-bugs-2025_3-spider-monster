using Microsoft.AspNetCore.Mvc;
using MediatR;
using BugStore.Requests.Customers;
using BugStore.Api.Requests.Customers;

namespace BugStore.Controllers
{
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/v1/customers")]
        public async Task<IActionResult> GetAsync()
        {
            var request = new GetCustomerRequest();
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        
        [HttpGet("/v1/customers/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id)
        {
            var request = new GetCustomerByIdRequest(id);
            var response = await _mediator.Send(request);
            return  response is not null 
                ? Ok(response) 
                : NotFound("Cliente não encontrado.");
        }

        [HttpPost("/v1/customers/")]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateCustomerRequest request)
        {            
            var response = await _mediator.Send(request);
            return Created(response.ToString(), request);
        }

        [HttpPut("/v1/customers/{id:guid}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] UpdateCustomerRequest customerRequest)
        {
            var request = new UpdateCustomerCommand(id, customerRequest);
            var response = await _mediator.Send(request); 

            return response is not null 
                ? Ok(response) 
                : NotFound("Cliente não encontrado para ser atualizado.");
        }        
    
        [HttpDelete("/v1/customers/{id:guid}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id)
        {
            var request = new DeleteCustomerRequest(id);
            var response = await _mediator.Send(request);

            return response is not null 
                ? Ok(response) 
                : NotFound("Cliente não encontrado para ser removido.");
        }
    }
}