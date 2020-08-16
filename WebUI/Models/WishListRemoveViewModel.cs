using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class WishListRemoveViewModel
    {
        public string Message { get; set; }
        public string Class { get; set; }
        public string Color { get; set; }
        public int DeleteId { get; set; }
    }
}