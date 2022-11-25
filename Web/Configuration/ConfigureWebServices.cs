using ApplicationCore;
using ApplicationCore.Interfaces;
using Web.Interfaces;
using Web.Services;

namespace Web.Configuration
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CatalogSettings>(configuration);
            services.AddScoped<CatalogViewModelService>();
            services.AddScoped<ICatalogViewModelService, CachedCatalogViewModelService>();
            services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            services.AddScoped<IOrderViewModelService, OrderViewModelService>();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
