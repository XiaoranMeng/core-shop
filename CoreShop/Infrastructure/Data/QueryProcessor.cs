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

            query = specification.Includes.Aggregate(query, (current, expression) => 
                current.Include(expression));

            return query;
        }
    }
}
