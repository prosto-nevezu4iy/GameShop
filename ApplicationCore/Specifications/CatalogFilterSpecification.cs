using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications
{
    public class CatalogFilterSpecification : BaseSpecification<Product>
    {
        public CatalogFilterSpecification(int? genreId)
            : base(p => !genreId.HasValue || p.GenreId == genreId)
        {
        }
    }
}
