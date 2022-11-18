using Ardalis.GuardClauses;

namespace ApplicationCore.Entities.OrderAggregate
{
    public class ProductOrdered // Value Object
    {
        #pragma warning disable CS8618 // Required by Entity Framework
        private ProductOrdered() { }

        public ProductOrdered(int productId, string productName, string pictureUri)
        {
            Guard.Against.OutOfRange(productId, nameof(productId), 1, int.MaxValue);
            Guard.Against.NullOrEmpty(productName, nameof(productName));
            Guard.Against.NullOrEmpty(pictureUri, nameof(pictureUri));

            ProductId = productId;
            ProductName = productName;
            PictureUri = pictureUri;
        }

        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUri { get; private set; }
    }
}