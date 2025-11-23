using BugStore.Api.Services.Interface;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Handlers.Products
{
    public class DeleteProductHandler(IProductService _ProductService) : IRequestHandler<DeleteProductRequest, DeleteProductResponse?>
    {
        public async Task<DeleteProductResponse?> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
           var product = await _ProductService.DeleteProductAsync(request.Id, cancellationToken);

            return new DeleteProductResponse(product.Id, "Produto removido com sucesso.");
        }
    }
}
