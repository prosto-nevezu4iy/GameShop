using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Web;

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
