
using Shary.Core.Entities;
using Shary.Core.Entities.Order_Aggregate;
using Shary.Core.Repositories.Contract;

namespace Shary.Core;

public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> CompleteAsync();
}
