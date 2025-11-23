namespace BugStore.Responses.Products
{
    public record CreateProductResponse(
        Guid Id,
        string Title,
        string Message
    );
}
