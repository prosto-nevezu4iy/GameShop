using Core.Abstract.Repositories;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Core.Infrastructure.Repositories
{
    public class ParameterValueRepository : Repository<ParameterValue>, IParameterValueRepository
    {
        public ParameterValueRepository(GameShopContext context)
             : base(context)
        {

        }

        public GameShopContext GameShopContext
        {
            get { return Context as GameShopContext; }
        }

        public IEnumerable<ParameterValue> GetValuesWithSubGroup()
        {
            return GameShopContext.Values.Include(v => v.SubGroup);
        }

        public ParameterValue GetValueWithSubGroup(int id)
        {
            return GameShopContext.Values
                .Include(v => v.SubGroup)
                .FirstOrDefault(v => v.Id == id);
        }
    }
}
