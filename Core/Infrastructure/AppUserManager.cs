using Core.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public class AppUserManager : UserManager<User>
    {
        public AppUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
                                                IOwinContext context)
        {
            GameShopContext db = context.Get<GameShopContext>();
            AppUserManager manager = new AppUserManager(new UserStore<User>(db));

            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            var provider = new DpapiDataProtectionProvider("SampleAppName");
            manager.UserTokenProvider = new DataProtectorTokenProvider<User>(provider.Create("SampleTokenName"));

            manager.EmailService = new EmailService();

            return manager;
        }
    }
}
