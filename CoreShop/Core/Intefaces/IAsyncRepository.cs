using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Intefaces
{
    public interface IAsyncRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(ISpecification<T> specification);

        Task<IReadOnlyList<T>> GetListAsync(ISpecification<T> specification);

        Task<T> GetItemByIdAsync(int id);

        Task<IReadOnlyList<T>> GetItemsAsync();
    }
}
