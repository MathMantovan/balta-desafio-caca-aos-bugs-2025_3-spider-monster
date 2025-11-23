using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Requests.Products
{
    public record CreateProductRequest(
        string Title,
        string Description,
        string Slug,
        decimal Price
    ) : IRequest<CreateProductResponse>;
}
