using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : BaseSpecification<Product>
    {
        public CatalogFilterPaginatedSpecification(int skip, int take, int? genreId)
            : base(p => !genreId.HasValue || p.GenreId == genreId)
        {
            ApplyPaging(skip, take);
        }
    }
}
