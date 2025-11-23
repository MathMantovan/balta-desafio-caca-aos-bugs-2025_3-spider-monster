using BugStore.Api.Services.Interface;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Handlers.Customers;

public class GetCustomerByIdHandler(ICustomerService _CustomerService) : IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
{

    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
    {
        var customer = await _CustomerService.GetCustomerByIdAsync(request.id, cancellationToken);

        return new GetCustomerByIdResponse(customer, "Cliente encontrado!");
    }
}