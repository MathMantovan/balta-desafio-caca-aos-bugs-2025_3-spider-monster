using BugStore.Api.Services.Interface;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Handlers.Products
{
    public class GetProductHandler(IProductService _ProductService) : IRequestHandler<GetProductRequest, GetProductResponse>
    {
        public async Task<GetProductResponse> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var products = await _ProductService.GetAllProductsAsync(cancellationToken);

            return new GetProductResponse(products, "A lista de produtos foi encontrada!");
        }
    }
}
