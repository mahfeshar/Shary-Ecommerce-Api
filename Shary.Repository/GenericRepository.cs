
using Microsoft.EntityFrameworkCore;
using Shary.Core.Entities;
using Shary.Core.Repositories.Contract;
using Shary.Core.Specifications;
using Shary.Repository.Data;

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
        // Problem : Every Entity We will add, we should add if for it
        if (typeof(T) == typeof(Product)) 
        {
            List<Product>? products =  await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync();
            return (IEnumerable<T>)products;
        }
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
    {
        return await ApplySpecifications(spec).ToListAsync();
    }

    public async Task<T?> GetAsync(int id)
    {
        if (typeof(T) == typeof(Product))
        {
            return await _dbContext.Set<Product>().Where(P => P.Id == id).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T;
        }
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
    {
        return await ApplySpecifications(spec).FirstOrDefaultAsync();
    }

    private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
    {
        return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
    }
}
