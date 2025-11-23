using MediatR;
using BugStore.Responses.Customers;

namespace BugStore.Requests.Customers;

public record DeleteCustomerRequest(Guid id) : IRequest<DeleteCustomerResponse>;