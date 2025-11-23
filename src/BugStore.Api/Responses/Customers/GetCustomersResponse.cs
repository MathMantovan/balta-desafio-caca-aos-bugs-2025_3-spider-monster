using BugStore.Models;

namespace BugStore.Responses.Customers;

public record GetCustomerResponse(List<Customer> Customer, string Message);