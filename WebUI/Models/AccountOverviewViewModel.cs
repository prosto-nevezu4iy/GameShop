using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class AccountOverviewViewModel
    {
        public User User { get; set; }
        public int OrdersCount { get; set; }
        public int WishListCount { get; set; }
        public int CartCount { get; set; }

    }
}