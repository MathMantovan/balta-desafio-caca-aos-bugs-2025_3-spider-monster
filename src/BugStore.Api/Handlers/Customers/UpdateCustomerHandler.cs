using BugStore.Api.Services.Interface;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Handlers.Customers;

public class UpdateCustomerHandler(ICustomerService _CustomerService) : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
{

    public async Task<UpdateCustomerResponse?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var CustomerUpdated = await _CustomerService.UpdateCustomerAsync
        (
            request.Id, 
            request.DataRequest.Name, 
            request.DataRequest.Email, 
            request.DataRequest.Phone, 
            request.DataRequest.BirthDate, 
            cancellationToken
        );

        return new UpdateCustomerResponse(CustomerUpdated.Id, CustomerUpdated.Name, "Cliente atualizado com sucesso!");
    }
}