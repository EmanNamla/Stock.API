using Microsoft.EntityFrameworkCore;
using Stock.Core.Entites;
using Stock.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public static class SpecificationEvalutor<T>  where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery, ISpecifications<T> spec)
        {
            var Query = InputQuery;
            if (spec.Criteria is not null)
            {
                Query = Query.Where(spec.Criteria);
            }
            if (spec.OrderByAsyning is not null)
            {
                Query = Query.OrderBy(spec.OrderByAsyning);
            }
            if (spec.OrderByDsyning is not null)
            {
                Query = Query.OrderByDescending(spec.OrderByDsyning);
            }
          

            Query = spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return Query;
        }

    }
}
