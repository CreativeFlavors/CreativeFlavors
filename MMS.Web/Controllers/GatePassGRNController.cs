using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.GatePassGRNModel;
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
    public class GatePassGRNMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult GatePassGRNMaster()
        {
            GatePassGRNModel vm = new GatePassGRNModel();
            return View(vm);
        }
        public ActionResult GatePassGRNMasterGrid()
        {
            GatePassGRNModel vm = new GatePassGRNModel();
            GatePassGRNManager GatePassGRNManager = new GatePassGRNManager();
            vm.GatePassGRNMasterList = GatePassGRNManager.Get();
            return PartialView("Partial/GatePassGRNMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult GatePassGRNMasterDetails(int GatePassInvwardId)
        {

            GatePassGRNManager GatePassGRNManager = new GatePassGRNManager();
            GatePassGRNMaster GatePassGRNMaster = new GatePassGRNMaster();
            GatePassGRNModel model = new GatePassGRNModel();

            GatePassGRNMaster = GatePassGRNManager.GetGatePassGRNID(GatePassInvwardId);
            if (GatePassGRNMaster != null)
            {
                model.GatePassInvwardId = GatePassGRNMaster.GatePassInvwardId;
                model.ReceiptNo = GatePassGRNMaster.ReceiptNo;
                model.IsSupplier = GatePassGRNMaster.IsSupplier;
                model.Date = Convert.ToDateTime(GatePassGRNMaster.Date).Date.ToString();
                model.PartyName = GatePassGRNMaster.PartyName;
                model.RefGatePassNo = GatePassGRNMaster.RefGatePassNo;
                model.MaterialName = GatePassGRNMaster.Material;
                model.Quantity = GatePassGRNMaster.Quantity;
                model.UOM = GatePassGRNMaster.UOM;
                model.Rate = GatePassGRNMaster.Rate;
                model.Amount = GatePassGRNMaster.Amount;            
                model.Instructions = GatePassGRNMaster.Instructions;
                model.IsPrintWithRateValue = GatePassGRNMaster.IsPrintWithRateValue;
                model.IsPrintWithoutCompanyAddress = GatePassGRNMaster.IsPrintWithoutCompanyAddress;
                model.CreatedDate = model.CreatedDate;
                model.UpdatedDate = DateTime.Now;
                model.CreatedBy = GatePassGRNMaster.CreatedBy;
                model.UpdatedBy = GatePassGRNMaster.UpdatedBy;
            }
            return PartialView("Partial/GatePassGRNMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult GatePassGRNMaster(GatePassGRNModel model)
        {
            GatePassGRNMaster GatePassGRNMasters = new GatePassGRNMaster();
            GatePassGRNMaster GatePassGRNMaster = new GatePassGRNMaster();
            if (ModelState.IsValid)
            {

                GatePassGRNManager GatePassGRNManager = new GatePassGRNManager();
                GatePassGRNMaster = GatePassGRNManager.GetMaterialName(model.MaterialName);
                
                    GatePassGRNMasters.GatePassInvwardId = model.GatePassInvwardId;
                    GatePassGRNMasters.ReceiptNo = model.ReceiptNo;
                    GatePassGRNMasters.IsSupplier = model.IsSupplier;

                var format = "dd/MM/yyyy";
                DateTime Date = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
               
                   GatePassGRNMasters.Date = Date;
                    GatePassGRNMasters.PartyName = model.PartyName;
                    GatePassGRNMasters.RefGatePassNo = model.RefGatePassNo;
                    GatePassGRNMasters.Material = model.MaterialName;
                    GatePassGRNMasters.Quantity = model.Quantity;
                    GatePassGRNMasters.UOM = model.UOM;
                    GatePassGRNMasters.Rate = model.Rate;
                    GatePassGRNMasters.Amount = model.Amount;
                    GatePassGRNMasters.Instructions = model.Instructions;
                    GatePassGRNMasters.IsPrintWithRateValue = model.IsPrintWithRateValue;
                    GatePassGRNMasters.IsPrintWithoutCompanyAddress = model.IsPrintWithoutCompanyAddress;
                    GatePassGRNMasters.CreatedDate = DateTime.Now;
                    GatePassGRNManager.Post(GatePassGRNMasters);

            }
            return Json(GatePassGRNMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(GatePassGRNModel model)
        {
            GatePassGRNMaster GatePassGRNMasters = new GatePassGRNMaster();
            GatePassGRNMaster GatePassGRNMaster = new GatePassGRNMaster();
            if (ModelState.IsValid)
            {

                GatePassGRNManager GatePassGRNManager = new GatePassGRNManager();
                GatePassGRNMaster = GatePassGRNManager.GetGatePassGRNID(model.GatePassInvwardId);
                
                    GatePassGRNMasters.GatePassInvwardId = model.GatePassInvwardId;
                    GatePassGRNMasters.ReceiptNo = model.ReceiptNo;
                    GatePassGRNMasters.IsSupplier = model.IsSupplier;
                var format = "dd/MM/yyyy";
                DateTime Date = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                GatePassGRNMasters.Date =  Date;
                    GatePassGRNMasters.PartyName = model.PartyName;
                    GatePassGRNMasters.RefGatePassNo = model.RefGatePassNo;
                    GatePassGRNMasters.Material = model.MaterialName;
                    GatePassGRNMasters.Quantity = model.Quantity;
                    GatePassGRNMasters.UOM = model.UOM;
                    GatePassGRNMasters.Rate = model.Rate;
                    GatePassGRNMasters.Amount = model.Amount;

                    GatePassGRNMasters.Instructions = model.Instructions;
                    GatePassGRNMasters.IsPrintWithRateValue = model.IsPrintWithRateValue;
                    GatePassGRNMasters.IsPrintWithoutCompanyAddress = model.IsPrintWithoutCompanyAddress;
                   
                    GatePassGRNManager.Put(GatePassGRNMasters);
                

            }
            return Json(GatePassGRNMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int GatePassInvwardId)
        {
            GatePassGRNMaster gatePassMaster = new GatePassGRNMaster();
            string status = "";
            GatePassGRNManager gatePassManager = new GatePassGRNManager();
            gatePassMaster = gatePassManager.GetGatePassGRNID(GatePassInvwardId);
            if (gatePassMaster != null)
            {
                status = "Success";
                gatePassManager.Delete(gatePassMaster.GatePassInvwardId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<GatePassGRNMaster> gatePassList = new List<GatePassGRNMaster>();
            GatePassGRNManager gatePassManager = new GatePassGRNManager();
            gatePassList = gatePassManager.Get();
            gatePassList = gatePassList.Where(x => x.Material.ToString().Trim().Contains(filter.ToLower().Trim())).ToList();
            GatePassGRNModel im = new GatePassGRNModel();
            im.GatePassGRNMasterList = gatePassList;
            return PartialView("Partial/GatePassGRNMasterGrid", im);
        }
       
        #endregion


    }
}
