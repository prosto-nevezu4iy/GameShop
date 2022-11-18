using ApplicationCore.Entities;
using ApplicationCore.Entities.OrderAggregate;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IBasketViewModelService _basketViewModelService;
        private readonly IBasketService _basketService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrderService _orderService;
        private readonly ILogger<BasketController> _logger;

        public BasketController(
            IRepository<Product> productRepository,
            IBasketViewModelService basketViewModelService,
            IBasketService basketService,
            ICurrentUserService currentUserService,
            IOrderService orderService,
            ILogger<BasketController> logger)
        {
            _productRepository = productRepository;
            _basketViewModelService = basketViewModelService;
            _basketService = basketService;
            _currentUserService = currentUserService;
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketViewModelService.GetOrCreateBasketForUser(GetOrSetBasketCookieAndUserId()));
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProductViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToPage("/Index");
            }

            var product = await _productRepository.GetByIdAsync(productDetails.Id);
            if (product == null)
            {
                return RedirectToPage("/Index");
            }

            var userId = GetOrSetBasketCookieAndUserId();
            var basket = await _basketService.AddItemToBasket(userId,
                productDetails.Id, product.Price);

            var basketModel = await _basketViewModelService.Map(basket);

            return View(basketModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(IEnumerable<BasketItemViewModel> items)
        {
            var basketView = await _basketViewModelService.GetOrCreateBasketForUser(GetOrSetBasketCookieAndUserId());

            if (!ModelState.IsValid)
            {
                return View(nameof(Index), basketView);
            }

            var updateModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);
            await _basketService.SetQuantities(basketView.Id, updateModel);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View(await _basketViewModelService.GetOrCreateBasketForUser(Guid.Parse(_currentUserService.Id)));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(IEnumerable<BasketItemViewModel> items)
        {
            try
            {
                var basketViewModel = await _basketViewModelService.GetOrCreateBasketForUser(Guid.Parse(_currentUserService.Id));

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var updateModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);
                await _basketService.SetQuantities(basketViewModel.Id, updateModel);
                await _orderService.CreateOrderAsync(basketViewModel.Id, new Address("123 Main St.", "Kent", "OH", "United States", "44240"));
                await _basketService.DeleteBasketAsync(basketViewModel.Id);
            }
            catch (EmptyBasketOnCheckoutException emptyBasketOnCheckoutException)
            {
                 _logger.LogWarning(emptyBasketOnCheckoutException.Message);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Success));
        }

        [Authorize]
        public async Task<IActionResult> Success()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TransferAnonymousBasket([FromBody] string userId)
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                var anonymousId = Request.Cookies[Constants.BASKET_COOKIENAME];
                if (Guid.TryParse(anonymousId, out var _))
                {
                    Guard.Against.NullOrEmpty(userId, nameof(userId));
                    await _basketService.TransferBasketAsync(anonymousId, userId);
                }
                Response.Cookies.Delete(Constants.BASKET_COOKIENAME);
            }

            return Ok();
        }

        private Guid GetOrSetBasketCookieAndUserId()
        {
            Guard.Against.Null(Request.HttpContext.User.Identity, nameof(Request.HttpContext.User.Identity));
            Guid userId = Guid.Empty;

            if (Request.HttpContext.User.Identity.IsAuthenticated)
            {
               Guard.Against.Null(_currentUserService.Id, nameof(_currentUserService.Id));
               return Guid.Parse(_currentUserService.Id);
            }

            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                if (!Request.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (!Guid.TryParse(Request.Cookies[Constants.BASKET_COOKIENAME].ToString(), out var parsedId))
                    {
                        userId = Guid.Empty;
                    } else
                    {
                        userId = parsedId;
                    }
                }
            }

            if (userId != Guid.Empty) return userId;

            userId = Guid.NewGuid();

            var cookieOptions = new CookieOptions { IsEssential = true };
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, userId.ToString(), cookieOptions);

            return userId;
        }
    }
}
