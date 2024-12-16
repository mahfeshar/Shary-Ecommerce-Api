
using Shary.Core.Entities;

namespace Shary.Core.Repositories.Contract;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
}
