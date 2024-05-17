using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.ConeyorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class ConveyorMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult ConveyorMaster()
        {
            ConveyorModel vm = new ConveyorModel();
            return View(vm);
        }
        public ActionResult ConveyorGrid()
        {
            ConveyorModel vm = new ConveyorModel();
            ConveyorManager conveyorManager = new ConveyorManager();
            vm.ConveyorMasterList = conveyorManager.Get();

            return PartialView("Partial/ConveyorGrid", vm);
        }
        [HttpPost]
        public ActionResult ConveyorDetails(int ConveyorMasterId)
        {
            ConveyorManager conveyorManager = new ConveyorManager();
            ConveyorMaster conveyorMaster = new ConveyorMaster();
            ConveyorModel model = new ConveyorModel();
            string AutoCode;
           
            conveyorMaster = conveyorManager.GetConveyorMasterId(ConveyorMasterId);
            if (ConveyorMasterId == 0)
            {
                AutoCode = GetMasterID();
                AutoCode = "Con-" + AutoCode;
                model.ConveyorCode = AutoCode;
            }
            else
            {
                model.ConveyorCode = "Con-" + conveyorMaster.ConveyorCode;
            }
            if (conveyorMaster != null)
            {
                model.ConveyorMasterId = conveyorMaster.ConveyorMasterId;
                model.CapacityPerDay = conveyorMaster.CapacityPerDay;
                model.CapacityUnits = conveyorMaster.CapacityUnits;
                model.ConveyorName = conveyorMaster.ConveyorName;
                model.ConveyorType = conveyorMaster.ConveyorType;

                model.CreatedDate = conveyorMaster.CreatedDate;
                model.UpdatedDate = conveyorMaster.UpdatedDate;
                model.CreatedBy = conveyorMaster.CreatedBy;
                model.UpdatedBy = conveyorMaster.UpdatedBy;
            }
            return PartialView("Partial/ConveyorDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult ConveyorMaster(ConveyorModel model)
        {
            ConveyorMaster conveyorMasters = new ConveyorMaster();
            ConveyorMaster conveyorMaster = new ConveyorMaster();
            ConveyorManager conveyorManager = new ConveyorManager();
            conveyorMaster = conveyorManager.GetConveyorName(model.ConveyorName);
            model.ConveyorCode = model.ConveyorCode.Substring(4);
            if (conveyorMaster == null)
            {
                conveyorMasters.ConveyorMasterId = model.ConveyorMasterId;
                conveyorMasters.CapacityPerDay = model.CapacityPerDay;
                conveyorMasters.CapacityUnits = model.CapacityUnits;
                conveyorMasters.ConveyorType = model.ConveyorType;
                conveyorMasters.ConveyorCode = model.ConveyorCode;
                conveyorMasters.ConveyorName = model.ConveyorName;
                conveyorMasters.CreatedDate = DateTime.Now;
                conveyorManager.Post(conveyorMasters);
            }
            else if (conveyorMaster != null && conveyorMaster.ConveyorName == model.ConveyorName && model.ConveyorMasterId == 0)
            {
                conveyorMaster.ConveyorMasterId = 0;
                return Json(conveyorMasters, JsonRequestBehavior.AllowGet);
            }
            return Json(conveyorMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(ConveyorModel model)
        {
            ConveyorMaster conveyorMasters = new ConveyorMaster();
            if (ModelState.IsValid)
            {
                ConveyorMaster conveyorMaster = new ConveyorMaster();
                ConveyorManager conveyorManager = new ConveyorManager();
                conveyorMaster = conveyorManager.GetConveyorMasterId(model.ConveyorMasterId);
                model.ConveyorCode = model.ConveyorCode.Substring(4);
                if (conveyorMaster != null)
                {
                    conveyorMasters.ConveyorMasterId = model.ConveyorMasterId;
                    conveyorMasters.CapacityPerDay = model.CapacityPerDay;
                    conveyorMasters.CapacityUnits = model.CapacityUnits;
                    conveyorMasters.ConveyorType = model.ConveyorType;
                    conveyorMasters.ConveyorCode = model.ConveyorCode;
                    conveyorMasters.ConveyorName = model.ConveyorName;
                    conveyorMasters.CreatedDate = conveyorMaster.CreatedDate;
                    conveyorMasters.UpdatedDate = DateTime.Now;
                    conveyorMasters.CreatedBy = conveyorMaster.CreatedBy;
                    conveyorMasters.UpdatedBy = conveyorMaster.UpdatedBy;
                    conveyorManager.Put(conveyorMasters);
                }
                else
                {
                    return Json(conveyorMasters, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(conveyorMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int ConveyorMasterId)
        {
            ConveyorMaster conveyorMaster = new ConveyorMaster();
            string status = "";
            ConveyorManager conveyorManager = new ConveyorManager();
            conveyorMaster = conveyorManager.GetConveyorMasterId(ConveyorMasterId);
            if (conveyorMaster != null)
            {
                status = "Success";
                conveyorManager.Delete(conveyorMaster.ConveyorMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<ConveyorMaster> conveyorMasterlist = new List<ConveyorMaster>();
            ConveyorManager conveyorManager = new ConveyorManager();
            conveyorMasterlist = conveyorManager.Get();
            conveyorMasterlist = conveyorMasterlist.Where(x => x.ConveyorName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.ConveyorCode.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.ConveyorType.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            ConveyorModel model = new ConveyorModel();
            model.ConveyorMasterList = conveyorMasterlist;
            return PartialView("Partial/ConveyorGrid", model);
        }
        public string GetMasterID()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateConveyorID();
            ID++;
            return ID.ToString();
        }
        #endregion

    }
}
