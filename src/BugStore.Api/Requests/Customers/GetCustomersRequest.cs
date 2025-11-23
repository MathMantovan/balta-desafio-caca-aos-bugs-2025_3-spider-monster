using MediatR;
using BugStore.Responses.Customers;

namespace BugStore.Requests.Customers;

public record GetCustomerRequest() : IRequest<GetCustomerResponse>;

