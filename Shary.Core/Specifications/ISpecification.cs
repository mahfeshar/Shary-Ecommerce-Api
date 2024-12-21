
using Shary.Core.Entities;
using System.Linq.Expressions;

namespace Shary.Core.Specifications;

public interface ISpecification<T> where T : BaseEntity
{
    Expression<Func<T, bool>> Criteria{ get; set; }
    List<Expression<Func<T, object>>> Includes { get; set; }
    Expression<Func<T, object>> OrderBy { get; set; }
    Expression<Func<T, object>> OrderByDesc { get; set; }
}
