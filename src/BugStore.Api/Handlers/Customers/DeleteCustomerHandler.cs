using BugStore.Api.Services.Interface;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Handlers.Customers;

public class DeleteCustomerHandler(ICustomerService _service) : IRequestHandler<DeleteCustomerRequest, DeleteCustomerResponse>
{
    public async Task<DeleteCustomerResponse> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {       
        var customer = await _service.DeleteCustomerAsync(request.id, cancellationToken);

        return new DeleteCustomerResponse(request.id, customer.Name, $"Cliente {customer.Name} foi deletado com sucesso!");
    }
}