using Core.Entities;
using System.Threading.Tasks;

namespace Core.Intefaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string cartId);

        Task<Cart> CreateOrUpdateCartAsync(Cart cart);

        Task<bool> DeleteCartAsync(string cartId);
    }
}
