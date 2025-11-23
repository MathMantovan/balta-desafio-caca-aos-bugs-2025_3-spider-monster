using BugStore.Api.Services.Interface;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Handlers.Products
{
    public class CreateProductHandler(IProductService _ProductService) : IRequestHandler<CreateProductRequest, CreateProductResponse>
    {

        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _ProductService.CreateProductAsync
                (            
                    request.Title,
                    request.Description,
                    request.Slug,
                    request.Price,
                    cancellationToken
                );
            return new CreateProductResponse(product.Id, product.Title, "Produto criado com sucesso!");
        }
    }
}
