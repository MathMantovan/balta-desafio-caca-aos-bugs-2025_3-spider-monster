using BugStore.Api.Repositories.Interface;
using BugStore.Data;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Api.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task AddAsync(Product obj, CancellationToken cancellationToken)
        {
            await _dbContext.Products.AddAsync(obj);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
           return await _dbContext.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Product> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            var customer =  await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == Id, cancellationToken);
            if (customer == null)
                throw null;
            return customer;
        }

        public async Task RemoveAsync(Product obj, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == obj.Id, cancellationToken);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Product obj, CancellationToken cancellationToken)
        {
            _dbContext.Products.Update(obj);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
