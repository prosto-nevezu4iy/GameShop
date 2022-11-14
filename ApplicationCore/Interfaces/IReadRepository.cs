using System.Linq.Expressions;

namespace ApplicationCore.Interfaces
{
    public interface IReadRepository<T> where T : class, IAggregateRoot
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> ListAsync(CancellationToken cancellationToken = default);
        Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
        Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    }
}
