using Core.Entities;
using Core.Intefaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AsyncRepository<T> : IAsyncRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _context;

        public AsyncRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetItemByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetItemsAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(ISpecification<T> specification)
        {
            return await GenerateQuery(specification).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetListAsync(ISpecification<T> specification)
        {
            return await GenerateQuery(specification).ToListAsync();
        }

        private IQueryable<T> GenerateQuery(ISpecification<T> specification)
        {
            return QueryProcessor<T>.ApplySpecification(_context.Set<T>().AsQueryable(), specification);
        }
    }
}
