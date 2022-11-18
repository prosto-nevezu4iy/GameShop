using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        private readonly IBasketService _basketService;

        public HomeController(
            ICatalogViewModelService catalogViewModelService, 
            ICurrentUserService currentUserService, 
            IBasketService basketService)
        {
            _catalogViewModelService = catalogViewModelService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index(CatalogIndexViewModel catalogModel, int? pageId)
        {
            return View(await _catalogViewModelService.GetCatalogItems(pageId ?? 0, Constants.ITEMS_PER_PAGE, catalogModel.GenresFilterApplied));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
