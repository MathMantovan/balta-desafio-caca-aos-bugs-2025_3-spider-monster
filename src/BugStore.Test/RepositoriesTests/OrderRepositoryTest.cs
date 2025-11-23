using BugStore.Api.Repositories.Interface;
using BugStore.Models;

namespace BugStore.Test.RepositoriesTests
{
    public class OrderRepositoryTest : IRepository<Order>
    {
        private List<Order> _orders = new List<Order>();
        public Task AddAsync(Order obj, CancellationToken cancellationToken)
        {
            _orders.Add(obj);
            return Task.CompletedTask;
        }

        public Task<List<Order>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
            => Task.FromResult(_orders.FirstOrDefault(x => x.Id == Id));
        

        public Task RemoveAsync(Order obj, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order obj, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
