using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class ParameterSubGroup
    {
        public ParameterSubGroup()
        {
            Values = new HashSet<ParameterValue>();
        }
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public ParameterGroup Group { get; set; }
        public ICollection<ParameterValue> Values { get; set; }
    }
}
