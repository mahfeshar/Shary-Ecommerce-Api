
using Shary.Core.Entities;
using Shary.Core.Specifications;

namespace Shary.Core.Repositories.Contract;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
    Task<T?> GetWithSpecAsync(ISpecification<T> spec);
}
