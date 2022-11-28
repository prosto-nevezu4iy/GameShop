using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Web.Config;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login() =>
            Challenge(new AuthenticationProperties
            {
                RedirectUri = Request.GetTypedHeaders().Referer.ToString()
            });

        public IActionResult UserInfo()
        {
            var identityConfig = _configuration.GetSection("Identity").Get<Identity>();

            return Redirect($"{identityConfig.Authority}/Manage");
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
