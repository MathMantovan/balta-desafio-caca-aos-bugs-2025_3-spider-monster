using BugStore.Api.Requests.Customers;
using BugStore.Api.Services.Interface;
using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Handlers.Customers;

public class CreateCustomerHandler(ICustomerService _Service) : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
{
    public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _Service.CreateCustomerAsync
        (
            request.Name,
            request.Email,
            request.Phone,
            request.BirthDate,
            cancellationToken
        );

        return new CreateCustomerResponse(customer.Id, "Cliente criado com sucesso!");
    }
}
