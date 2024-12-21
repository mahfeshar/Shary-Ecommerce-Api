
using Shary.Core.Entities;
using System.Linq.Expressions;

namespace Shary.Core.Specifications;

public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; } = new();
    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDesc { get; set; }

    public BaseSpecifications()
    {
        
    }
    public BaseSpecifications(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDesc = orderByDescExpression;
    }
}
