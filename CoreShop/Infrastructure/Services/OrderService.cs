using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Intefaces;
using Core.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _cartRepository;

        public OrderService(
            IUnitOfWork unitOfWork,
            ICartRepository cartRepository)
        {
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
        }
        
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, 
            string cartId, Address shippingAddress)
        {
            var cart = await _cartRepository.GetCartAsync(cartId);
            var orderItems = new List<OrderItem>();

            foreach (var cartItem in cart.CartItems)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetItemByIdAsync(cartItem.Id);
                var productItemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, productItem.Price, cartItem.Quantity); // Get the actual price from product table instead of cart item
                orderItems.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetItemByIdAsync(deliveryMethodId);
            var subtotal = orderItems.Sum(x => x.Price * x.Quantity);
            var order = new Order(orderItems, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            _unitOfWork.Repository<Order>().Add(order);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                return null;
            }

            await _cartRepository.DeleteCartAsync(cartId);
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var specification = new OrdersByDateWithItemsAndDeliverySpecification(id, buyerEmail);
            return await _unitOfWork.Repository<Order>().GetByIdAsync(specification);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync(string buyerEmail)
        {
            var specification = new OrdersByDateWithItemsAndDeliverySpecification(buyerEmail);
            return await _unitOfWork.Repository<Order>().GetListAsync(specification);
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetItemsAsync();
        }
    }
}
