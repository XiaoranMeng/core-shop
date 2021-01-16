using Core.Entities;
using System;
using System.Threading.Tasks;

namespace Core.Intefaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<T> Repository<T>() where T : Entity;

        Task<int> Complete();
    }
}
