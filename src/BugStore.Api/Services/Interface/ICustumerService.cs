using BugStore.Models;

namespace BugStore.Api.Services.Interface;

public interface ICustomerService
{
    Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken);
    Task<Customer> CreateCustomerAsync(
        string name, string email, string phone, DateTime birthDate, CancellationToken cancellationToken);
    Task<Customer> UpdateCustomerAsync(
        Guid id, string? name, string? email, string? phone, DateTime birthDate, CancellationToken cancellationToken);
    Task <Customer> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken);
}