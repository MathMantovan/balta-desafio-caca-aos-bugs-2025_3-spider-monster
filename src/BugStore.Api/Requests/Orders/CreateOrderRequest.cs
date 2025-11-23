using BugStore.Responses.Orders;
using MediatR;

namespace BugStore.Requests.Orders
{
    public record CreateOrderRequest(
        Guid CustomerId,
        List<CreateOrderLineRequest> Lines
    ) : IRequest<CreateOrderResponse>;

    public record CreateOrderLineRequest(
        Guid ProductId,
        int Quantity
    );
}
