using System;
using System.Collections.Generic;

namespace Core.Entities.OrderAggregate
{
    public class Order : Entity
    {
        public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, 
            Address shoppingAddress, DeliveryMethod deliveryMethod, decimal subtotal)
        {
            OrderItems = orderItems;
            BuyerEmail = buyerEmail;
            ShoppingAddress = shoppingAddress;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
        }

        /// <summary>
        /// Used to retrive a list of orders for a user
        /// No connection to identity that is in a seperate context boundary
        /// </summary>
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public Address ShoppingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public decimal Subtotal { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string PaymentIntentId { get; set; }

        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}
