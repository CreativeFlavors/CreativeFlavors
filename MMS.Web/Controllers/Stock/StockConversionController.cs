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
    public class StockConversionController : Controller
    {
        //
        // GET: /StockConversion/
        #region Stock Conversion View

        [HttpGet]
        public ActionResult StockConversion()
        {
            StockConversionModel model = new StockConversionModel();
            return View("~/Views/Stock/StockConversion/StockConversion.cshtml", model);
        }

        public ActionResult StockConversionGrid()
        {
            StockConversionModel model = new StockConversionModel();
            StockConversionManager stockConversionManager = new StockConversionManager();
            model.StockConversionFormList = stockConversionManager.Get();

            return PartialView("~/Views/Stock/StockConversion/Partial/StockConversionGrid.cshtml", model);
        }

        [HttpPost]
        public ActionResult StockConversionDetails(int StockConversionId)
        {
            StockConversionManager stockConversionManager = new StockConversionManager();
            StockConversionForm stockConversionForm = new StockConversionForm();
            StockConversionModel model = new StockConversionModel();
            stockConversionForm = stockConversionManager.GetStockConversionId(StockConversionId);
            if (stockConversionForm.StockConversionId != 0)
            {
                model.StockConversionId = stockConversionForm.StockConversionId;
                model.DocNo = stockConversionForm.DocNo;
                model.FromStore = stockConversionForm.FromStore;
                model.ToStore = stockConversionForm.ToStore;
                model.Remarks = stockConversionForm.Remarks;
                model.Date =Convert.ToDateTime(stockConversionForm.Date).Date.ToString("dd/MM/yyyy");
                model.StockValueChange = stockConversionForm.StockValueChange;
                model.MaterialGroupId = stockConversionForm.MaterialGroupId;
                model.UomId = stockConversionForm.UomId;
                model.MaterialMasterId = stockConversionForm.MaterialMasterId;
                model.ColourMasterId = stockConversionForm.ColourMasterId;
                model.IoNo = stockConversionForm.IoNo;
                model.Quantity = stockConversionForm.Quantity;
                model.Rate = stockConversionForm.Rate;
                model.ReservedStock = stockConversionForm.ReservedStock;
                model.FreeStock = stockConversionForm.FreeStock;
                model.StockInAllStores = stockConversionForm.StockInAllStores;
                model.StockInCurrentStores = stockConversionForm.StockInCurrentStores;
                model.CreatedDate = stockConversionForm.CreatedDate;
                model.UpdatedDate = DateTime.Now;

            }
            return PartialView("~/Views/Stock/StockConversion/Partial/StockConversionDetails.cshtml", model);
        }

        #endregion

        #region Crud Operations
        [HttpPost]
        public ActionResult StockConversion(StockConversionModel model)
        {
            StockConversionForm stockConversionForm = new StockConversionForm();
            if (ModelState.IsValid)
            {
                StockConversionManager stockConversionManager = new StockConversionManager();

                stockConversionForm.DocNo = model.DocNo;
                stockConversionForm.FromStore = model.FromStore;
                stockConversionForm.ToStore = model.ToStore;
                stockConversionForm.Remarks = model.Remarks;
                var format = "dd/MM/yyyy";
                DateTime SCdate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                stockConversionForm.Date = SCdate.Date;
                stockConversionForm.StockValueChange = model.StockValueChange;
                stockConversionForm.MaterialGroupId = model.MaterialGroupId;
                stockConversionForm.UomId = model.UomId;
                stockConversionForm.MaterialMasterId = model.MaterialMasterId;
                stockConversionForm.ColourMasterId = model.ColourMasterId;
                stockConversionForm.IoNo = model.IoNo;
                stockConversionForm.Quantity = model.Quantity;
                stockConversionForm.Rate = model.Rate;
                stockConversionForm.ReservedStock = model.ReservedStock;
                stockConversionForm.FreeStock = model.FreeStock;
                stockConversionForm.StockInAllStores = model.StockInAllStores;
                stockConversionForm.StockInCurrentStores = model.StockInCurrentStores;
                stockConversionForm.CreatedDate = DateTime.Now;
                stockConversionManager.Post(stockConversionForm);
            }
            return Json(stockConversionForm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(StockConversionModel model)
        {
            StockConversionForm stockConversionForm = new StockConversionForm();
            if (ModelState.IsValid)
            {
                StockConversionForm stockConversion = new StockConversionForm();
                StockConversionManager stockConversionManager = new StockConversionManager();
                stockConversionForm = stockConversionManager.GetStockConversionId(model.StockConversionId);
                if (stockConversionForm != null)
                {
                    stockConversionForm.DocNo = model.DocNo;
                    stockConversionForm.FromStore = model.FromStore;
                    stockConversionForm.ToStore = model.ToStore;
                    stockConversionForm.Remarks = model.Remarks;
                    var format = "dd/MM/yyyy";
                    DateTime SCdate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                    stockConversionForm.Date = SCdate.Date;
                    stockConversionForm.StockValueChange = model.StockValueChange;
                    stockConversionForm.MaterialGroupId = model.MaterialGroupId;
                    stockConversionForm.UomId = model.UomId;
                    stockConversionForm.MaterialMasterId = model.MaterialMasterId;
                    stockConversionForm.ColourMasterId = model.ColourMasterId;
                    stockConversionForm.IoNo = model.IoNo;
                    stockConversionForm.Quantity = model.Quantity;
                    stockConversionForm.Rate = model.Rate;
                    stockConversionForm.ReservedStock = model.ReservedStock;
                    stockConversionForm.FreeStock = model.FreeStock;
                    stockConversionForm.StockInAllStores = model.StockInAllStores;
                    stockConversionForm.StockInCurrentStores = model.StockInCurrentStores;

                    stockConversionManager.Put(stockConversionForm);

                }
                else
                {
                    return Json(stockConversionForm, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(stockConversionForm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int StockConversionId)
        {
            string status = "";
            StockConversionManager stockConversionManager = new StockConversionManager();
            StockConversionForm stockConversionForm = new StockConversionForm();
            stockConversionForm = stockConversionManager.GetStockConversionId(StockConversionId);
            if (stockConversionForm != null)
            {
                status = "Success";
                stockConversionManager.Delete(stockConversionForm.StockConversionId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helper Method

        public ActionResult Search(string filter)
        {
            List<StockConversionForm> stockConversionFormlist = new List<StockConversionForm>();
            StockConversionManager stockConversionManager = new StockConversionManager();
            stockConversionFormlist = stockConversionManager.Get();
            stockConversionFormlist = stockConversionFormlist.Where(s => s.DocNo.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            StockConversionModel model = new StockConversionModel();
            model.StockConversionFormList = stockConversionFormlist;

            return PartialView("~/Views/Stock/StockConversion/Partial/StockConversionGrid.cshtml", model);
        }
        #endregion
    }
}
