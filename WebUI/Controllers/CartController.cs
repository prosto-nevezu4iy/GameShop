using Core;
using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cartId = _unitOfWork.Carts.GetCartId(HttpContext);

            // Set up our ViewModel
            var model = new CartViewModel
            {
                CartItems = _unitOfWork.Carts.GetCartItems(cartId),
                CartTotal = _unitOfWork.Carts.GetTotal(cartId)
            };
            // Return the view
            return View(model);
        }

        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            // Retrieve the product from the database
            var addedProduct = _unitOfWork.Products.Get(id);

            // Add it to the shopping cart
            var cartId = _unitOfWork.Carts.GetCartId(HttpContext);

            _unitOfWork.Carts.AddToCart(addedProduct, cartId);

            _unitOfWork.Complete();

            var results = new CartAddViewModel()
            {
                Message = Server.HtmlEncode(addedProduct.Name) +
                    " has been added to your shopping cart.",
                CartCount = _unitOfWork.Carts.GetCount(cartId)
            };

            return Json(results);
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cartId = _unitOfWork.Carts.GetCartId(HttpContext);

            // Get the name of the product to display confirmation
            var productName = _unitOfWork.Carts.GetCart(id).Product.Name;

            // Remove from cart
            _unitOfWork.Carts.RemoveFromCart(id, cartId);

            _unitOfWork.Complete();

            // Display the confirmation message
            var results = new CartRemoveViewModel()
            {
                Message = Server.HtmlEncode(productName) +
                    " has been removed from your shopping cart.",
                CartTotal = _unitOfWork.Carts.GetTotal(cartId),
                CartCount = _unitOfWork.Carts.GetCount(cartId),
                DeleteId = id
            };

            return Json(results);
        }

        [HttpPost]
        public ActionResult IncreaseItem(int id)
        {
            // Remove the item from the cart
            var cartId = _unitOfWork.Carts.GetCartId(HttpContext);

            // Get the name of the product to display confirmation
            var productName = _unitOfWork.Carts.GetCart(id).Product.Name;

            // Remove from cart
            int itemCount = _unitOfWork.Carts.IncreaseItem(id, cartId);

            _unitOfWork.Complete();

            // Display the confirmation message
            var results = new CartModifyViewModel()
            {
                Message = Server.HtmlEncode(productName) +
                    " has been increased in your shopping cart.",
                CartTotal = _unitOfWork.Carts.GetTotal(cartId),
                CartCount = _unitOfWork.Carts.GetCount(cartId),
                ItemCount = itemCount,
                ItemId = id
            };

            return Json(results);
        }

        [HttpPost]
        public ActionResult DecreaseItem(int id)
        {
            // Remove the item from the cart
            var cartId = _unitOfWork.Carts.GetCartId(HttpContext);

            // Get the name of the product to display confirmation
            var productName = _unitOfWork.Carts.GetCart(id).Product.Name;

            // Remove from cart
            int itemCount = _unitOfWork.Carts.DecreaseItem(id, cartId);

            _unitOfWork.Complete();

            // Display the confirmation message
            var results = new CartModifyViewModel()
            {
                Message = Server.HtmlEncode(productName) +
                    " has been decreased in your shopping cart.",
                CartTotal = _unitOfWork.Carts.GetTotal(cartId),
                CartCount = _unitOfWork.Carts.GetCount(cartId),
                ItemCount = itemCount,
                ItemId = id
            };

            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cartId = _unitOfWork.Carts.GetCartId(HttpContext);

            var model = _unitOfWork.Carts.GetCount(cartId);
            return PartialView(model);
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
    }
}