using BugStore.Api.Repositories.Interface;
using BugStore.Data;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Api.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task AddAsync(Order obj, CancellationToken cancellationToken)
        {
            _appDbContext.Orders.Add(obj);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Order>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.Orders
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
        }

        public async Task<Order> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders
                .Include(x => x.Lines)
                    .ThenInclude(l => l.Product)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == Id, cancellationToken);

            if (order == null)
                return null;

            return order;
        }

        public async Task RemoveAsync(Order obj, CancellationToken cancellationToken)
        {
            var order = await _appDbContext.Orders
                .FirstOrDefaultAsync(p => p.Id == obj.Id, cancellationToken);

            if (order == null)
                throw null;

            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Order obj, CancellationToken cancellationToken)
        {
            _appDbContext.Orders.Update(obj);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
