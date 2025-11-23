using BugStore.Models;
using BugStore.Requests.Orders;

namespace BugStore.Api.Services.Interface
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Order> CreateOrderAsync(
            CreateOrderRequest request, CancellationToken cancellationToken);
    }
}
