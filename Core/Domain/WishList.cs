using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class WishList
    {
        public int Id { get; set; }
        public string WishListId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
