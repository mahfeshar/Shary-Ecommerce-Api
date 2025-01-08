
using Shary.Core;
using Shary.Core.Entities;
using Shary.Core.Entities.Order_Aggregate;
using Shary.Core.Repositories.Contract;
using Shary.Repository.Data;
using System.Collections;

namespace Shary.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreContext _dbContext;
    private Hashtable _repositories;

    public UnitOfWork(StoreContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new ();
    }
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var key = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(key))
        {
            var repository = new GenericRepository<TEntity>(_dbContext);
            _repositories.Add(key, repository);
        }
        return _repositories[key] as GenericRepository<TEntity>;
    }
    public async Task<int> CompleteAsync()
        => await _dbContext.SaveChangesAsync();

    public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();

}
