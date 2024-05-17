using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class StockTransferController : Controller
    {
        //
        // GET: /StockTransfer/

        public ActionResult StockTransfer()
        {
            StockTransferModel model = new StockTransferModel();
            StockTransferManager StockTransferManager = new StockTransferManager();
            model.StockTransferList = StockTransferManager.Get();
            return View("~/Views/Stock/StockTransfer/StockTransfer.cshtml", model);
        }

        [HttpPost]
        public ActionResult StockTransferDetails(int StockTransferID)
        {
            StockTransfer Transmodel = new StockTransfer();
            StockTransferModel model = new StockTransferModel();
            StockTransferManager StockTransferManager = new StockTransferManager();
            Transmodel = StockTransferManager.GetStockTransferByID(StockTransferID);

            model.StockTransferID = Transmodel.StockTransferID;
            model.TypeId = Transmodel.TypeId;
            model.MaterialCategoryID = Transmodel.MaterialCategoryID;
            model.DcGrnNo = Transmodel.DcGrnNo;
            model.MaterialGroupID = Transmodel.MaterialGroupID;
            model.MaterialGroupID = Transmodel.MaterialGroupID;
            model.ColorID = Transmodel.ColorID;
            model.Stores = Transmodel.Stores;
            model.QuantityAmount = Transmodel.QuantityAmount;
            model.QuantityValue = Transmodel.QuantityValue;
            model.TransportDetails = Transmodel.TransportDetails;
            model.ClosingStockInAllStores = Transmodel.ClosingStockInAllStores;
            model.RefDcNo = Transmodel.RefDcNo;
            model.ClosingStockInCurrentStores = Transmodel.ClosingStockInCurrentStores;
            model.Remarks = Transmodel.Remarks;
            model.ReservedStock = Transmodel.ReservedStock;
            model.Rate = Transmodel.Rate;
            model.Value = Transmodel.Value;
            model.PartyDcNo = Transmodel.PartyDcNo;
            model.InvoiceRef = Transmodel.InvoiceRef;
            model.FreeStock = Transmodel.FreeStock;
            model.InvoiceDate = Convert.ToDateTime(Transmodel.InvoiceDate).Date.ToString("dd/MM/yyyy"); 

            return PartialView("~/Views/Stock/StockTransfer/Partial/StockTransferDetails.cshtml", model);
        }

        [HttpPost]
        public ActionResult StockTransfer(FormCollection Collection)
        {
            StockTransfer model = new StockTransfer();
            StockTransferManager StockTransferManager = new StockTransferManager();
            model.StockTransferID = Convert.ToInt32(Collection["StockTransferID"]);
            model.TypeId = Convert.ToInt32(Collection["TypeId"]);            
            model.MaterialCategoryID = Convert.ToInt32(Collection["MaterialCategoryID"]);
            model.DcGrnNo = Convert.ToInt32(Collection["DcGrnNo"]);
            model.MaterialGroupID = Convert.ToInt32(Collection["MaterialGroupID"]);
            model.IssuedToFrom = Convert.ToInt32(Collection["IssuedToFrom"]);
            model.ColorID = Convert.ToInt32(Collection["ColorID"]);
            model.Stores = Convert.ToInt32(Collection["Stores"]);
            model.QuantityAmount = Collection["QuantityAmount"];
            model.QuantityValue = Convert.ToInt32(Collection["QuantityValue"]);
            model.TransportDetails = Collection["TransportDetails"];
            model.ClosingStockInAllStores = Collection["ClosingStockInAllStores"];
            model.RefDcNo = Convert.ToInt32(Collection["RefDcNo"]);
            model.ClosingStockInCurrentStores = Collection["ClosingStockInCurrentStores"];
            model.Remarks = Collection["Remarks"];
            model.ReservedStock = Collection["ReservedStock"];
            model.Rate = Collection["Rate"];
            model.Value = Collection["Value"];
            model.PartyDcNo = Collection["PartyDcNo"];
            model.InvoiceRef = Collection["InvoiceRef"];
            model.FreeStock = Collection["FreeStock"];
            var format = "dd/MM/yyyy";
            string invoiceDate_ = Collection["InvoiceDate"].ToString();
            DateTime STinvoicedate = DateTime.ParseExact(invoiceDate_, format ,CultureInfo.InvariantCulture);
            model.InvoiceDate = STinvoicedate.Date;

            if(model.StockTransferID == 0)
            {
                StockTransferManager.Post(model);
            }
            else
            {
                StockTransferManager.Put(model);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int StockTransferID)
        {
            bool result = false;
             StockTransferManager StockTransferManager = new StockTransferManager();
             result = StockTransferManager.Delete(StockTransferID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #region Helper Method

        public ActionResult Search(string filter)
        {
            List<Core.Entities.Stock.StockTransfer> stockTransferList = new List<Core.Entities.Stock.StockTransfer>();
            StockTransferManager stockTransferManager = new StockTransferManager();
            stockTransferList = stockTransferManager.Get();
            stockTransferList = stockTransferList.Where(x => x.MaterialGroupID.ToString().Contains(filter)).ToList();
            StockTransferModel model = new StockTransferModel();
            model.StockTransferList = stockTransferList;
            return PartialView("Partial/StockTransferMasterGrid", model);
        }

        #endregion
    }
}
