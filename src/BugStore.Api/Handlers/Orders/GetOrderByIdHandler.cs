using BugStore.Api.Services.Interface;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;
using MediatR;

namespace BugStore.Handlers.Orders
{
    public class GetOrderByIdHandler(IOrderService _service) : IRequestHandler<GetOrderByIdRequest, GetOrderByIdResponse?>
    {
        public async Task<GetOrderByIdResponse?> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
        {
            var order = await _service.GetOrderByIdAsync(request.Id, cancellationToken);

            return new GetOrderByIdResponse(order, "Ordem encontrada com sucesso!");
        }
    }
}
