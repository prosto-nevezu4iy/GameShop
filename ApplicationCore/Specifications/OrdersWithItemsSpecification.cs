using ApplicationCore.Entities.OrderAggregate;

namespace ApplicationCore.Specifications
{
    public class OrdersWithItemsSpecification : BaseSpecification<Order>
    {
        public OrdersWithItemsSpecification(Guid buyerId)
         : base(o => o.BuyerId == buyerId)
        {
            AddInclude(o => o.OrderItems);
        }
    }
}
