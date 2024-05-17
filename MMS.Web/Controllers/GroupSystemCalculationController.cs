using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.GroupSystemCalculationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class GroupSystemCalculationController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult GroupSystemCalculation()
        {
            GroupSystemCalculationModel vm = new GroupSystemCalculationModel();
            return View(vm);
        }
        public ActionResult GroupSystemCalculationGrid()
        {
            GroupSystemCalculationModel vm = new GroupSystemCalculationModel();
            GroupSystemCalculationManager groupSystemCalculationManager = new GroupSystemCalculationManager();
            vm.GroupSystemCalculationList = groupSystemCalculationManager.Get();

            return PartialView("Partial/GroupSystemCalculationGrid", vm);
        }
        [HttpPost]
        public ActionResult GroupSystemCalculationDetails(int GroupSystemCalculationId)
        {
            GroupSystemCalculationManager groupSystemCalculationManager = new GroupSystemCalculationManager();
            GroupSystemCalculation groupSystemCalculation = new GroupSystemCalculation();
            GroupSystemCalculationModel model = new GroupSystemCalculationModel();
            groupSystemCalculation = groupSystemCalculationManager.GetGroupSystemCalculationId(GroupSystemCalculationId);
            if (groupSystemCalculation != null)
            {
                model.GroupSystemCalculationId = groupSystemCalculation.GroupSystemCalculationId;
                model.ProductionPercent = groupSystemCalculation.ProductionPercent;           
                model.Amount = groupSystemCalculation.Amount;
                model.CreatedDate = groupSystemCalculation.CreatedDate;
                model.UpdatedDate = groupSystemCalculation.UpdatedDate;
                model.CreatedBy = groupSystemCalculation.CreatedBy;
                model.UpdatedBy = groupSystemCalculation.UpdatedBy;
            }
            return PartialView("Partial/GroupSystemCalculationDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult GroupSystemCalculation(GroupSystemCalculationModel model)
        {
            GroupSystemCalculation groupSystemCalculation = new GroupSystemCalculation();
            GroupSystemCalculationManager groupSystemCalculationManager = new GroupSystemCalculationManager();
            groupSystemCalculation.Amount = model.Amount;
            groupSystemCalculation.CreatedDate = DateTime.Now;
            groupSystemCalculation.UpdatedDate = DateTime.Now;
            groupSystemCalculation.ProductionPercent = model.ProductionPercent;
            groupSystemCalculation.CreatedBy = "";
            groupSystemCalculation.UpdatedBy = "";
            groupSystemCalculationManager.Post(groupSystemCalculation);
            return Json(groupSystemCalculation, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(GroupSystemCalculationModel model)
        {
            GroupSystemCalculation groupSystemCalculations = new GroupSystemCalculation();
            if (ModelState.IsValid)
            {
                GroupSystemCalculation groupSystemCalculation = new GroupSystemCalculation();
                GroupSystemCalculationManager groupSystemCalculationManager = new GroupSystemCalculationManager();
                groupSystemCalculation = groupSystemCalculationManager.GetGroupSystemCalculationId(model.GroupSystemCalculationId);
                if (groupSystemCalculation != null)
                {
                    groupSystemCalculations.GroupSystemCalculationId = model.GroupSystemCalculationId;
                    groupSystemCalculations.ProductionPercent = model.ProductionPercent;
                    groupSystemCalculations.Amount = model.Amount;
                    groupSystemCalculations.CreatedDate = groupSystemCalculation.CreatedDate;
                    groupSystemCalculations.UpdatedDate = DateTime.Now;
                    groupSystemCalculationManager.Put(groupSystemCalculations);
                }
                else
                {
                    return Json(groupSystemCalculations, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(groupSystemCalculations, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int GroupSystemCalculationId)
        {
            GroupSystemCalculation groupSystemCalculation = new GroupSystemCalculation();
            string status = "";
            GroupSystemCalculationManager groupSystemCalculationManager = new GroupSystemCalculationManager();
            groupSystemCalculation = groupSystemCalculationManager.GetGroupSystemCalculationId(GroupSystemCalculationId);
            if (groupSystemCalculation != null)
            {
                status = "Success";
                groupSystemCalculationManager.Delete(groupSystemCalculation.GroupSystemCalculationId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<GroupSystemCalculation> groupSystemCalculationlist = new List<GroupSystemCalculation>();
            GroupSystemCalculationManager groupSystemCalculationManager = new GroupSystemCalculationManager();
            groupSystemCalculationlist = groupSystemCalculationManager.Get();
            groupSystemCalculationlist = groupSystemCalculationlist.Where(s => s.ProductionPercent.ToLower().Trim().Contains(filter.ToLower().Trim()) || s.Amount.ToString().Trim().Contains(filter.ToLower().Trim())).ToList();
            GroupSystemCalculationModel model = new GroupSystemCalculationModel();
            model.GroupSystemCalculationList = groupSystemCalculationlist;
            return PartialView("Partial/GroupSystemCalculationGrid", model);
        }
        #endregion

    }
}
