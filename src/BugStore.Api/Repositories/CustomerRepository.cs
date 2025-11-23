using BugStore.Api.Repositories.Interface;
using BugStore.Data;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Api.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task AddAsync(Customer obj, CancellationToken cancellationToken)
        {
            _appDbContext.Customers.Add(obj);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.Customers
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Customer> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            if (Id == null)
                throw null;

            var customer = await _appDbContext.Customers
               .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            
            if (customer == null)
                throw null;

            return customer;
        }

        public async Task RemoveAsync(Customer obj, CancellationToken cancellationToken)
        {
            _appDbContext.Customers.Remove(obj);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Customer objT, CancellationToken cancellationToken)
        {
            _appDbContext.Customers.Update(objT);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
