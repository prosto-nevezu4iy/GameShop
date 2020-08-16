using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class ProductOperationViewModel
    {
        public string Name { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public string Field { get; set; }
        public string Sort { get; set; }

        private int _limit = 10;

        public int Limit
        {
            get => _limit;
            set
            {
                _limit = value;
            }
        }

        private string _grid = "grid";

        public string Grid
        {
            get => _grid;
            set
            {
                _grid = value;
            }
        }
    }
}