using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;

namespace ApplicationCore.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public string PictureUri { get; private set; }

        public int GenreId { get; private set; }

        public Genre? Genre { get; private set; }

        public Product(int genreId, 
            string name,
            string description,
            decimal price,
            string pictureUri)
        {
            GenreId = genreId;
            Name = name;
            Description = description;
            Price = price;
            PictureUri = pictureUri;
        }

        public void UpdateDetails(CatalogItemDetails details)
        {
            Guard.Against.NullOrEmpty(details.Name, nameof(details.Name));
            Guard.Against.NullOrEmpty(details.Description, nameof(details.Description));
            Guard.Against.NegativeOrZero(details.Price, nameof(details.Price));

            Name = details.Name;
            Description = details.Description;
            Price = details.Price;
        }

        public void UpdateGenre(int genreId)
        {
            Guard.Against.Zero(genreId, nameof(genreId));
            GenreId = genreId;
        }

        public void UpdatePictureUri(string pictureName)
        {
            if (string.IsNullOrEmpty(pictureName))
            {
                PictureUri = string.Empty;
                return;
            }
            PictureUri = $"images\\products\\{pictureName}?{new DateTime().Ticks}";
        }
    }

    public record CatalogItemDetails(string Name, string Description, decimal Price);
}
