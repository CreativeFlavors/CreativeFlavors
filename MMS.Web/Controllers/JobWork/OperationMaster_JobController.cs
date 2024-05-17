using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.OperationMasterModel;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Common;


namespace MMS.Web.Controllers.JobWork
{

    [CustomFilter]
    public class OperationMaster_JobController : Controller
    {
        //
        // GET: /OperationMaster/
        #region Accounts View

        [HttpGet]
        public ActionResult OperationMaster()
        {
            OperationMasterModel vm = new OperationMasterModel();
            return View("~/Views/Jobwork/Job_Master/OperationMaster/OperationMaster.cshtml", vm);
        }
        public ActionResult OperationMasterGrid()
        {
            OperationMasterModel vm = new OperationMasterModel();
            OperationManager operationManager = new OperationManager();
            vm.OperationMasterList = operationManager.Get().Where(x=>x.ISJobWork== true).ToList();

            return PartialView("~/Views/Jobwork/Job_Master/OperationMaster/Partial/OperationMasterGrid.cshtml", vm);
    
        }
        [HttpPost]
        public ActionResult OperationMasterDetails(int OperationMasterId)
        {
            OperationManager operationManager = new OperationManager();
            OperationMaster operationMaster = new OperationMaster();
            OperationMasterModel model = new OperationMasterModel();
            operationMaster = operationManager.GetOperationMasterID(OperationMasterId);
            string autoId = GetAutoid();
            if (operationMaster != null)
            {
                model.OperationMasterId = operationMaster.OperationMasterId;
                model.OperationName = operationMaster.OperationName;
                model.OperationTypeCode = operationMaster.OperationTypeCode;
                model.CreatedBy = operationMaster.CreatedBy;
                model.UpdatedBy = operationMaster.UpdatedBy;
                model.CreatedDate = operationMaster.CreatedDate;
                model.UpdatedDate = operationMaster.UpdatedDate;
            }
            if (OperationMasterId == 0)
            {
                model.OperationTypeCode = "OPR" + autoId;
            }
            else
            {
                model.OperationTypeCode = operationMaster.OperationTypeCode;
            }
            return PartialView("~/Views/Jobwork/Job_Master/OperationMaster/Partial/OperationMasterDetails.cshtml", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult OperationMaster(OperationMasterModel model)
        {
            OperationMaster OperationMasters = new OperationMaster();
            OperationMaster operationMaster = new OperationMaster();
            if (ModelState.IsValid)
            {

                OperationManager operationManager = new OperationManager();
                operationMaster = operationManager.GetOperationName(model.OperationName);
                if (operationMaster == null)
                {
                    OperationMasters.OperationMasterId = model.OperationMasterId;
                    OperationMasters.OperationTypeCode = model.OperationTypeCode;
                    OperationMasters.OperationName = model.OperationName;
                    OperationMasters.CreatedDate = DateTime.Now;
                    OperationMasters.ISJobWork = true;
                    operationManager.Post(OperationMasters);
                }
                else if (operationMaster != null && operationMaster.OperationName == model.OperationName && model.OperationMasterId == 0)
                {
                    OperationMasters.OperationMasterId = 0;
                    return Json(OperationMasters, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(OperationMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(OperationMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(OperationMasterModel model)
        {
            OperationMaster OperationMasters = new OperationMaster();
            OperationMaster operationMaster = new OperationMaster();
            if (ModelState.IsValid)
            {
                OperationManager operationManager = new OperationManager();
                operationMaster = operationManager.GetOperationMasterID(model.OperationMasterId);
                if (operationMaster != null)
                {
                    OperationMasters.OperationMasterId = model.OperationMasterId;
                    OperationMasters.OperationTypeCode = model.OperationTypeCode;
                    OperationMasters.OperationName = model.OperationName;
                    operationManager.Put(OperationMasters);
                }
                else
                {
                    return Json(OperationMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(OperationMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int OperationMasterID)
        {
            OperationMaster operationMaster = new OperationMaster();
            string status = "";
            OperationManager operationManager = new OperationManager();
            operationMaster = operationManager.GetOperationMasterID(OperationMasterID);
            if (operationMaster != null)
            {
                status = "Success";
                operationManager.Delete(operationMaster.OperationMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public string GetAutoid()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoOperationMasterD();
            ID++;
            return ID.ToString();
        }
        #endregion


    }
}
