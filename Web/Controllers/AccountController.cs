using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBasketService _basketService;

        public AccountController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public IActionResult Login() =>
            Challenge(new AuthenticationProperties
            {
                RedirectUri = Request.GetTypedHeaders().Referer.ToString()
            });

        public IActionResult UserInfo()
        {
            var identityUrl = "https://localhost:5002";

            var redirectUrl = $"{identityUrl}/Manage";

            return Redirect(redirectUrl);
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
