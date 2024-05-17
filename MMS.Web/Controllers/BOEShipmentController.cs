using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.BOEShipmentModel;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Common;
using System.Globalization;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class BOEShipmentController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult BOEMaster()
        {
            BOEShipmentModel vm = new BOEShipmentModel();
            return View(vm);
        }
        public ActionResult BOEMasterGrid()
        {
            BOEShipmentModel vm = new BOEShipmentModel();
            BOEShipmentManager boManager = new BOEShipmentManager();
            vm.BOEShipmentMastetList = boManager.Get();

            return PartialView("Partial/BOEMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult BOEMasterDetails(int BOEId)
        {
            BOEShipmentManager bOEShipmentManager = new BOEShipmentManager();
            BOEShipmentMaster boeMaster = new BOEShipmentMaster();
            BOEShipmentModel model = new BOEShipmentModel();
            boeMaster = bOEShipmentManager.GetBOEShipmentID(BOEId);
            if (boeMaster != null)
            {
                model.BOEId = boeMaster.BOEId;
                model.CountryStamp = boeMaster.CountryStamp;
                model.ShipmentFrom = Convert.ToDateTime(boeMaster.ShipmentFrom).Date.ToString("dd/MM/yyyy");
                model.ShipmentTo = Convert.ToDateTime(boeMaster.ShipmentTo).Date.ToString("dd/MM/yyyy");

                model.OtherInstructionFromBuyer = boeMaster.OtherInstructionFromBuyer;
              
                model.CreatedBy = boeMaster.CreatedBy;
                model.UpdatedBy = boeMaster.UpdatedBy;
                model.CreatedDate = boeMaster.CreatedDate;
                model.UpdatedDate = boeMaster.UpdatedDate;
            }
            return PartialView("Partial/BOEMasterDetails", model);
        }
        #endregion
        #region Curd Operation
        [HttpPost]
        public ActionResult BOEMaster(BOEShipmentModel model)
        {
            BOEShipmentMaster boeMasters = new BOEShipmentMaster();
            BOEShipmentMaster boeMaster = new BOEShipmentMaster();
            if (ModelState.IsValid)
            {
                BOEShipmentManager bOEShipmentManager = new BOEShipmentManager();
                boeMaster = bOEShipmentManager.GetCountryStamp(model.CountryStamp);
                if (boeMaster == null)
                {
                    boeMasters.BOEId = model.BOEId;
                    boeMasters.CountryStamp = model.CountryStamp;
                    var format = "dd/MM/yyyy";
                    DateTime ShipmtFrom = DateTime.ParseExact(model.ShipmentFrom, format, CultureInfo.InvariantCulture);
                    DateTime ShipmtTo = DateTime.ParseExact(model.ShipmentTo, format, CultureInfo.InvariantCulture);

                    boeMasters.ShipmentFrom = ShipmtFrom.Date;
                    boeMasters.ShipmentTo = ShipmtTo.Date;
                    boeMasters.OtherInstructionFromBuyer = model.OtherInstructionFromBuyer;
                    boeMasters.CreatedDate = DateTime.Now;
                    bOEShipmentManager.Post(boeMasters);
                }
                else
                {
                    return Json(boeMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(boeMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(BOEShipmentModel model)
        {
            BOEShipmentMaster boeMasters = new BOEShipmentMaster();
            BOEShipmentMaster boeMaster = new BOEShipmentMaster();
            if (ModelState.IsValid)
            {
                BOEShipmentManager bOEShipmentManager = new BOEShipmentManager();
                boeMaster = bOEShipmentManager.GetBOEShipmentID(model.BOEId);
                if (boeMaster != null)
                {
                    boeMasters.BOEId = model.BOEId;
                    boeMasters.CountryStamp = model.CountryStamp;
                    var format = "dd/MM/yyyy";
                    DateTime ShipmtFrom = DateTime.ParseExact(model.ShipmentFrom, format, CultureInfo.InvariantCulture);
                    DateTime ShipmtTo = DateTime.ParseExact(model.ShipmentTo, format, CultureInfo.InvariantCulture);

                    boeMasters.ShipmentFrom = ShipmtFrom.Date;
                    boeMasters.ShipmentTo = ShipmtTo.Date;

                    boeMasters.OtherInstructionFromBuyer = model.OtherInstructionFromBuyer;
                    bOEShipmentManager.Put(boeMasters);
                }
                else
                {
                    return Json(boeMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(boeMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int BOEId)
        {
            BOEShipmentMaster boeMaster = new BOEShipmentMaster();
            string status = "";
            BOEShipmentManager boeShipManager = new BOEShipmentManager();
            boeMaster = boeShipManager.GetBOEShipmentID(BOEId);
            if (boeMaster != null)
            {
                status = "Success";
                boeShipManager.Delete(boeMaster.BOEId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<BOEShipmentMaster> boeMasterList = new List<BOEShipmentMaster>();
            BOEShipmentManager boeShipManager = new BOEShipmentManager();
            boeMasterList = boeShipManager.Get();
            boeMasterList = boeMasterList.Where(x => x.CountryStamp.ToLower().Contains(filter.ToLower())).ToList();
            BOEShipmentModel im = new BOEShipmentModel();
            im.BOEShipmentMastetList = boeMasterList;
            return PartialView("Partial/BOEMasterGrid", im);
        }
        #endregion


    }
}
