using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstract.Repositories;
using Core.Infrastructure.Repositories;
using Core.Repositories;

namespace Core.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameShopContext _context;

        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public ICartRepository Carts { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IProductImageRepository ProductImages { get; private set; }
        public IParameterGroupRepository ParameterGroups { get; private set; }
        public IParameterSubGroupRepository ParameterSubGroups { get; private set; }
        public IParameterValueRepository ParameterValues { get; private set; }
        public IWishListRepository WishLists { get; private set; }

        public UnitOfWork(GameShopContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            Carts = new CartRepository(_context);
            Orders = new OrderRepository(_context);
            ProductImages = new ProductImageRepository(_context);
            ParameterGroups = new ParameterGroupRepository(_context);
            ParameterSubGroups = new ParameterSubGroupRepository(_context);
            ParameterValues = new ParameterValueRepository(_context);
            WishLists = new WishListRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
