using BugStore.Api.Repositories.Interface;
using BugStore.Models;

namespace BugStore.Api.Services
{
    public class VerificationProduct
    {
        public async Task ProductExistOnRepository(Product product, IRepository<Product> repository, CancellationToken cancellationToken)
        {
            var ProductList = await repository.GetAllAsync(cancellationToken);

            if (ProductList.Any
                (c =>
                c.Title == product.Title
                )
              )
                throw new InvalidOperationException("Os dados deste produto ja estão cadastrados!");
        }
        public void CreateProductVerificationData(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Title))
                throw new ArgumentException("Titulo é obrigatório.");

            if (string.IsNullOrWhiteSpace(product.Description))
                throw new ArgumentException("Descrição é obrigatória.");

            if (product.Price <= 0 || product.Price == null)
                throw new ArgumentException("Preço inválido.");

            if (string.IsNullOrWhiteSpace(product.Slug))
                throw new ArgumentException("O Slug é obrigatório");
        }
    }
}
