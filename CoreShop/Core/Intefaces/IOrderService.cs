using Core.Entities.OrderAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Intefaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, 
            string cartId, Address shippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersAsync(string buyerEmail);

        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
