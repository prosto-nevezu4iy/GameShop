using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.ViewComponents
{
    public class Basket : ViewComponent
    {
        private readonly IBasketViewModelService _basketService;
        private readonly ICurrentUserService _currentUserService;

        public Basket(IBasketViewModelService basketService, ICurrentUserService currentUserService)
        {
            _basketService = basketService;
            _currentUserService = currentUserService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new BasketComponentViewModel
            {
                ItemsCount = await CountTotalBasketItems()
            };
            return View(vm);
        }

        private async Task<int> CountTotalBasketItems()
        {
            if (User.Identity.IsAuthenticated)
            {
                Guard.Against.Null(_currentUserService.Id, nameof(_currentUserService.Id));
                return await _basketService.CountTotalBasketItems(Guid.Parse(_currentUserService.Id));
            }

            Guid anonymousId = GetAnnonymousIdFromCookie();
            if (anonymousId == Guid.Empty)
                return 0;

            return await _basketService.CountTotalBasketItems(anonymousId);
        }

        private Guid GetAnnonymousIdFromCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                var id = Request.Cookies[Constants.BASKET_COOKIENAME];

                if (Guid.TryParse(id, out var parsedId))
                {
                    return parsedId;
                }
            }
            return Guid.Empty;
        }
    }
}
