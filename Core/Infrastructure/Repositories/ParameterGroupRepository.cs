using Core.Abstract.Repositories;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Repositories
{
    public class ParameterGroupRepository : Repository<ParameterGroup>, IParameterGroupRepository
    {
        public ParameterGroupRepository(GameShopContext context)
             : base(context)
        {

        }

        public GameShopContext GameShopContext
        {
            get { return Context as GameShopContext; }
        }
    }
}
