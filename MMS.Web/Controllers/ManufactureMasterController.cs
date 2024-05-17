using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.Manufacturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class ManufactureMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult ManufactureMaster()
        {
            ManufacturerModel vm = new ManufacturerModel();
            return View(vm);
        }
        public ActionResult ManufactureGrid()
        {
            ManufacturerModel vm = new ManufacturerModel();
            ManufactureManager manufactureManager = new ManufactureManager();
            vm.ManufacturerMasterList = manufactureManager.Get();

            return PartialView("Partial/ManufactureGrid", vm);
        }
        [HttpPost]
        public ActionResult ManufactureDetails(int ManufacturerMasterId)
        {
            ManufactureManager manufactureManager = new ManufactureManager();
            ManufacturerMaster manufacturerMaster = new ManufacturerMaster();
            ManufacturerModel model = new ManufacturerModel();
            manufacturerMaster = manufactureManager.GetManufacturerMasterId(ManufacturerMasterId);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoManufactureMasterD();
            ID++;
            if (manufacturerMaster != null)
            {
                model.ManufacturerMasterId = manufacturerMaster.ManufacturerMasterId;
                model.ManufacturerCode = manufacturerMaster.ManufacturerCode;
                model.ManufacturerName = manufacturerMaster.ManufacturerName;
                model.CreatedDate = manufacturerMaster.CreatedDate;
                model.UpdatedDate = manufacturerMaster.UpdatedDate;
                model.CreatedBy = manufacturerMaster.CreatedBy;
                model.UpdatedBy = manufacturerMaster.UpdatedBy;
            }
            if (ManufacturerMasterId != 0)
            {
                model.ManufacturerCode = model.ManufacturerCode;
            }
            else
            {
                model.ManufacturerCode = "MAF" + ID;
            }
            return PartialView("Partial/ManufactureDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult ManufactureMaster(ManufacturerModel model)
        {
            ManufacturerMaster manufacturerMasters = new ManufacturerMaster();
            ManufacturerMaster manufacturerMaster = new ManufacturerMaster();
            ManufactureManager manufactureManager = new ManufactureManager();
            manufacturerMaster = manufactureManager.GetManufacturerName(model.ManufacturerName);
            if (manufacturerMaster == null)
            {
                manufacturerMasters.ManufacturerMasterId = model.ManufacturerMasterId;
                manufacturerMasters.ManufacturerCode = model.ManufacturerCode;
                manufacturerMasters.ManufacturerName = model.ManufacturerName;
                manufacturerMasters.CreatedDate = DateTime.Now;
                manufactureManager.Post(manufacturerMasters);
            }
            else if (manufacturerMaster != null && manufacturerMaster.ManufacturerName == model.ManufacturerName && model.ManufacturerMasterId == 0)
            {
                manufacturerMasters.ManufacturerMasterId = 0;
                return Json(manufacturerMasters, JsonRequestBehavior.AllowGet);
            }
            return Json(manufacturerMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(ManufacturerModel model)
        {
            ManufacturerMaster manufacturerMasters = new ManufacturerMaster();
            if (ModelState.IsValid)
            {
                ManufacturerMaster manufacturerMaster = new ManufacturerMaster();
                ManufactureManager manufactureManager = new ManufactureManager();
                manufacturerMaster = manufactureManager.GetManufacturerMasterId(model.ManufacturerMasterId);
                if (manufacturerMaster != null)
                {
                    manufacturerMasters.ManufacturerMasterId = model.ManufacturerMasterId;
                    manufacturerMasters.ManufacturerCode = model.ManufacturerCode;
                    manufacturerMasters.ManufacturerName = model.ManufacturerName;
                    manufacturerMasters.CreatedDate = manufacturerMaster.CreatedDate;
                    manufacturerMasters.UpdatedDate = DateTime.Now;
                    manufacturerMasters.CreatedBy = manufacturerMaster.CreatedBy;
                    manufacturerMasters.UpdatedBy = manufacturerMaster.UpdatedBy;
                    manufactureManager.Put(manufacturerMasters);
                }
                else
                {
                    return Json(manufacturerMasters, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(manufacturerMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int ManufacturerMasterId)
        {
            ManufacturerMaster manufacturerMaster = new ManufacturerMaster();
            string status = "";
            ManufactureManager manufactureManager = new ManufactureManager();
            manufacturerMaster = manufactureManager.GetManufacturerMasterId(ManufacturerMasterId);
            if (manufacturerMaster != null)
            {
                status = "Success";
                manufactureManager.Delete(manufacturerMaster.ManufacturerMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<ManufacturerMaster> manufacturerMasterlsit = new List<ManufacturerMaster>();
            ManufactureManager manufactureManager = new ManufactureManager();
            manufacturerMasterlsit = manufactureManager.Get();
            manufacturerMasterlsit = manufacturerMasterlsit.Where(x => x.ManufacturerName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            ManufacturerModel model = new ManufacturerModel();
            model.ManufacturerMasterList = manufacturerMasterlsit;
            return PartialView("Partial/ManufactureGrid", model);
        }
        #endregion

    }
}
