using BugStore.Api.Services.Interface;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Handlers.Customers;

public class GetCustomersHandler(ICustomerService _CustomerService) : IRequestHandler<GetCustomerRequest, GetCustomerResponse>
{
    public async Task<GetCustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
    {
        var CustomerList = await _CustomerService.GetAllCustomersAsync(cancellationToken);

        return new GetCustomerResponse(CustomerList, "A Lista de clientes foi encontrada!");
    }
}