using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class CartViewModel
    {
        public IEnumerable<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}