using Core.Domain;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Core.Infrastructure;
using Core;
using System.Globalization;

namespace WebUI.Controllers
{
    [Authorize(Roles = "User")]
    public class CheckoutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckoutController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>().FindById(User.Identity.GetUserId());

            var countries = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(c => new RegionInfo(c.LCID)).Distinct().OrderBy(c => c.EnglishName);

            var model = new OrderViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Countries = new SelectList(countries, "EnglishName", "EnglishName")
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddressAndPayment(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Country = model.Country,
                    City = model.City,
                    Address = model.Address,
                    PostalCode = model.PostalCode,
                    OrderDate = DateTime.Now
                };

                _unitOfWork.Orders.Add(order);

                var cartId = _unitOfWork.Carts.GetCartId(HttpContext);
                _unitOfWork.Carts.CreateOrder(order, cartId);

                _unitOfWork.Complete();

                return RedirectToAction("Complete", new { id = order.Id });
            }
            return View(model);
        }

        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = _unitOfWork.Orders.isValid(id, User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}