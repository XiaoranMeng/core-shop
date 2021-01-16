namespace Core.Entities.OrderAggregate
{
    public class OrderItem : Entity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
        {
            this.itemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered itemOrdered { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
