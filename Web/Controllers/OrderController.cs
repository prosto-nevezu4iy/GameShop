using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderViewModelService _orderViewModelService;
        private readonly ICurrentUserService _currentUserService;

        public OrderController(IOrderViewModelService orderViewModelService, ICurrentUserService currentUserService)
        {
            _orderViewModelService = orderViewModelService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            _currentUserService.AssertNotNull(nameof(_currentUserService.Id));

            var viewModel = await _orderViewModelService.GetOrders(Guid.Parse(_currentUserService.Id));

            return View(viewModel);
        }

        [HttpGet("Order/Detail/{orderId}")]
        public async Task<IActionResult> Detail(int orderId)
        {
            _currentUserService.AssertNotNull(nameof(_currentUserService.Id));

            var viewModel = await _orderViewModelService.GetOrderDetails(orderId);

            if (viewModel == null)
            {
                return BadRequest("No such order found for this user.");
            }

            return View(viewModel);
        }
    }
}
