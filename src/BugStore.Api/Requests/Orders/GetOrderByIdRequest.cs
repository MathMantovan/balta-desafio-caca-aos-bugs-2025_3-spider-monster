using BugStore.Responses.Orders;
using MediatR;

namespace BugStore.Requests.Orders
{
    public record GetOrderByIdRequest(Guid Id) : IRequest<GetOrderByIdResponse>;
}
