using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Requests.Products
{
    public record DeleteProductRequest(Guid Id) : IRequest<DeleteProductResponse>;
}
