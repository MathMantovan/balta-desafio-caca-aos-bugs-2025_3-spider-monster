using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Commands.Products;
    public record UpdateProductCommand(Guid Id, UpdateProductRequest Data)
        : IRequest<UpdateProductResponse>;
