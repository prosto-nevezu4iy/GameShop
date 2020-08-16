using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NavController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PartialViewResult Menu()
        {
            var categories = _unitOfWork.Categories.GetAllWithSubCategories();

            return PartialView(categories);
        }
    }
}