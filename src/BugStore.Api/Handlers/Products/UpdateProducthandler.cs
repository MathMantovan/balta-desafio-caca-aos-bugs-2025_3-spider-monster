using BugStore.Api.Services.Interface;
using BugStore.Commands.Products;
using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Handlers.Products
{
    public class UpdateProductHandler(IProductService _ProductService) : IRequestHandler<UpdateProductCommand, UpdateProductResponse?>
    {
        public async Task<UpdateProductResponse?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
           var product = await _ProductService.UpdateProductAsync
                (
                    request.Id,
                    request.Data.Title,
                    request.Data.Description,
                    request.Data.Slug,
                    request.Data.Price,
                    cancellationToken

                );

            return new UpdateProductResponse(product.Id, product.Title, "O Produto foi atualizado com sucesso!");
        }
    }
}
