using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public bool IsProductAtWishList { get; set; }
    }
}