using Core.Abstract.Repositories;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Infrastructure;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Web;

namespace Core.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(GameShopContext context)
            :base(context)
        {
        }

        public GameShopContext GameShopContext
        {
            get { return Context as GameShopContext; }
        }

        public int PageSize 
        { 
            get { return 10; }
        }

        public IQueryable<Product> GetProductsByCategory(int categoryId, string name, int? priceFrom, int? priceTo, int limit)
        {
            var result = GameShopContext.Products
                .Include(p => p.Images)
                .Include(p => p.WishLists)
                .Where(p => p.CategoryId == categoryId);

            if(!string.IsNullOrEmpty(name))
            {
                result = result.Where(p => p.Name.Contains(name));
            }

            // Fix with total price
            if(priceFrom.HasValue)
            {
                result = result.Where(p => p.Price >= priceFrom);
            }

            if(priceTo.HasValue)
            {
                result = result.Where(p => p.Price <= priceTo);
            }

            result = result.Take(limit);

            return result;
        }

        public Product Get(string productAlias)
        {
            return GameShopContext.Products
                .Include(p => p.Images)
                .FirstOrDefault(p => p.Alias == productAlias);
        }

        public IEnumerable<Product> GetProductsWithCategory()
        {
            return GameShopContext.Products
                .Include(p => p.Category);
        }

        public Product GetProductWithImages(int id)
        {
            return GameShopContext.Products
                .Include(p => p.Images)
                .FirstOrDefault(p => p.Id == id);
        }

        public Product GetProductWithAllRelations(int id)
        {
            return GameShopContext.Products
                .Include(p => p.Images)
                .Include(p => p.Values)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> Sort(IQueryable<Product> items, string field, string sort)
        {
            switch (field)
            {
                case "Name":
                    items = sort == "Asc" ? items.OrderBy(s => s.Name) : items.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    items = sort == "Asc" ? items.OrderBy(s => s.Price) : items.OrderByDescending(s => s.Price);
                    break;
                default:
                    items = items.OrderBy(s => s.Name);
                    break;
            }

            return items;
        }
    }
}
