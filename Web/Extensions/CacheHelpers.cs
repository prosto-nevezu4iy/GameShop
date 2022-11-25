namespace Web.Extensions
{
    public static class CacheHelpers
    {
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromSeconds(30);
        private static readonly string _itemsKeyTemplate = "items-{0}-{1}-{2}";

        public static string GenerateCatalogItemCacheKey(int pageIndex, int itemsPage, int? genreId)
        {
            return string.Format(_itemsKeyTemplate, pageIndex, itemsPage, genreId);
        }

        public static string GenerateGenresCacheKey()
        {
            return "genres";
        }
    }
}
