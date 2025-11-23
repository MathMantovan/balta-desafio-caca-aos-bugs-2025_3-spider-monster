using BugStore.Commands.Products;
using BugStore.Requests.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/v1/products")]
        public async Task<IActionResult> GetAsync()
        {
            var request = new GetProductRequest();
            var response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpGet("/v1/products/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var request = new GetProductByIdRequest(id);
            var response = await _mediator.Send(request);

            return response is not null
                ? Ok(response)
                : NotFound("Produto não encontrado.");
        }

        [HttpPost("/v1/products")]
        public async Task<IActionResult> PostAsync([FromBody] CreateProductRequest request)
        {
            var response = await _mediator.Send(request);
            return Created($"/v1/products/{response.Id}", response);
        }

        [HttpPut("/v1/products/{id:guid}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateProductRequest productRequest)
        {
            var request = new UpdateProductCommand(id, productRequest);
            var response = await _mediator.Send(request);

            return response is not null
                ? Ok(response)
                : NotFound("Produto não encontrado para ser atualizado.");
        }

        [HttpDelete("/v1/products/{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var request = new DeleteProductRequest(id);
            var response = await _mediator.Send(request);

            return response is not null
                ? Ok(response)
                : NotFound("Produto não encontrado para ser removido.");
        }
    }
}