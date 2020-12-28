using Core.Entities;
using Core.Intefaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    public class QueryProcessor<T> where T : Entity
    {
        public static IQueryable<T> ApplySpecification(
            IQueryable<T> query,
            ISpecification<T> specification)
        {
            if (specification.Predicate != null)
            {
                query = query.Where(specification.Predicate);
            }

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IsPagingEnabled)
            {
                query = query
                    .Skip(specification.Skip)
                    .Take(specification.Take);
            }

            query = specification.Includes.Aggregate(query, (current, expression) => 
                current.Include(expression));

            return query;
        }
    }
}
