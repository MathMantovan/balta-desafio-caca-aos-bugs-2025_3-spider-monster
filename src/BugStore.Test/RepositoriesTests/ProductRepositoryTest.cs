using BugStore.Api.Repositories.Interface;
using BugStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugStore.Test.RepositoriesTests
{
    public class ProductRepositoryTest : IRepository<Product>
    {
        private readonly List<Product> _Product = new();

        public Task AddAsync(Product obj, CancellationToken cancellationToken)
        {
            _Product.Add(obj);
            return Task.CompletedTask;
        }

        public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
                => Task.FromResult(_Product);
        

        public Task<Product> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
                => Task.FromResult(_Product.FirstOrDefault(x => x.Id == Id));
        

        public Task RemoveAsync(Product obj, CancellationToken cancellationToken)
                => Task.FromResult(_Product.Remove(obj));

        public Task UpdateAsync(Product obj, CancellationToken cancellationToken)
        {
            var product = _Product.FirstOrDefault(x => x.Id == obj.Id);
            if (product is not null)
            {
                product.Title = obj.Title;
                product.Description = obj.Description;
                product.Slug = obj.Slug;
                product.Price = obj.Price;
            }
            return Task.CompletedTask;
        }
    }
}
