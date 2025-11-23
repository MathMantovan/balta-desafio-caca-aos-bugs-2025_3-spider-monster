using BugStore.Models;

namespace BugStore.Responses.Products
{
    public record GetProductByIdResponse(Product Product, string Message);
}