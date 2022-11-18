using ApplicationCore.Extensions;

namespace ApplicationCore.Entities.BasketAggregate
{
    public class BasketItem : BaseEntity
    {
        public decimal UnitPrice { get; private set; }
        public byte Quantity { get; private set; }
        public int ProductId { get; private set; }

        public BasketItem(int productId, byte quantity, decimal unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            SetQuantity(quantity);
        }

        public void AddQuantity(byte quantity)
        {
            quantity.AssertOutOfRange(nameof(quantity), byte.MinValue, byte.MaxValue);

            Quantity += quantity;
        }

        public void SetQuantity(byte quantity)
        {
            quantity.AssertOutOfRange(nameof(quantity), byte.MinValue, byte.MaxValue);

            Quantity = quantity;
        }
    }
}