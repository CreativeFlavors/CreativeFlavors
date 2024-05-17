using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.GatePassModel;
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
    public class  GatePassMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult  GatePassMaster()
        {
             GatePassModel vm = new GatePassModel();
            return View(vm);
        }
        public ActionResult GatePassMasterGrid()
        {
            GatePassModel vm = new GatePassModel();
            GatePassManager gatePassManager = new GatePassManager();
            vm.GatePassMasterList = gatePassManager.Get();
            ;
            return PartialView("Partial/GatePassMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult GatePassMasterDetails(int GatePassId)
        {
        
             GatePassManager gatePassManager = new GatePassManager();
            GatePassMaster gatePassMaster = new GatePassMaster();
            GatePassModel model = new GatePassModel();                 

            gatePassMaster = gatePassManager.GetGatePassID(GatePassId);
            if (gatePassMaster != null)
            {
                model.GatePassId = gatePassMaster.GatePassId;
                model.GatePassNo = gatePassMaster.GatePassNo;
                model.IsSupplier = gatePassMaster.IsSupplier;
                model.Date = Convert.ToDateTime(gatePassMaster.Date).Date.ToString("dd/MM/yyyy");
                model.PartyName = gatePassMaster.PartyName;
                model.DeliveryAddress = gatePassMaster.DeliveryAddress;
                model.MaterialName = gatePassMaster.Material;
                model.Quantity = gatePassMaster.Quantity;
                model.UOM = gatePassMaster.UOM;
                model.Rate = gatePassMaster.Rate;
                model.Amount = gatePassMaster.Amount;
                model.IfReturnable = Convert.ToDateTime(gatePassMaster.IfReturnable).Date.ToString("dd/MM/yyyy");  
                model.Remarks = gatePassMaster.Remarks;
                model.IsPrintWithRateValue = gatePassMaster.IsPrintWithRateValue;
                model.IsPrintWithoutCompanyAddress = gatePassMaster.IsPrintWithoutCompanyAddress;

                model.CreatedDate = model.CreatedDate;
                model.UpdatedDate = DateTime.Now;
                model.CreatedBy = gatePassMaster.CreatedBy;
                model.UpdatedBy = gatePassMaster.UpdatedBy;
            }
            return PartialView("Partial/GatePassMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult gatePassMaster(GatePassModel model)
        {
            GatePassMaster gatePassMasters = new GatePassMaster();
            GatePassMaster gatePassMaster = new GatePassMaster();
            if (ModelState.IsValid)
            {

               GatePassManager gatePassManager = new GatePassManager();
                gatePassMaster = gatePassManager.GetMaterialName(model.MaterialName);
                if (gatePassMaster == null)
                {
                    gatePassMasters.GatePassId = model.GatePassId;
                    gatePassMasters.GatePassNo = model.GatePassNo;
                    gatePassMasters.IsSupplier = model.IsSupplier;
                    var format = "dd/MM/yyyy";
                    DateTime Date = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                    DateTime IfReturnable = DateTime.ParseExact(model.IfReturnable, format, CultureInfo.InvariantCulture);
                    gatePassMasters.Date = Date;
                    gatePassMasters.PartyName = model.PartyName;
                    gatePassMasters.DeliveryAddress = model.DeliveryAddress;
                    gatePassMasters.Material = model.MaterialName;
                    gatePassMasters.Quantity = model.Quantity;
                    gatePassMasters.UOM = model.UOM;
                    gatePassMasters.Rate = model.Rate;
                    gatePassMasters.Amount = model.Amount;
                    gatePassMasters.IfReturnable = IfReturnable;
                    gatePassMasters.Remarks = model.Remarks;
                    gatePassMasters.IsPrintWithRateValue = model.IsPrintWithRateValue;
                    gatePassMasters.IsPrintWithoutCompanyAddress = model.IsPrintWithoutCompanyAddress;
                    gatePassMasters.CreatedDate = model.CreatedDate;
                    gatePassManager.Post(gatePassMasters);
                }
                else
                {
                    return Json(gatePassMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(gatePassMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(GatePassModel model)
        {
            GatePassMaster gatePassMasters = new GatePassMaster();
            GatePassMaster gatePassMaster = new GatePassMaster();
            if (ModelState.IsValid)
            {
                //SeasonMaster seasonMaster = new SeasonMaster();
               GatePassManager gatePassManager = new GatePassManager();
               gatePassMaster = gatePassManager.GetGatePassID(model.GatePassId);
               if (gatePassMaster != null)
                {
                    gatePassMasters.GatePassId = model.GatePassId;
                    gatePassMasters.GatePassNo = model.GatePassNo;
                    gatePassMasters.IsSupplier = model.IsSupplier;
                    var format = "dd/MM/yyyy";
                    DateTime Date = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                    DateTime IfReturnable = DateTime.ParseExact(model.IfReturnable, format, CultureInfo.InvariantCulture);

                    gatePassMasters.Date =  Date;
                    gatePassMasters.PartyName = model.PartyName;
                    gatePassMasters.DeliveryAddress = model.DeliveryAddress;
                    gatePassMasters.Material = model.MaterialName;
                    gatePassMasters.Quantity = model.Quantity;
                    gatePassMasters.UOM = model.UOM;
                    gatePassMasters.Rate = model.Rate;
                    gatePassMasters.Amount = model.Amount;
                    gatePassMasters.IfReturnable =  IfReturnable;
                    gatePassMasters.Remarks = model.Remarks;
                    gatePassMasters.IsPrintWithRateValue = model.IsPrintWithRateValue;
                    gatePassMasters.IsPrintWithoutCompanyAddress = model.IsPrintWithoutCompanyAddress;

                    gatePassManager.Put(gatePassMasters);
                }
                else
                {
                    return Json(gatePassMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(gatePassMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int GatePassId)
        {
            GatePassMaster gatePassMaster = new GatePassMaster();
            string status = "";
           GatePassManager gatePassManager = new GatePassManager();
           gatePassMaster = gatePassManager.GetGatePassID(GatePassId);
           if (gatePassMaster != null)
            {
                status = "Success";
                gatePassManager.Delete(gatePassMaster.GatePassId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
           List<GatePassMaster> gatePassList = new List<GatePassMaster>();
           GatePassManager gatePassManager = new GatePassManager();
           gatePassList = gatePassManager.Get();
           gatePassList = gatePassList.Where(x => x.Material.ToString().Trim().Contains(filter.ToLower().Trim())).ToList();
           GatePassModel im = new GatePassModel();
           im.GatePassMasterList = gatePassList;
           return PartialView("Partial/GatePassMasterGrid", im);
        }       
        #endregion
        
    }
}
