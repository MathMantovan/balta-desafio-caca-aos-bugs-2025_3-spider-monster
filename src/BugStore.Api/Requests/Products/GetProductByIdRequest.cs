using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Requests.Products
{
    public record GetProductByIdRequest(Guid Id) : IRequest<GetProductByIdResponse>;
}