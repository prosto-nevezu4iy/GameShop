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
    public class ParameterValuesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ParameterValuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/ParameterValues
        public ActionResult Index()
        {
            var model = _unitOfWork.ParameterValues.GetValuesWithSubGroup();
            return View(model);
        }

        // GET: Admin/ParameterValues/Create
        public ActionResult Create()
        {
            var model = new ParameterValueViewModel()
            {
                ParameterSubGroups = new SelectList(_unitOfWork.ParameterSubGroups.GetSubGroupsWithGroup(), "Id", "Name", "Group.Name", null, null)
            };
            return View(model);
        }

        // POST: Admin/ParameterValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParameterValueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var value = new ParameterValue()
                {
                    Value = model.Value,
                    SubGroupId = model.SubGroupId,
                };

                _unitOfWork.ParameterValues.Add(value);

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            model.ParameterSubGroups = new SelectList(_unitOfWork.ParameterSubGroups.GetSubGroupsWithGroup(), "Id", "Name", "Group.Name", null, null);
            return View(model);
        }

        // GET: Admin/ParameterValues/Edit/5
        public ActionResult Edit(int id)
        {
            var value = _unitOfWork.ParameterValues.Get(id);

            var model = new ParameterValueViewModel()
            {
                Value = value.Value,
                ParameterSubGroups = new SelectList(_unitOfWork.ParameterSubGroups.GetSubGroupsWithGroup(), "Id", "Name", "Group.Name", value.SubGroupId, null),
                SubGroupId = value.SubGroupId
            };
            return View(model);
        }

        // POST: Admin/ParameterValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ParameterValueViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var value = _unitOfWork.ParameterValues.Get(id);

                value.Value = model.Value;
                value.SubGroupId = model.SubGroupId;

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            model.ParameterSubGroups = new SelectList(_unitOfWork.ParameterSubGroups.GetSubGroupsWithGroup(), "Id", "Name", "Group.Name", model.SubGroupId, null);
            return View(model);
        }

        // POST: Admin/ParameterValues/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var value = _unitOfWork.ParameterValues.Get(id);

            _unitOfWork.ParameterValues.Remove(value);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}
