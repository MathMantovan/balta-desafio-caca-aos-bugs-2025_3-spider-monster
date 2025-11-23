using BugStore.Api.Repositories.Interface;
using BugStore.Models;

namespace BugStore.Api.Services
{
    public class VerificationCustomer
    {
        public async Task CustomerAlreadyExistOnRepository(Customer customer, IRepository<Customer> repository, CancellationToken cancellationToken)
        {
            var CustomerList = await repository.GetAllAsync(cancellationToken);
           
            if(CustomerList.Any
                (c =>
                c.Name == customer.Name 
                &&
                c.BirthDate == customer.BirthDate
                )
              )
                throw new InvalidOperationException("Os dados deste cliente ja estão cadastrados!");
        }
        public void CreateCustomerVerificationData(string? name, string? email, DateTime birthDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email é obrigatório.");

            if (birthDate >= DateTime.Today)
                throw new ArgumentException("Data de nascimento incorreta");
        }
    }
}
