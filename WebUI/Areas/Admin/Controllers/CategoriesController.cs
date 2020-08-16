using System.Web.Mvc;
using Core;
using Core.Domain;
using WebUI.Areas.Admin.Models;
using Core.Extensions;
using System.IO;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            var model = _unitOfWork.Categories.GetAllWithSubCategories();
            return View(model);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            var model = new CategoryAddViewModel()
            {
                Categories = new SelectList(_unitOfWork.Categories.GetAll(), "Id", "Name")
            };
            return View(model);
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    Name = model.Name,
                    Alias = model.Name.ToUrlSlug(),
                    ParentId = model.ParentId,
                };

                if (model.Image != null)
                {
                    category.ImageMimeType = model.Image.ContentType;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        model.Image.InputStream.Seek(0, SeekOrigin.Begin);
                        model.Image.InputStream.CopyTo(ms);
                        category.Image = ms.GetBuffer();
                    }
                }

                _unitOfWork.Categories.Add(category);

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            model.Categories = new SelectList(_unitOfWork.Categories.GetAll(), "Id", "Name");
            return View(model);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _unitOfWork.Categories.Get(id);
            var model = new CategoryEditViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Categories = new SelectList(_unitOfWork.Categories.GetAll(), "Id", "Name", category.ParentId),
                isImageUploaded = category.Image != null ? true : false
            };

            return View(model);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryEditViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var category = _unitOfWork.Categories.Get(id);

                category.Name = model.Name;
                category.Alias = model.Name.ToUrlSlug();
                category.ParentId = model.ParentId;

                if (model.Image != null)
                {
                    category.ImageMimeType = model.Image.ContentType;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        model.Image.InputStream.Seek(0, SeekOrigin.Begin);
                        model.Image.InputStream.CopyTo(ms);
                        category.Image = ms.GetBuffer();
                    }
                }

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            model.Categories = new SelectList(_unitOfWork.Categories.GetAll(), "Id", "Name", model.ParentId);
            return View(model);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var category = _unitOfWork.Categories.Get(id);

            _unitOfWork.Categories.Remove(category);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        // POST: Admin/Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteImage(int id)
        {
            var category = _unitOfWork.Categories.Get(id);

            _unitOfWork.Categories.RemoveImage(category);

            _unitOfWork.Complete();

            return RedirectToAction("Edit", new { id = id });
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
