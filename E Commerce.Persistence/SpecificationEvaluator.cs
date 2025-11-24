using E_Commerce.Domain.Contracts.Specifications;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    internal static class SpecificationEvaluator
    {

        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> BaseQuery, ISpecifications<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>
        {
            var Query = BaseQuery;

            if(specifications.criteria is not null)
            {
                Query = Query.Where(specifications.criteria);
            }

            if (specifications.IncludeExp is not null && specifications.IncludeExp.Any())
            {
                Query = specifications.IncludeExp.Aggregate(Query, (currentQuery, specification) => currentQuery.Include(specification));
            }

            if (specifications.OrderBy is not null)
            {
                Query=Query.OrderBy(specifications.OrderBy);
            }

            if(specifications.OrderByDesc is not null)
            {
                Query=Query.OrderByDescending(specifications.OrderByDesc);
            }
            if(specifications.IsPaginated)
            {
                Query=Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return Query;
        }
    }
}
