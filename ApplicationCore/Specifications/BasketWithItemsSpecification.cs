using ApplicationCore.Entities.BasketAggregate;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications
{
    public class BasketWithItemsSpecification : BaseSpecification<Basket>
    {
        public BasketWithItemsSpecification(int basketId)
        : base(b => b.Id == basketId)
        {
            AddInclude(b => b.Items);
        }
        public BasketWithItemsSpecification(Guid buyerId)
            : base(b => b.BuyerId == buyerId)
        {
            AddInclude(b => b.Items);
        }
    }
}
