using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Categories
        public ActionResult List(string alias)
        {
            var subCategories = _unitOfWork.Categories.GetCategoryWithSubCategories(alias);

            return View(subCategories);
        }

        public FileContentResult GetImage(int id)
        {
            var category = _unitOfWork.Categories.Get(id);
            if (category != null)
            {
                return File(category.Image, category.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}