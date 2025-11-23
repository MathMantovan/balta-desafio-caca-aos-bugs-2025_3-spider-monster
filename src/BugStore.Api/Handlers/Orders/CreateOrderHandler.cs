using BugStore.Api.Services.Interface;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;
using MediatR;

namespace BugStore.Handlers.Orders
{
    public class CreateOrderHandler(IOrderService _service) : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
    {
        public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _service.CreateOrderAsync(request, cancellationToken);

            return new CreateOrderResponse(order.Id, "Ordem criada com sucesso!");
        }
    }
}
