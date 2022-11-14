using ApplicationCore.Entities;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications
{
    public class ProductsSpecification : BaseSpecification<Product>
    {
        public ProductsSpecification(params int[] ids) : base(p => ids.Contains(p.Id))
        {
        }
    }
}
