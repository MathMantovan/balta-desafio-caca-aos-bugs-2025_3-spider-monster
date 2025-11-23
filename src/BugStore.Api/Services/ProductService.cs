using BugStore.Api.Repositories.Interface;
using BugStore.Api.Services.Interface;
using BugStore.Models;

namespace BugStore.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _ProductRepository;
        private readonly VerificationProduct _VerificationProduct = new VerificationProduct();

        public ProductService(IRepository<Product> repository)
        {
            _ProductRepository = repository;
        }

        public async Task<Product> CreateProductAsync(string title, string description, string slug, decimal price, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Slug = slug,
                Price = price
            };

            _VerificationProduct.CreateProductVerificationData(product);
            await _VerificationProduct.ProductExistOnRepository(product, _ProductRepository, cancellationToken);

            await _ProductRepository.AddAsync(product, cancellationToken);
            return product;
        }

        public async Task<Product> DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _ProductRepository.GetByIdAsync(id, cancellationToken);
            if (product is null)
                throw new ArgumentException("Produto não encontrado!");

            await _ProductRepository.RemoveAsync(product, cancellationToken);
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _ProductRepository.GetAllAsync(cancellationToken);
            if (products.Count is 0)
                throw new InvalidOperationException("Nenhum produto Cadastrado");

            return products;
        }

        public async Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _ProductRepository.GetByIdAsync(id, cancellationToken);
            if (product is null)
                throw new ArgumentException("Produto não encontrado!");
            return product;
        }

        public async Task<Product> UpdateProductAsync(Guid id, string? title, string? description, string? slug, decimal? price, CancellationToken cancellationToken)
        {
            var product = await _ProductRepository.GetByIdAsync(id, cancellationToken);
            if (product is null)
                throw new ArgumentException("Produto não encontrado para ser atualizado!");

            product.Title = title ?? product.Title;
            product.Description = description ?? product.Description;   
            product.Slug = slug ?? product.Slug;
            product.Price = price ?? product.Price;

            await _ProductRepository.UpdateAsync(product, cancellationToken);

            return product;    
        }
    }
}
