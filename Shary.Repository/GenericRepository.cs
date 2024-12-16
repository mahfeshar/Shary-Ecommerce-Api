
using Microsoft.EntityFrameworkCore;
using Shary.Core.Entities;
using Shary.Core.Repositories.Contract;

namespace Shary.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext _dbContext;

    public GenericRepository(StoreContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }
}
