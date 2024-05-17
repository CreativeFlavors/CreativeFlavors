using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.ProjectionMasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MMS.Common;
using System.Globalization;

namespace MMS.Web
{
    [CustomFilter]
    public class ProjectionMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult ProjectionMaster()
        {
            ProjectionMasterModel vm = new ProjectionMasterModel();
            return View(vm);
        }
        public ActionResult ProjectionMasterGrid()
        {
            ProjectionMasterModel vm = new ProjectionMasterModel();
            ProjectionManager  projectionManager = new ProjectionManager();
            vm.ProjectionMasterList = projectionManager.Get();

            return PartialView("Partial/ProjectionMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult ProjectionMasterDetails(int ProjectionId)
        {
            ProjectionManager projectionManager = new ProjectionManager();
            ProjectionMaster projectionMaster = new ProjectionMaster();
            ProjectionMasterModel model = new ProjectionMasterModel();

            projectionMaster = projectionManager.GetProjectionMasterId(ProjectionId);
            if (projectionMaster != null)
            {
                model.ProjectionId = projectionMaster.ProjectionId;
                model.OrderIndicationNo = projectionMaster.OrderIndicationNo;
                model.BuyerMasterId = projectionMaster.BuyerMasterId;
                model.SeasonMasterId = projectionMaster.SeasonMasterId;
                model.Style = projectionMaster.Style;
                model.Date = Convert.ToDateTime(projectionMaster.Date).Date.ToString("dd/MM/yyyy");
                model.WeekNo = projectionMaster.WeekNo;
                model.CustomerPlan = projectionMaster.CustomerPlan;
                model.Quantity = projectionMaster.Quantity;
                model.IsSize = projectionMaster.IsSize;
                model.SizeRangeMasterId = projectionMaster.SizeRangeMasterId;                
                model.CreatedDate = projectionMaster.CreatedDate;
                model.UpdatedDate = projectionMaster.UpdatedDate;
                model.CreatedBy = projectionMaster.CreatedBy;
                model.UpdatedBy = projectionMaster.UpdatedBy;
            }
            return PartialView("Partial/ProjectionMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult ProjectionMaster(ProjectionMasterModel model)
        {
            ProjectionMaster projectionMasters = new ProjectionMaster();
            if (ModelState.IsValid)
            {
                ProjectionManager projectionManager = new ProjectionManager();
                ProjectionMaster projectionMaster = new ProjectionMaster();
                projectionMaster = projectionManager.GetOrderIndicationNo(model.OrderIndicationNo);
                if (projectionMaster == null)
                {
                    projectionMasters.ProjectionId = model.ProjectionId;
                    projectionMasters.OrderIndicationNo = model.OrderIndicationNo;
                    projectionMasters.BuyerMasterId = model.BuyerMasterId;
                    projectionMasters.SeasonMasterId = model.SeasonMasterId;
                    projectionMasters.Style = model.Style;
                    var format = "dd/MM/yyyy";
                    DateTime PMdate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                    projectionMasters.Date = PMdate.Date;

                    projectionMasters.WeekNo = model.WeekNo;
                    projectionMasters.CustomerPlan = model.CustomerPlan;
                    projectionMasters.Quantity = model.Quantity;
                    projectionMasters.IsSize = model.IsSize;
                    projectionMasters.SizeRangeMasterId = model.SizeRangeMasterId;
                    projectionMasters.CreatedDate = DateTime.Now;
                    projectionMasters.UpdatedDate = DateTime.Now;                   
                    projectionManager.Post(projectionMasters);
                }
                else
                {
                    return Json(projectionMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(projectionMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(ProjectionMasterModel model)
        {
            ProjectionMaster projectionMasters = new ProjectionMaster();
            if (ModelState.IsValid)
            {
                ProjectionManager projectionManager = new ProjectionManager();
                ProjectionMaster projectionMaster = new ProjectionMaster();

                projectionMaster = projectionManager.GetProjectionMasterId(model.ProjectionId);
                if (projectionMaster != null)
                {

                    projectionMasters.ProjectionId = model.ProjectionId;
                    projectionMasters.OrderIndicationNo = model.OrderIndicationNo;
                    projectionMasters.BuyerMasterId = model.BuyerMasterId;
                    projectionMasters.SeasonMasterId = model.SeasonMasterId;
                    projectionMasters.Style = model.Style;
                    var format = "dd/MM/yyyy";
                    DateTime PMdate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                    projectionMasters.Date = PMdate.Date;
                    projectionMasters.WeekNo = model.WeekNo;
                    projectionMasters.CustomerPlan = model.CustomerPlan;
                    projectionMasters.Quantity = model.Quantity;
                    projectionMasters.IsSize = model.IsSize;
                    projectionMasters.SizeRangeMasterId = model.SizeRangeMasterId;
                    projectionManager.Put(projectionMasters);
                }
                else
                {
                    return Json(projectionMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(projectionMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int ProjectionId)
        {
            ProjectionMaster projectionMaster = new ProjectionMaster();
            string status = "";
            ProjectionManager projectionManager = new ProjectionManager();
            projectionMaster = projectionManager.GetProjectionMasterId(ProjectionId);
            if (projectionMaster != null)
            {
                status = "Success";
                projectionManager.Delete(projectionMaster.ProjectionId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<ProjectionMaster> projectmasterList = new List<ProjectionMaster>();
            ProjectionManager projectionManager = new ProjectionManager();
            projectmasterList = projectionManager.Get();
            projectmasterList = projectmasterList.Where(x => x.OrderIndicationNo.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            ProjectionMasterModel vm = new ProjectionMasterModel();
            vm.ProjectionMasterList = projectmasterList;
            return PartialView("Partial/ProjectionMasterGrid", vm);
        }
        #endregion

    }
}
