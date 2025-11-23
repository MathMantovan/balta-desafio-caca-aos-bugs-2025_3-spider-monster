using MediatR;
using BugStore.Responses.Customers;

namespace BugStore.Requests.Customers;

public record GetCustomerByIdRequest(Guid id) : IRequest<GetCustomerByIdResponse>;