using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core;
using Core.Domain;
using Core.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize(Roles = "User")]
    public class CabinetController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CabinetController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Cabinet
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var model = new AccountOverviewViewModel()
            {
                User = user,
                OrdersCount = _unitOfWork.Orders.getOrdersCount(user.Email),
                WishListCount = _unitOfWork.WishLists.getWishListCount(user.Email),
                CartCount = _unitOfWork.Carts.getCartCount(user.Email)
            };

            return View(model);
        }

        public ActionResult Account()
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var model = new AccountInformationViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Account(AccountInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.InternalGender = (int)model.Gender;
                user.PhoneNumber = model.PhoneNumber;

                UserManager.Update(user);
            }

            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View(new AccountChangePasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(AccountChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                var token = UserManager.GeneratePasswordResetToken(user.Id);

                var result = UserManager.ResetPassword(user.Id, token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                AddErrorsFromResult(result);
            }

            return View(model);
        }

        public ActionResult Orders()
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var orders = _unitOfWork.Orders.GetAllWithDetails(user.Email);

            return View(orders);
        }

        public FileContentResult GetImage(int id)
        {
            var productImage = _unitOfWork.ProductImages.Get(id);
            if (productImage != null)
            {
                return File(productImage.Image, productImage.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}
