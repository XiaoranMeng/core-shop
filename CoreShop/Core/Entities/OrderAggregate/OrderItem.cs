﻿namespace Core.Entities.OrderAggregate
{
    public class OrderItem : Entity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
        {
            this.ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered ItemOrdered { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
