using BugStore.Models;

namespace BugStore.Responses.Products
{
    public record GetProductResponse(List<Product> Products, string Message);
}