using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModels
{
    public class CatalogIndexViewModel
    {
        public List<ProductViewModel>? Products { get; set; }
        public List<SelectListItem>? Genres { get; set; }
        public int? GenresFilterApplied { get; set; }
        public PaginationInfoViewModel? PaginationInfo { get; set; }
    }
}
