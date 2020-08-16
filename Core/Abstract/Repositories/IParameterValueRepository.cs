using Core.Domain;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract.Repositories
{
    public interface IParameterValueRepository : IRepository<ParameterValue>
    {
        IEnumerable<ParameterValue> GetValuesWithSubGroup();
        ParameterValue GetValueWithSubGroup(int id);
    }
}
