using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class WishListCreateCartViewModel
    {
        public string Message { get; set; }
        public int DeleteId { get; set; }
        public int CartCount { get; set; }
    }
}