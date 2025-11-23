using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Requests.Products
{
    public record UpdateProductRequest(
        string Title,
        string Description,
        string Slug,
        decimal Price
    ) : IRequest<UpdateProductResponse>;
}
