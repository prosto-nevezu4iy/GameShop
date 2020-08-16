using Core.Abstract.Repositories;
using Core.Domain;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Repositories
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(GameShopContext context)
            : base(context)
        {

        }

        public GameShopContext GameShopContext
        {
            get { return Context as GameShopContext; }
        }

        public void AddRange(IEnumerable<ProductImage> images)
        {
            GameShopContext.ProductImages.AddRange(images);
        }
    }
}
