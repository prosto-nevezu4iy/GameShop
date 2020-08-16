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
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ParameterGroupsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ParameterGroupsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/ParameterGroups
        public ActionResult Index()
        {
            var model = _unitOfWork.ParameterGroups.GetAll();
            return View(model);
        }

        // GET: Admin/ParameterGroups/Create
        public ActionResult Create()
        {
            return View(new ParameterGroupViewModel());
        }

        // POST: Admin/ParameterGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParameterGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var group = new ParameterGroup()
                {
                    Name = model.Name
                };

                _unitOfWork.ParameterGroups.Add(group);

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/ParameterGroups/Edit/5
        public ActionResult Edit(int id)
        {
            var group = _unitOfWork.ParameterGroups.Get(id);

            var model = new ParameterGroupViewModel()
            {
                Name = group.Name
            };

            return View(model);
        }

        // POST: Admin/ParameterGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ParameterGroupViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var group = _unitOfWork.ParameterGroups.Get(id);

                group.Name = model.Name;

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Admin/ParameterGroups/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var group = _unitOfWork.ParameterGroups.Get(id);

            _unitOfWork.ParameterGroups.Remove(group);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}
