using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Requests.Customers;

public record UpdateCustomerRequest(string Name, string Email, string Phone, DateTime BirthDate) : IRequest<UpdateCustomerResponse>;