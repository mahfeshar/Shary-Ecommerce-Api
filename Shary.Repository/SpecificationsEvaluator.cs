using Microsoft.EntityFrameworkCore;
using Shary.Core.Entities;
using Shary.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shary.Repository;

internal class SpecificationsEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery (IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
        {
            inputQuery = inputQuery.Where(spec.Criteria);
        }
        if (spec.OrderBy != null)
        {
            inputQuery = inputQuery.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDesc != null)
        {
            inputQuery = inputQuery.OrderByDescending(spec.OrderByDesc);
        }
        inputQuery = spec.Includes.Aggregate(inputQuery, (currentQuery, includeExpression) 
            => currentQuery.Include(includeExpression));
        return inputQuery;
    }
}
