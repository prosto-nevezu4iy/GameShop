using ApplicationCore.Extensions;

namespace ApplicationCore.Entities.OrderAggregate
{
    public class ProductOrdered // Value Object
    {
        // Required by Entity Framework
        private ProductOrdered() { }

        public ProductOrdered(int productId, string productName, string pictureUri)
        {
            productId.AssertOutOfRange(nameof(productId), 1, int.MaxValue);
            productName.AssertNotEmpty(nameof(productName));
            pictureUri.AssertNotEmpty(nameof(pictureUri));

            ProductId = productId;
            ProductName = productName;
            PictureUri = pictureUri;
        }

        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUri { get; private set; }
    }
}