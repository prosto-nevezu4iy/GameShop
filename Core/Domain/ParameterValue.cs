using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class ParameterValue
    {
        public ParameterValue()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public int SubGroupId { get; set; }
        public string Value { get; set; }
        public ParameterSubGroup SubGroup { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
