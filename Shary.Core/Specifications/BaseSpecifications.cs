
using Shary.Core.Entities;
using System.Linq.Expressions;

namespace Shary.Core.Specifications;

public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; } = new();

    public BaseSpecifications()
    {
        
    }
    public BaseSpecifications(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
}
