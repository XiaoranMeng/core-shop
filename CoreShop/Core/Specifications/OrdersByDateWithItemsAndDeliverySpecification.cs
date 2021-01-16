using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersByDateWithItemsAndDeliverySpecification : Specification<Order>
    {
        public OrdersByDateWithItemsAndDeliverySpecification(string email)
            : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrdersByDateWithItemsAndDeliverySpecification(int id, string email)
            : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
