namespace BugStore.Responses.Customers;

public record DeleteCustomerResponse (Guid Id, string Name, string Message);