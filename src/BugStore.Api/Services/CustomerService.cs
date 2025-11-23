using BugStore.Api.Repositories.Interface;
using BugStore.Api.Services.Interface;
using BugStore.Models;

namespace BugStore.Api.Services
{
    public class CustomerService : ICustomerService

    {
        private readonly IRepository<Customer> _Repository;
        private readonly VerificationCustomer _verificationCustomer = new VerificationCustomer();
        public CustomerService(IRepository<Customer> repository)
        {
            _Repository = repository;
        }


        public async Task<Customer> CreateCustomerAsync(string name, string email, string phone, DateTime birthDate, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Phone = phone,
                BirthDate = birthDate
            };

            _verificationCustomer.CreateCustomerVerificationData(name, email, birthDate);
            await _verificationCustomer.CustomerAlreadyExistOnRepository(customer, _Repository, cancellationToken);
            
            await _Repository.AddAsync(customer, cancellationToken);
            return customer;
        }

        public async Task<Customer> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _Repository.GetByIdAsync(id, cancellationToken);
            if (customer is null)
                throw new ArgumentException("Cliente não encontrado!");

            await _Repository.RemoveAsync(customer, cancellationToken);
            return customer;
        }

        public async Task<List<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken)
        {
            var customers = await _Repository.GetAllAsync(cancellationToken);

            if (customers.Count is 0)
                throw new InvalidOperationException("Nenhum Cliente Cadastrado");

            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _Repository.GetByIdAsync(id, cancellationToken);

            if (customer is null)
                throw new ArgumentException("Cliente não encontrado!");

            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Guid id, string? name, string? email, string? phone, DateTime birthDate, CancellationToken cancellationToken)
        {
            var customer = await _Repository.GetByIdAsync(id, cancellationToken);
            if (customer is null)
                throw new ArgumentException("Cliente não encontrado para ser atualizado!");
           
            customer.Name = name ?? customer.Name;
            customer.Email = email ?? customer.Email;
            customer.Phone = phone ?? customer.Phone;       
           
            if(customer.BirthDate != birthDate)
                customer.BirthDate = birthDate;

            await _Repository.UpdateAsync(customer, cancellationToken);

            return customer;
        }
    }
}