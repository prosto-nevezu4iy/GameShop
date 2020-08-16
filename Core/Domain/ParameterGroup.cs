using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class ParameterGroup
    {
        public ParameterGroup()
        {
            SubGroups = new HashSet<ParameterSubGroup>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ParameterSubGroup> SubGroups { get; set; }
    }
}
