using BugStore.Api.Services.Interface;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Handlers.Products
{
    public class GetProductByIdHandler(IProductService _ProductService) : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse?>
    {
        public async Task<GetProductByIdResponse?> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        { 
            var product = await _ProductService.GetProductByIdAsync(request.Id, cancellationToken);

            return new GetProductByIdResponse(product, "Produto encontrado!");
        }
    }
}