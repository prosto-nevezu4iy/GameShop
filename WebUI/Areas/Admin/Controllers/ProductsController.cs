
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core;
using Core.Domain;
using Core.Extensions;
using Core.Infrastructure;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Products
        public ActionResult Index()
        {
            var model = _unitOfWork.Products.GetProductsWithCategory();
            return View(model);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            var model = new ProductAddViewModel()
            {
                Categories = new SelectList(_unitOfWork.Categories.GetAll(), "Id", "Name"),
                Values = new MultiSelectList(_unitOfWork.ParameterValues.GetValuesWithSubGroup(), "Id", "Value", "SubGroup.Name", null, null)
            };
            return View(model);
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Alias = model.Name.ToUrlSlug(),
                    Description = model.Description,
                    Stock = model.Stock, 
                    Price = model.Price,
                    Discount = model.Discount,
                    CategoryId = model.CategoryId
                };

                _unitOfWork.Products.Add(product);

                foreach (HttpPostedFileBase image in model.Images)
                {
                    if (image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            image.InputStream.Seek(0, SeekOrigin.Begin);
                            image.InputStream.CopyTo(ms);

                            product.Images.Add(new ProductImage
                            {
                                Image = ms.GetBuffer(),
                                ImageMimeType = image.ContentType
                            });
                        }
                    }
                }

                if(model.ValueIds != null)
                {
                    foreach (var valueId in model.ValueIds)
                    {
                        var value = _unitOfWork.ParameterValues.Get(valueId);
                        product.Values.Add(value);
                    }
                }

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            model.Categories = new SelectList(_unitOfWork.Categories.GetAll(), "Id", "Name");
            model.Values = new MultiSelectList(_unitOfWork.ParameterValues.GetValuesWithSubGroup(), "Id", "Value", "SubGroup.Name", null, null);
            return View(model);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _unitOfWork.Products.GetProductWithAllRelations(id);
            var model = new ProductEditViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
                Discount = product.Discount,
                Categories = new SelectList(_unitOfWork.Categories.GetAll(), "Id", "Name", product.CategoryId),
                CategoryId = product.CategoryId,
                UploadedImages = product.Images,
                Values = new MultiSelectList(_unitOfWork.ParameterValues.GetValuesWithSubGroup(), "Id", "Value", "SubGroup.Name", product.Values.Select(v => v.Id), null)
            };

            return View(model);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEditViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var product = _unitOfWork.Products.GetProductWithAllRelations(id);
                product.Name = model.Name;
                product.Alias = model.Name.ToUrlSlug();
                product.Description = model.Description;
                product.Stock = model.Stock;
                product.Price = model.Price;
                product.Discount = model.Discount;
                product.CategoryId = model.CategoryId;

                foreach (HttpPostedFileBase image in model.Images)
                {
                    if (image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            image.InputStream.Seek(0, SeekOrigin.Begin);
                            image.InputStream.CopyTo(ms);

                            product.Images.Add(new ProductImage
                            {
                                Image = ms.GetBuffer(),
                                ImageMimeType = image.ContentType
                            });
                        }
                    }
                }

                if (model.ValueIds != null)
                {
                    product.Values.Clear();

                    foreach (var valueId in model.ValueIds)
                    {
                        var value = _unitOfWork.ParameterValues.Get(valueId);

                        product.Values.Add(value);
                    }
                }

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            model.Categories = new SelectList(_unitOfWork.Categories.GetAll(), "Id", "Name", model.CategoryId);
            model.Values = new MultiSelectList(_unitOfWork.ParameterValues.GetValuesWithSubGroup(), "Id", "Value", "SubGroup.Name", model.ValueIds, null);
            return View(model);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var product = _unitOfWork.Products.Get(id);

            _unitOfWork.Products.Remove(product);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteImage(int id, int productId)
        {
            var productImage = _unitOfWork.ProductImages.Get(id);

            _unitOfWork.ProductImages.Remove(productImage);

            _unitOfWork.Complete();

            return RedirectToAction("Edit", new { id = productId });
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
