using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.InspectionTypeMasterModel;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Common;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class InspectionTypeMasterController : Controller
    {


        #region Accounts View

        [HttpGet]
        public ActionResult InspectionTypeMaster()
        {
            InspectionTypeMasterModel vm = new InspectionTypeMasterModel();
            return View(vm);
        }
        public ActionResult InspectionTypeMasterGrid()
        {
            InspectionTypeMasterModel vm = new InspectionTypeMasterModel();
            InspectionTypeManager inspectionManager = new InspectionTypeManager();
            vm.InspectionTypeMasteList = inspectionManager.Get();

            return PartialView("Partial/InspectionTypeMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult InspectionTypeMasterDetails(int InspectionTypeMasterId)
        {

            InspectionTypeManager inspectionManager = new InspectionTypeManager();


            InspectionTypeMaster inspectionMaster = new InspectionTypeMaster();
            InspectionTypeMasterModel model = new InspectionTypeMasterModel();
            inspectionMaster = inspectionManager.GetInspectionTypeMasterId(InspectionTypeMasterId);
            int id_;
            string id = MMS.Web.ExtensionMethod.HtmlHelper.getAutoInspectionTypeMasterID();
            if (id == "")
            {
                id_ = 0;
            }
            else
            {
                id_ = Convert.ToInt32(id);
            }
            id_++;
            if (inspectionMaster != null)
            {
                model.InspectionTypeMasterId = inspectionMaster.InspectionTypeMasterId;
                model.Inspection = inspectionMaster.Inspection;
                model.OperationId = inspectionMaster.OperationId;
                model.InspectionReportName = inspectionMaster.InspectionReportName;
                model.Parameter = inspectionMaster.Parameter;
                model.InspectionFrequency = inspectionMaster.InspectionFrequency;
                model.Type = inspectionMaster.Type;
                model.CommonCause = inspectionMaster.CommonCause;
                model.CreatedBy = inspectionMaster.CreatedBy;
                model.UpdatedBy = inspectionMaster.UpdatedBy;
                model.CreatedDate = inspectionMaster.CreatedDate;
                model.UpdatedDate = inspectionMaster.UpdatedDate;
            }
            if (model.InspectionTypeMasterId == 0)
            {
                model.Code = "ITM" + id_;
            }
            else
            {
                model.Code = inspectionMaster.Code;
            }
            return PartialView("Partial/InspectionTypeMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult InspectionTypeMaster(InspectionTypeMasterModel model)
        {
            InspectionTypeMaster inspectionMasters = new InspectionTypeMaster();
            InspectionTypeMaster inspectionMaster = new InspectionTypeMaster();
            if (ModelState.IsValid)
            {

                InspectionTypeManager inspectionManager = new InspectionTypeManager();

                inspectionMaster = inspectionManager.GetParameter(model.InspectionReportName);
                if (inspectionMaster == null)
                {
                    inspectionMasters.InspectionTypeMasterId = model.InspectionTypeMasterId;
                    if (model.RadioButtonValue == "rawMaterial")
                    {
                        model.Inspection = 1;
                    }
                    else if (model.RadioButtonValue == "Production")
                    {
                        model.Inspection = 2;
                    }
                    inspectionMasters.Inspection = model.Inspection;
                    inspectionMasters.InspectionReportName = model.InspectionReportName;
                    inspectionMasters.OperationId = model.OperationId;
                    inspectionMasters.Code = model.Code;
                    inspectionMasters.Parameter = model.Parameter;
                    inspectionMasters.InspectionFrequency = model.InspectionFrequency;
                    inspectionMasters.Type = model.Type;
                    inspectionMasters.IsDeleted = false;
                    inspectionMasters.CommonCause = model.CommonCause;
                    inspectionMasters.CreatedDate = DateTime.Now;
                    inspectionManager.Post(inspectionMasters);
                }
                else if (inspectionMaster != null && inspectionMaster.InspectionReportName == model.InspectionReportName && model.InspectionTypeMasterId == 0)
                {
                    inspectionMaster.InspectionTypeMasterId = 0;
                    return Json(inspectionMaster, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(inspectionMaster, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(inspectionMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(InspectionTypeMasterModel model)
        {
            InspectionTypeMaster inspectionMasters = new InspectionTypeMaster();
            InspectionTypeMaster inspectionMaster = new InspectionTypeMaster();
            if (ModelState.IsValid)
            {
                InspectionTypeManager inspectionManager = new InspectionTypeManager();

                inspectionMaster = inspectionManager.GetInspectionTypeMasterId(model.InspectionTypeMasterId);

                if (inspectionMaster != null)
                {

                    inspectionMasters.InspectionTypeMasterId = model.InspectionTypeMasterId;
                    inspectionMasters.Inspection = model.Inspection;
                    inspectionMasters.InspectionReportName = model.InspectionReportName;
                    inspectionMasters.OperationId = model.OperationId;
                    inspectionMasters.Code = inspectionMaster.Code;
                    inspectionMasters.Parameter = model.Parameter;
                    inspectionMasters.InspectionFrequency = model.InspectionFrequency;
                    inspectionMasters.Type = model.Type;
                    inspectionMasters.CommonCause = model.CommonCause;
                    inspectionMasters.IsDeleted = inspectionMaster.IsDeleted;            
                    inspectionManager.Put(inspectionMasters);
                }
                else
                {
                    return Json(inspectionMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(inspectionMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int InspectionTypeMasterId)
        {
            InspectionTypeMaster inspectionMaster = new InspectionTypeMaster();
            string status = "";
            InspectionTypeManager inspectionManager = new InspectionTypeManager();
            inspectionMaster = inspectionManager.GetInspectionTypeMasterId(InspectionTypeMasterId);
            if (inspectionMaster != null)
            {
                status = "Success";
                inspectionManager.Delete(inspectionMaster.InspectionTypeMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<InspectionTypeMaster> InspectionTypeMasterList = new List<InspectionTypeMaster>();
            InspectionTypeManager inspectionManager = new InspectionTypeManager();
            InspectionTypeMasterList = inspectionManager.Get();
            InspectionTypeMasterList = InspectionTypeMasterList.Where(x => x.Code.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.Parameter.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            InspectionTypeMasterModel im = new InspectionTypeMasterModel();
            im.InspectionTypeMasteList = InspectionTypeMasterList;
            return PartialView("Partial/InspectionTypeMasterGrid", im);
        }
        #endregion

        public string InspectionTypeMasterID()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateID();
            ID++;
            return ID.ToString();
        }
    }
}
