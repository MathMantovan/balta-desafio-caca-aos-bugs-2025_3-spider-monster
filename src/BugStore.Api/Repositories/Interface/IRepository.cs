namespace BugStore.Api.Repositories.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(T obj, CancellationToken cancellationToken);
        Task UpdateAsync(T obj, CancellationToken cancellationToken);
        Task RemoveAsync(T obj, CancellationToken cancellationToken);
    }
}
