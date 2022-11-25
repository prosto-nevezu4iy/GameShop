using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Web.Extensions;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class CachedCatalogViewModelService : ICatalogViewModelService
    {
        private readonly IMemoryCache _cache;
        private readonly CatalogViewModelService _catalogViewModelService;

        public CachedCatalogViewModelService(IMemoryCache cache,
            CatalogViewModelService catalogViewModelService)
        {
            _cache = cache;
            _catalogViewModelService = catalogViewModelService;
        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? genreId)
        {
            var cacheKey = CacheHelpers.GenerateCatalogItemCacheKey(pageIndex, Constants.ITEMS_PER_PAGE, genreId);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
                return await _catalogViewModelService.GetCatalogItems(pageIndex, itemsPage, genreId);
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetGenres()
        {
            return await _cache.GetOrCreateAsync(CacheHelpers.GenerateGenresCacheKey(), async entry =>
            {
                entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
                return await _catalogViewModelService.GetGenres();
            });
        }
    }
}
