namespace BugStore.Responses.Products
{
    public record DeleteProductResponse(
        Guid Id,
        string Message
    );
}
