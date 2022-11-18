using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog.Core;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class CatalogViewModelService : ICatalogViewModelService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IUriComposer _uriComposer;
        private readonly ILogger<CatalogViewModelService> _logger;

        public CatalogViewModelService(
            IRepository<Product> productRepository,
            IRepository<Genre> genreRepository,
            IUriComposer uriComposer,
            ILogger<CatalogViewModelService> logger)
        {
            _productRepository = productRepository;
            _genreRepository = genreRepository;
            _uriComposer = uriComposer;
            _logger = logger;
        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? genreId)
        {
            _logger.LogInformation("GetCatalogItems called.");

            var filterSpecification = new CatalogFilterSpecification(genreId);
            var filterPaginatedSpecification =
                new CatalogFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, genreId);

            // the implementation below using ForEach and Count. We need a List.
            var itemsOnPage = await _productRepository.ListAsync(filterPaginatedSpecification);
            var totalItems = await _productRepository.CountAsync(filterSpecification);

            var vm = new CatalogIndexViewModel()
            {
                Products = itemsOnPage.Select(i => new ProductViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    PictureUri = _uriComposer.ComposePicUri(i.PictureUri),
                    Price = i.Price
                }).ToList(),
                Genres = (await GetGenres()).ToList(),
                GenresFilterApplied = genreId ?? 0,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.Count,
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "disabled" : "";

            return vm;
        }

        public async Task<IEnumerable<SelectListItem>> GetGenres()
        {
            _logger.LogInformation("GetGenres called.");
            var genres = await _genreRepository.ListAsync();

            var items = genres
                .Select(type => new SelectListItem() { Value = type.Id.ToString(), Text = type.Name })
                .OrderBy(t => t.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }
    }
}
