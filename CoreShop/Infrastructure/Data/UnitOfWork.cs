using Core.Entities;
using Core.Intefaces;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _registry;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAsyncRepository<T> Repository<T>() where T : Entity
        {
            if (_registry is null)
            {
                _registry = new Hashtable();
            }

            var key = typeof(T).Name;

            if (!_registry.ContainsKey(key))
            {
                var repositoryType = typeof(AsyncRepository<>);
                var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context); // Shared db context
                _registry.Add(key, repository);
            }

            return (IAsyncRepository<T>)_registry[key];
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
