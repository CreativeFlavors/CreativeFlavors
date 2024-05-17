using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.BasicUnitMasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class BasicUnitMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult BasicUnitMaster()
        {
            BasicUnitMasterModel vm = new BasicUnitMasterModel();
            return View(vm);
        }
        public ActionResult BasicUnitMasterGrid()
        {
            BasicUnitMasterModel vm = new BasicUnitMasterModel();
            UOMManager uOMManager = new UOMManager();
            BasicUnitManager unitConversionManager = new BasicUnitManager();

            vm.BasicUnitMasterList = uOMManager.Get();

            return PartialView("Partial/BasicUnitMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult BasicUnitMasterDetails(int UomMasterId)
        {
            UomMaster BasicUnitMaster = new UomMaster();
            UOMManager BasicUnitManager = new UOMManager();
            BasicUnitMasterModel model = new BasicUnitMasterModel();
            if (UomMasterId != 0)
            {
                BasicUnitMaster = BasicUnitManager.GetUomMasterId(UomMasterId);
                model.LongUnitName = BasicUnitMaster.LongUnitName;
                model.ShortUnitName = BasicUnitMaster.ShortUnitName;
                model.CreatedDate = BasicUnitMaster.CreatedDate.Value;
                model.UpdatedBy = BasicUnitMaster.UpdatedBy;
                model.IsDeleted = BasicUnitMaster.IsDeleted;
                model.DeletedBy = BasicUnitMaster.DeletedBy;
                model.UomMasterId = BasicUnitMaster.UomMasterId;
            }

            return PartialView("Partial/BasicUnitMasterDetails", model);
        }
        #endregion

        #region Curd Operation
    

        [HttpPost]
        public ActionResult CreateUnitDetails(UomMaster model)
        {
            bool Result = false;
            UomMaster uomMaster = new UomMaster();
            UOMManager UOMManager = new UOMManager();
            model.IsDeleted = false;
            model.CreatedDate = DateTime.Now;
            uomMaster = UOMManager.GetUnitName(model.ShortUnitName);
            if (uomMaster == null)
            {
                uomMaster = UOMManager.Post(model);
            }
            else if (uomMaster != null && model.ShortUnitName == uomMaster.ShortUnitName && model.UomMasterId == 0)
            {
                model.UomMasterId = 0;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
           
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Update(UomMaster model)
        {
            UomMaster uomMaster = new UomMaster();
            UOMManager UOMManager = new UOMManager();
            bool Result = false;
            model.UomMasterId = model.UomMasterId;
            model.LongUnitName = model.LongUnitName;
            model.ShortUnitName = model.ShortUnitName;
            model.IsDeleted = false;
            Result = UOMManager.Put(model);

            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Delete(int UomMasterId)
        {
            UomMaster uomMaster = new UomMaster();
            string status = "";
            UOMManager uOMManager = new UOMManager();
            uomMaster = uOMManager.GetUomMasterId(UomMasterId);
            if (uomMaster != null)
            {
                status = "Success";
                uOMManager.Delete(uomMaster.UomMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<UomMaster> UOMMasterlist = new List<UomMaster>();
            UOMManager uOMManager = new UOMManager();
            UOMMasterlist = uOMManager.Get();
            UOMMasterlist = UOMMasterlist.Where(x => x.ShortUnitName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.LongUnitName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            BasicUnitMasterModel model = new BasicUnitMasterModel();
             model.BasicUnitMasterList = UOMMasterlist;
            return PartialView("Partial/BasicUnitMasterGrid", model);
        }
        #endregion

    }
}
