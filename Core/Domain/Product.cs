using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            Images = new HashSet<ProductImage>();
            Values = new HashSet<ParameterValue>();
            WishLists = new HashSet<WishList>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int? Discount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ParameterValue> Values { get; set; }
        public ICollection<WishList> WishLists { get; set; }

        public decimal TotalPrice => Discount.HasValue ? Price - (Price * Discount.Value / 100) : Price;
    }
}
