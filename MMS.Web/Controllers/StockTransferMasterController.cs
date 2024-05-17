using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.StockTransferModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class StockTransferMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult StockTransferMaster()
        {
            StockTransferModel vm = new StockTransferModel();
            return View(vm);
        }
        public ActionResult StockTransferMasterGrid()
        {
            StockTransferModel vm = new StockTransferModel();
            StockTransferManager stockManager = new StockTransferManager();
            vm.StockTransferMasterList = stockManager.Get();
            return PartialView("Partial/StockTransferMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult StockTransferMasterDetails(int StockTransferID)
        {
            StockTransferManager stockManager = new StockTransferManager();
            StockTransferMaster stockMaster = new  StockTransferMaster();
            StockTransferModel model = new StockTransferModel();
            stockMaster = stockManager.GetStockTransferMasterId(StockTransferID);
            if (stockMaster != null)
            {
                model.StockTransferID =stockMaster.StockTransferID;
                model.GrnTypeMasterId =stockMaster.GrnTypeMasterId;
                model.GRNNo =stockMaster.GRNNo;
                model.IssuedTo =stockMaster.IssuedTo;
                model.StoreMasterId =stockMaster.StoreMasterId;
                model.TransportDetails =stockMaster.TransportDetails;
                model.Remarks =stockMaster.Remarks;
                model.Rate =stockMaster.Rate;
                model.Value =stockMaster.Value;
                model.PartyDCNo = stockMaster.PartyDCNo;
                model.MatCategoryId =stockMaster.MatCategoryId;
                model.MatGroupId =stockMaster.MatGroupId;
                model.ColourId =stockMaster.ColourId;
                model.Quantity =stockMaster.Quantity;
                model.ClosingStockInAllStores =stockMaster.ClosingStockInAllStores;
                model.ClosingStockingInCurrentStores =stockMaster.ClosingStockingInCurrentStores;
                model.ReservedStock =stockMaster.ReservedStock;
                model.FreeStock =stockMaster.FreeStock;
                model.InvoiceRef = Convert.ToDateTime(stockMaster.InvoiceRef ).Date.ToString("dd/MM/yyyy");
                model.InvoiceDate = Convert.ToDateTime(stockMaster.InvoiceDate).Date.ToString("dd/MM/yyyy"); 
                model.CreatedBy = stockMaster.CreatedBy;
                model.UpdatedBy = stockMaster.UpdatedBy;
                model.CreatedDate = stockMaster.CreatedDate;
                model.UpdatedDate = stockMaster.UpdatedDate;

              
            }
            return PartialView("Partial/StockTransferMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult  StockTransferMaster(StockTransferModel model)
        {
             StockTransferMaster StockTransferMasters = new  StockTransferMaster();
             StockTransferMaster StockTransferMaster = new StockTransferMaster();
            if (ModelState.IsValid)
            {
                StockTransferManager stockManager = new StockTransferManager();
                StockTransferMaster = stockManager.GetGRNNo(model.GRNNo);
                if (StockTransferMaster == null)
                {
                    StockTransferMasters.StockTransferID =  model.StockTransferID;
                    StockTransferMasters.GrnTypeMasterId =  model.GrnTypeMasterId;
                    StockTransferMasters.GRNNo =  model.GRNNo;
                    StockTransferMasters.IssuedTo =  model.IssuedTo;
                    StockTransferMasters.StoreMasterId =  model.StoreMasterId;
                    StockTransferMasters.TransportDetails =  model.TransportDetails;
                    StockTransferMasters.Remarks =  model.Remarks;
                    StockTransferMasters.Rate =  model.Rate;
                    StockTransferMasters.Value =  model.Value;
                    StockTransferMasters.PartyDCNo = model.PartyDCNo;
                    StockTransferMasters.MatCategoryId =  model.MatCategoryId;
                    StockTransferMasters.MatGroupId =  model.MatGroupId;
                    StockTransferMasters.ColourId =  model.ColourId;
                    StockTransferMasters.Quantity =  model.Quantity;
                    StockTransferMasters.ClosingStockInAllStores =  model.ClosingStockInAllStores;
                    StockTransferMasters.ClosingStockingInCurrentStores =  model.ClosingStockingInCurrentStores;
                    StockTransferMasters.ReservedStock =  model.ReservedStock;
                    StockTransferMasters.FreeStock =  model.FreeStock;

                    var format = "dd/MM/yyyy";
                    DateTime InvoiceRef = DateTime.ParseExact(model.InvoiceRef, format, CultureInfo.InvariantCulture);
                    DateTime InvoiceDate = DateTime.ParseExact(model.InvoiceDate, format, CultureInfo.InvariantCulture);
                    StockTransferMasters.InvoiceRef =   InvoiceRef;
                    StockTransferMasters.InvoiceDate =  InvoiceDate;
                    StockTransferMasters.CreatedBy =  model.CreatedBy;
                    StockTransferMasters.UpdatedBy =  model.UpdatedBy;
                    StockTransferMasters.CreatedDate =  model.CreatedDate;
                    StockTransferMasters.UpdatedDate =  model.UpdatedDate;
                    StockTransferMasters.CreatedDate = DateTime.Now;
                    stockManager.Post(StockTransferMasters);
                }
                else
                {
                    return Json(StockTransferMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(StockTransferMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(StockTransferModel model)
        {
            StockTransferMaster StockTransferMasters = new StockTransferMaster();
            StockTransferMaster StockTransferMaster = new StockTransferMaster();
            if (ModelState.IsValid)
            {
                StockTransferManager stockManager = new StockTransferManager();
                StockTransferMaster = stockManager.GetStockTransferMasterId(model.StockTransferID);
                if (StockTransferMaster != null)
                {
                    StockTransferMasters.StockTransferID = model.StockTransferID;
                    StockTransferMasters.GrnTypeMasterId = model.GrnTypeMasterId;
                    StockTransferMasters.GRNNo = model.GRNNo;
                    StockTransferMasters.IssuedTo = model.IssuedTo;
                    StockTransferMasters.StoreMasterId = model.StoreMasterId;
                    StockTransferMasters.TransportDetails = model.TransportDetails;
                    StockTransferMasters.Remarks = model.Remarks;
                    StockTransferMasters.Rate = model.Rate;
                    StockTransferMasters.Value = model.Value;
                    StockTransferMasters.PartyDCNo = model.PartyDCNo;
                    StockTransferMasters.MatCategoryId = model.MatCategoryId;
                    StockTransferMasters.MatGroupId = model.MatGroupId;
                    StockTransferMasters.ColourId = model.ColourId;
                    StockTransferMasters.Quantity = model.Quantity;
                    StockTransferMasters.ClosingStockInAllStores = model.ClosingStockInAllStores;
                    StockTransferMasters.ClosingStockingInCurrentStores = model.ClosingStockingInCurrentStores;
                    StockTransferMasters.ReservedStock = model.ReservedStock;
                    StockTransferMasters.FreeStock = model.FreeStock;

                    var format = "dd/MM/yyyy";
                    DateTime InvoiceRef = DateTime.ParseExact(model.InvoiceRef, format, CultureInfo.InvariantCulture);
                    DateTime InvoiceDate = DateTime.ParseExact(model.InvoiceDate, format, CultureInfo.InvariantCulture);
                    
                    StockTransferMasters.InvoiceRef = InvoiceRef;
                    StockTransferMasters.InvoiceDate =InvoiceDate;
                  
                    stockManager.Put(StockTransferMasters);
                }
                else
                {
                    return Json(StockTransferMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(StockTransferMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int StockTransferID)
        {
             StockTransferMaster stockMaster = new  StockTransferMaster();
            string status = "";
            StockTransferManager stockManager = new StockTransferManager();
            stockMaster = stockManager.GetStockTransferMasterId(StockTransferID);
            if (stockMaster != null)
            {
                status = "Success";
                stockManager.Delete(stockMaster.StockTransferID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<StockTransferMaster> stockMasterList = new List< StockTransferMaster>();
            StockTransferManager stockManager = new StockTransferManager();
            stockMasterList = stockManager.Get();
            stockMasterList = stockMasterList.Where(x => x.GRNNo.ToLower().Contains(filter.ToLower()) || x.TransportDetails.ToLower().Contains(filter.ToLower())).ToList();
            StockTransferModel sm = new StockTransferModel();
            sm.StockTransferMasterList = stockMasterList;
            return PartialView("Partial/StockTransferMasterGrid", sm);
        }
        #endregion
    }
}
