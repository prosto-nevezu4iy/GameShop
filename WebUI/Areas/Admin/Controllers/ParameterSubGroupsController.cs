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
    public class ParameterSubGroupsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ParameterSubGroupsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/ParameterSubGroups
        public ActionResult Index()
        {
            var model = _unitOfWork.ParameterSubGroups.GetSubGroupsWithGroup();
            return View(model);
        }

        // GET: Admin/ParameterSubGroups/Create
        public ActionResult Create()
        {
            var model = new ParameterSubGroupViewModel()
            {
                ParameterGroups = new SelectList(_unitOfWork.ParameterGroups.GetAll(), "Id", "Name")
            };
            return View(model);
        }

        // POST: Admin/ParameterSubGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParameterSubGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var subGroup = new ParameterSubGroup()
                {
                    Name = model.Name,
                    GroupId = model.GroupId,
                };

                _unitOfWork.ParameterSubGroups.Add(subGroup);

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            model.ParameterGroups = new SelectList(_unitOfWork.ParameterGroups.GetAll(), "Id", "Name");
            return View(model);
        }

        // GET: Admin/ParameterSubGroups/Edit/5
        public ActionResult Edit(int id)
        {
            var subGroup = _unitOfWork.ParameterSubGroups.Get(id);

            var model = new ParameterSubGroupViewModel()
            {
                Name = subGroup.Name,
                ParameterGroups = new SelectList(_unitOfWork.ParameterGroups.GetAll(), "Id", "Name", subGroup.GroupId),
                GroupId = subGroup.GroupId
            };

            return View(model);
        }

        // POST: Admin/ParameterSubGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ParameterSubGroupViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var subGroup = _unitOfWork.ParameterSubGroups.Get(id);

                subGroup.Name = model.Name;
                subGroup.GroupId = model.GroupId;

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            model.ParameterGroups = new SelectList(_unitOfWork.ParameterGroups.GetAll(), "Id", "Name", model.GroupId);
            return View(model);
        }

        // POST: Admin/ParameterSubGroups/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var subGroup = _unitOfWork.ParameterSubGroups.Get(id);

            _unitOfWork.ParameterSubGroups.Remove(subGroup);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}
