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
using WebUI.Models;

namespace WebUI.Controllers
{
    public class WishListController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WishListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: WishLists
        public ActionResult Index()
        {
            var wishListId = _unitOfWork.WishLists.GetWishListId(HttpContext);

            var wishListItems = _unitOfWork.WishLists.GetWishListItems(wishListId);

            return View(wishListItems);
        }

        [HttpPost]
        public ActionResult AddToWishList(int id)
        {
            // Retrieve the product from the database
            var addedProduct = _unitOfWork.Products.Get(id);

            // Add it to the shopping cart
            var wishListId = _unitOfWork.WishLists.GetWishListId(HttpContext);

            _unitOfWork.WishLists.AddToWishList(addedProduct, wishListId);

            _unitOfWork.Complete();

            var results = new WishListAddViewModel()
            {
                Message = Server.HtmlEncode(addedProduct.Name) +
                    " has been added to your wish list.",
                Class = "removeFromWishList",
                Color = "text-danger"
            };

            return Json(results);
        }

        [HttpPost]
        public ActionResult RemoveFromWishList(int id)
        {
            // Remove the item from the cart
            var wishListId = _unitOfWork.WishLists.GetWishListId(HttpContext);

            // Get the name of the product to display confirmation
            var productName = _unitOfWork.Products.Get(id).Name;

            // Remove from cart
            _unitOfWork.WishLists.RemoveFromWishList(id, wishListId);

            _unitOfWork.Complete();

            // Display the confirmation message
            var results = new WishListRemoveViewModel()
            {
                Message = Server.HtmlEncode(productName) +
                    " has been removed from your wish list.",
                Class = "addToWishList",
                Color = "text-danger",
                DeleteId = id
            };

            return Json(results);
        }

        [HttpPost]
        public ActionResult CreateCart(int id)
        {
            // Remove the item from the cart
            var wishListId = _unitOfWork.WishLists.GetWishListId(HttpContext);

            // Get the name of the product to display confirmation
            var productName = _unitOfWork.Products.Get(id).Name;

            // Remove from cart
            _unitOfWork.WishLists.CreateCart(id, wishListId);

            _unitOfWork.Complete();

            // Display the confirmation message
            var results = new WishListCreateCartViewModel()
            {
                Message = Server.HtmlEncode(productName) +
                    " has been moved from your wish list to cart.",
                DeleteId = id,
                CartCount = _unitOfWork.Carts.GetCount(wishListId)
            };

            return Json(results);
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
