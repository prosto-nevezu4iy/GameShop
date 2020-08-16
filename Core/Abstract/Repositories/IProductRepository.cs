using Core.Domain;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> GetProductsByCategory(int categoryId, string name, int? priceFrom, int? PriceTo, int limit);
        IEnumerable<Product> GetProductsWithCategory();
        int PageSize { get; }
        Product Get(string productAlias);
        Product GetProductWithImages(int id);
        Product GetProductWithAllRelations(int id);
        IEnumerable<Product> Sort(IQueryable<Product> items, string field, string sort);
    }
}
