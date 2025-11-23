using BugStore.Models;

namespace BugStore.Responses.Customers;

public record GetCustomerByIdResponse(Customer Customer, string Message);