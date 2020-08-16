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
    public class ParameterSubGroupRepository : Repository<ParameterSubGroup>, IParameterSubGroupRepository
    {
        public ParameterSubGroupRepository(GameShopContext context)
             : base(context)
        {

        }

        public GameShopContext GameShopContext
        {
            get { return Context as GameShopContext; }
        }

        public IEnumerable<ParameterSubGroup> GetSubGroupsWithGroup()
        {
            return GameShopContext.SubGroups.Include(s => s.Group);
        }
    }
}
