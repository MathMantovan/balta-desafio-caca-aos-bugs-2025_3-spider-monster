using BugStore.Models;

namespace BugStore.Api.Services.Interface
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
        Task<Product> CreateProductAsync(
            string title, string description, string slug, decimal price, CancellationToken cancellationToken);
        Task<Product> UpdateProductAsync(
            Guid id, string? title, string? description, string? slug, decimal? price, CancellationToken cancellationToken);
        Task<Product> DeleteProductAsync(Guid id, CancellationToken cancellationToken);
    }
}

