using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class ProductsByCategoryViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Category Category { get; set; }
        public IDictionary<int, bool> AreProductsAtWishList { get; set; }
    }
}