using Core;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using System.Data.Entity;
using Core.Abstract;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult List(string subAlias, ProductOperationViewModel vm)
        {
            var category = _unitOfWork.Categories.GetByAlias(subAlias);

            var products = _unitOfWork.Products.GetProductsByCategory(category.Id, vm.Name, vm.PriceFrom, vm.PriceTo, vm.Limit);

            var sortedProducts = _unitOfWork.Products.Sort(products, vm.Field, vm.Sort);

            var wishListId = _unitOfWork.WishLists.GetWishListId(HttpContext);

            var areProductsAtWishList = new Dictionary<int, bool>();

            foreach(var product in products)
            {
                var check = product.WishLists.Any(w => w.ProductId == product.Id && w.WishListId == wishListId);
                areProductsAtWishList.Add(product.Id, check);
            }

            var model = new ProductsByCategoryViewModel()
            {
                Products = sortedProducts,
                Category = category,
                AreProductsAtWishList = areProductsAtWishList
            };

            if(Request.IsAjaxRequest())
            {
                if(vm.Grid == "grid")
                {
                    return View("ProductsGrid", model);
                }

                return View("ProductsLarge", model);
            }

            return View(model);
        }

        public ActionResult Show(string productAlias)
        {
            var product = _unitOfWork.Products.Get(productAlias);

            var wishListId = _unitOfWork.WishLists.GetWishListId(HttpContext);
            var isProductAtWishList = _unitOfWork.WishLists.isProductAtWishList(product.Id, wishListId);

            var model = new ProductViewModel()
            {
                Product = product,
                IsProductAtWishList = isProductAtWishList
            };

            return View(model);
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