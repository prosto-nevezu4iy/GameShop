namespace ApplicationCore.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public ProductOrdered ProductOrdered { get; private set; }
        public decimal UnitPrice { get; private set; }
        public byte Units { get; private set; }

        // Required by Entity Framework
        private OrderItem() { }

        public OrderItem(ProductOrdered productOrdered, decimal unitPrice, byte units)
        {
            ProductOrdered = productOrdered;
            UnitPrice = unitPrice;
            Units = units;
        }
    }
}