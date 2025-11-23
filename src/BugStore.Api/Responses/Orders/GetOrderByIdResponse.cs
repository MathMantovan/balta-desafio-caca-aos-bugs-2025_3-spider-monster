using BugStore.Models;

namespace BugStore.Responses.Orders
{
    public record GetOrderByIdResponse(Order Order, string Message);
}
