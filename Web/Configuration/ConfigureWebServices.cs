using ApplicationCore;
using Web.Interfaces;
using Web.Services;

namespace Web.Configuration
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CatalogSettings>(configuration);
            services.AddScoped<ICatalogViewModelService, CatalogViewModelService>();

            return services;
        }
    }
}
