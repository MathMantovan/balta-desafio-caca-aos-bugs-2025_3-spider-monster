using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Requests.Customers;
public record UpdateCustomerCommand(Guid Id, UpdateCustomerRequest DataRequest) : IRequest<UpdateCustomerResponse>;