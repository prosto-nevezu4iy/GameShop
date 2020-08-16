using Core.Abstract.Repositories;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        ICartRepository Carts { get; }
        IOrderRepository Orders { get; }
        IProductImageRepository ProductImages { get; }
        IParameterGroupRepository ParameterGroups { get; }
        IParameterSubGroupRepository ParameterSubGroups { get; }
        IParameterValueRepository ParameterValues { get; }
        IWishListRepository WishLists { get; }
        int Complete();
    }
}
