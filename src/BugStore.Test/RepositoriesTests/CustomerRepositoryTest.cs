using BugStore.Api.Repositories.Interface;
using BugStore.Models;

namespace BugStore.Test.RepositoriesTests
{
    public class CustomerRepositoryTest : IRepository<Customer>
    {
        private readonly List<Customer> _Customer = new();
        public Task AddAsync(Customer obj, CancellationToken cancellationToken)
        {
            _Customer.Add(obj);
            return Task.CompletedTask;
        }

        public Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(_Customer);

        public Task<Customer> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            var customer = _Customer.FirstOrDefault(x => x.Id == Id);

            if (customer == null)
                return Task.FromResult<Customer>(null!);

            return Task.FromResult(customer);
        }

        public Task RemoveAsync(Customer obj, CancellationToken cancellationToken)
             => Task.FromResult(_Customer.Remove(obj));


        public Task UpdateAsync(Customer obj, CancellationToken cancellationToken)
        {
            var customer = _Customer.FirstOrDefault(x => x.Id == obj.Id);
            if (customer is not null)
            {
                customer.Name = obj.Name;
                customer.Email = obj.Email;
                customer.Phone = obj.Phone;
                customer.BirthDate = obj.BirthDate;
            }
            return Task.CompletedTask;
        }
    }
}