using MMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using Newtonsoft.Json;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MMS.Web.Models;

namespace MMS.Web.Controllers.Stock
{

    [CustomFilter]
    public class StockAdjustmentController : Controller
    {

        #region StockAdjustment Form View

        [HttpGet]
        public ActionResult StockAdjustment()
        {
            StockAdjustmentModel model = new StockAdjustmentModel();
            return View("~/Views/Stock/StockAdjustment/StockAdjustment.cshtml", model);
        }
        public ActionResult StockAdjustmentGrid()
        {
            StockAdjustmentModel model = new StockAdjustmentModel();
            StockAdjustmentManager stockadjustmentManager = new StockAdjustmentManager();
            model.StockAdjustmentFormList = stockadjustmentManager.Get();
            return PartialView("~/Views/Stock/StockAdjustment/Partial/StockAdjustmentGrid.cshtml", model);
        }
        [HttpPost]

        public ActionResult StockAdjustmentDetails(int StockAdjustmentId)
        {
            StockAdjustmentManager stockAdjustmentManager = new StockAdjustmentManager();
            StockAdjustmentForm stockAdjustmentForm = new StockAdjustmentForm();
            StockAdjustmentModel model = new StockAdjustmentModel();
            stockAdjustmentForm = stockAdjustmentManager.GetStockAdjustmentId(StockAdjustmentId);
            StockAdjustmentForm obj = new StockAdjustmentForm();
            int ID = 0;
            if (StockAdjustmentId == 0)
            {
                ID = MMS.Web.ExtensionMethod.HtmlHelper.getStockAdjustmentCodeId();
                model.StockNo = ID++.ToString();
            }

            List<SizeItemsStockAdjustment> arg1 = new List<SizeItemsStockAdjustment>();
            arg1 = stockAdjustmentManager.GetSizeQuantityRangeById(StockAdjustmentId);
            if (stockAdjustmentForm.StockAdjustmentId != 0)
            {
                model.StockAdjustmentId = stockAdjustmentForm.StockAdjustmentId;
                model.StockNo = stockAdjustmentForm.StockNo;
                model.AsOnDate = Convert.ToDateTime(stockAdjustmentForm.AsOnDate).Date.ToString("dd/MM/yyyy");
                model.StoreId = stockAdjustmentForm.StoreId;
                model.CategoryId = stockAdjustmentForm.CategoryId;
                model.GroupId = stockAdjustmentForm.GroupId;
                model.SubGroupId = stockAdjustmentForm.SubGroupId;
                model.MaterialType = stockAdjustmentForm.MaterialType;
                model.CreatedDate = stockAdjustmentForm.CreatedDate;
                model.UpdatedDate = DateTime.Now;
                if (arg1 != null)
                {
                    model.SizeRangeQtyRateList = arg1;
                }

            }


            return PartialView("~/Views/Stock/StockAdjustment/Partial/StockAdjustmentDetails.cshtml", model);
        }



        #endregion

        #region Crud Operations
        [HttpPost]
        public ActionResult StockAdjustment(StockAdjustmentModel model)
        {
            StockAdjustmentForm stockAdjustmentForm = new StockAdjustmentForm();
            StockAdjustmentManager stockAdjustmentManager = new StockAdjustmentManager();
            string Message = "";
            if (model.StockAdjustmentId == 0)
            {

                stockAdjustmentForm.StockAdjustmentId = model.StockAdjustmentId;
                var format = "dd/MM/yyyy";
                DateTime SCdate = DateTime.ParseExact(model.AsOnDate, format, CultureInfo.InvariantCulture);
                stockAdjustmentForm.AsOnDate = SCdate.Date;
                stockAdjustmentForm.StoreId = model.StoreId;
                stockAdjustmentForm.CategoryId = model.CategoryId;
                stockAdjustmentForm.GroupId = model.GroupId;
                stockAdjustmentForm.SubGroupId = model.SubGroupId;
                stockAdjustmentForm.StockNo = model.StockNo;
                stockAdjustmentForm.MaterialType = model.MaterialType;
                stockAdjustmentForm.CreatedDate = DateTime.Now;

                stockAdjustmentManager.Post(stockAdjustmentForm);
                var SizeQuantityRate = JsonConvert.DeserializeObject<List<SizeItemsStockAdjustment>>(model.SizeQuantityRate);
                SizeQuantityRate = SizeQuantityRate.Where(x => x.PhysicalStock != null).ToList();
                foreach (var item in SizeQuantityRate)
                {
                    List<StockAdjustMent> isExistsstockadjustmentForm = new List<StockAdjustMent>();
                    isExistsstockadjustmentForm = stockAdjustmentManager.isExistsStockAdjustment(stockAdjustmentForm.AsOnDate, stockAdjustmentForm.CategoryId, stockAdjustmentForm.StoreId, stockAdjustmentForm.GroupId, stockAdjustmentForm.MaterialType.Value, item.MaterialMasterId.Value);
                    if (isExistsstockadjustmentForm != null && isExistsstockadjustmentForm.Count > 0)
                    {
                       // Message = "Already exists";
                      
                    }
                    else
                    {
                        SizeItemsStockAdjustment SizeRangeQtyRateDetails = new SizeItemsStockAdjustment();
                        SizeRangeQtyRateDetails.MaterialDescription = item.MaterialDescription;
                        SizeRangeQtyRateDetails.Size = item.Size;
                        SizeRangeQtyRateDetails.Quantity = item.Quantity;
                        SizeRangeQtyRateDetails.MaterialMasterId = item.MaterialMasterId;
                        SizeRangeQtyRateDetails.ShortUnitName = item.ShortUnitName;
                        SizeRangeQtyRateDetails.Rate = item.Rate;
                        if (item.BookStock == null)
                            SizeRangeQtyRateDetails.BookStock = 0;
                        else
                            SizeRangeQtyRateDetails.BookStock = item.BookStock;
                        if (item.PhysicalStock == null)
                            SizeRangeQtyRateDetails.PhysicalStock = 0;
                        else
                            SizeRangeQtyRateDetails.PhysicalStock = item.PhysicalStock;
                        if (item.VariationStock == null)
                            SizeRangeQtyRateDetails.VariationStock = 0;
                        else
                            SizeRangeQtyRateDetails.VariationStock = item.VariationStock;
                        if (item.PlusStock == null)
                            SizeRangeQtyRateDetails.PlusStock = 0;
                        else
                            SizeRangeQtyRateDetails.PlusStock = item.PlusStock;
                        if (item.MinusStock == null)
                            SizeRangeQtyRateDetails.MinusStock = 0;
                        else
                            SizeRangeQtyRateDetails.MinusStock = item.MinusStock;
                        if (item.BookStockValue == null)
                            SizeRangeQtyRateDetails.BookStockValue = 0;
                        else
                            SizeRangeQtyRateDetails.BookStockValue = item.BookStockValue;
                        if (item.PhysicalStockValue == null)
                            SizeRangeQtyRateDetails.PhysicalStockValue = 0;
                        else
                            SizeRangeQtyRateDetails.PhysicalStockValue = item.PhysicalStockValue;
                        if (item.VariationStockValue == null)
                            SizeRangeQtyRateDetails.VariationStockValue = 0;
                        else
                            SizeRangeQtyRateDetails.VariationStockValue = item.VariationStockValue;
                        if (item.PlusStockValue == null)
                            SizeRangeQtyRateDetails.PlusStockValue = 0;
                        else
                            SizeRangeQtyRateDetails.PlusStockValue = item.PlusStockValue;
                        if (item.MinusStockValue == null)
                            SizeRangeQtyRateDetails.MinusStockValue = 0;
                        else
                            SizeRangeQtyRateDetails.MinusStockValue = item.MinusStockValue;

                        SizeRangeQtyRateDetails.StockAdjustmentFk = stockAdjustmentForm.StockAdjustmentId;
                        SizeRangeQtyRateDetails.CreatedDate = model.CreatedDate;
                        SizeRangeQtyRateDetails.UpdatedDate = DateTime.Now;
                        stockAdjustmentManager.PostStockAdjustmentSizeQtyRate(SizeRangeQtyRateDetails);
                    }
                    Message = "Saved";

                }

                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
           

            }
            else if (model.StockAdjustmentId != 0)
            {
                stockAdjustmentForm = stockAdjustmentManager.GetStockAdjustmentId(model.StockAdjustmentId);
                stockAdjustmentForm.StockAdjustmentId = model.StockAdjustmentId;
                var format = "dd/MM/yyyy";
                DateTime SCdate = DateTime.ParseExact(model.AsOnDate, format, CultureInfo.InvariantCulture);
                stockAdjustmentForm.AsOnDate = SCdate.Date;
                stockAdjustmentForm.StoreId = model.StoreId;
                stockAdjustmentForm.CategoryId = model.CategoryId;
                stockAdjustmentForm.GroupId = model.GroupId;
                stockAdjustmentForm.SubGroupId = model.SubGroupId;
                stockAdjustmentForm.MaterialType = model.MaterialType;
                stockAdjustmentForm.CreatedDate = model.CreatedDate;
                stockAdjustmentForm.UpdatedDate = DateTime.Now;
                stockAdjustmentManager.Post(stockAdjustmentForm);
                if (model.SizeQuantityRate != null)
                {
                    var SizeQuantityRate = JsonConvert.DeserializeObject<List<SizeItemsStockAdjustment>>(model.SizeQuantityRate);
                    SizeQuantityRate = SizeQuantityRate.Where(x => x.PhysicalStock != null).ToList();
                    List<SizeItemsStockAdjustment> sizeRangeQtyRateList = new List<SizeItemsStockAdjustment>();
                    var SizeQuantityRt = stockAdjustmentManager.GetSizeQuantityRangeById(model.StockAdjustmentId).Where(x => x.StockAdjustmentFk == model.StockAdjustmentId).Select(x => x.StockAdjustmentFk).ToList();
                    model.SizeQuantityRate = SizeQuantityRt.ToString();
                    stockAdjustmentManager.DeleteSizeQuantityRangeById(model.StockAdjustmentId);
                    foreach (var item in SizeQuantityRate)
                    {
                        SizeItemsStockAdjustment SizeRangeQtyRateDetails = new SizeItemsStockAdjustment();
                        SizeRangeQtyRateDetails.MaterialDescription = item.MaterialDescription;
                        SizeRangeQtyRateDetails.Size = item.Size;
                        SizeRangeQtyRateDetails.MaterialMasterId = item.MaterialMasterId;
                        SizeRangeQtyRateDetails.Quantity = item.Quantity;
                        SizeRangeQtyRateDetails.ShortUnitName = item.ShortUnitName;
                        SizeRangeQtyRateDetails.Rate = item.Rate;
                        if (item.BookStock == null)
                            item.BookStock = 0;
                        else
                            SizeRangeQtyRateDetails.BookStock = item.BookStock;
                        if (item.PhysicalStock == null)
                            item.PhysicalStock = 0;
                        else
                            SizeRangeQtyRateDetails.PhysicalStock = item.PhysicalStock;
                        if (item.VariationStock == null)
                            item.VariationStock = 0;
                        else
                            SizeRangeQtyRateDetails.VariationStock = item.VariationStock;
                        if (item.PlusStock == null)
                            item.PlusStock = 0;
                        else
                            SizeRangeQtyRateDetails.PlusStock = item.PlusStock;
                        if (item.MinusStock == null)
                            item.MinusStock = 0;
                        else
                            SizeRangeQtyRateDetails.MinusStock = item.MinusStock;
                        if (item.BookStockValue == null)
                            item.BookStockValue = 0;
                        else
                            SizeRangeQtyRateDetails.BookStockValue = item.BookStockValue;
                        if (item.PhysicalStockValue == null)
                            item.PhysicalStockValue = 0;
                        else
                            SizeRangeQtyRateDetails.PhysicalStockValue = item.PhysicalStockValue;
                        if (item.VariationStockValue == null)
                            item.VariationStockValue = 0;
                        else
                            SizeRangeQtyRateDetails.VariationStockValue = item.VariationStockValue;
                        if (item.PlusStockValue == null)
                            item.PlusStockValue = 0;
                        else
                            SizeRangeQtyRateDetails.PlusStockValue = item.PlusStockValue;
                        if (item.MinusStockValue == null)
                            item.MinusStockValue = 0;
                        else
                            SizeRangeQtyRateDetails.MinusStockValue = item.MinusStockValue;
                        SizeRangeQtyRateDetails.StockAdjustmentFk = stockAdjustmentForm.StockAdjustmentId;
                        SizeRangeQtyRateDetails.CreatedDate = model.CreatedDate;
                        SizeRangeQtyRateDetails.UpdatedDate = DateTime.Now;
                        stockAdjustmentManager.PostStockAdjustmentSizeQtyRate(SizeRangeQtyRateDetails);
                    }
                    Message = "Updated";
                    return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int? StockAdjustmentId)
        {
            StockAdjustmentForm stockAdjustmentForm = new StockAdjustmentForm();
            StockAdjustmentManager stockAdjustmentManager = new StockAdjustmentManager();
            string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
            stockAdjustmentManager.Delete(StockAdjustmentId.Value);
            List<SizeItemsStockAdjustment> listSizeItems = new List<SizeItemsStockAdjustment>();
            listSizeItems = stockAdjustmentManager.GetSizeQuantityRangeById(StockAdjustmentId.Value);
            SqlConnection connection = new SqlConnection(consString);
            string Message = string.Empty;
            string sqlStatement = "DELETE FROM SizeItemsStockAdjustment WHERE StockAdjustmentFk =" + StockAdjustmentId.Value;

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                Message = "Deleted Sccuessfully";
            }
            finally
            {
                connection.Close();
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IsApprovedStockAdjustment(int? StockAdjustmentID)
        {
            StockAdjustmentForm stockAdjustmentForm = new StockAdjustmentForm();
            StockAdjustmentManager stockAdjustmentManager = new StockAdjustmentManager();
            if (StockAdjustmentID != 0)
            {
                string Message = string.Empty;
                stockAdjustmentForm = stockAdjustmentManager.GetStockAdjustmentId(StockAdjustmentID.Value);
                if (stockAdjustmentForm.IsApproved == true)
                {
                    Message = "Alredy Approved";
                    return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    stockAdjustmentForm.IsApproved = true;
                    stockAdjustmentManager.Post(stockAdjustmentForm);
                }
            }
            return Json(stockAdjustmentForm, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Helper Method

        public ActionResult Search(string filter)
        {
            List<StockAdjustmentForm> stockAdjustmentFormlist = new List<StockAdjustmentForm>();
            StockAdjustmentManager stockAdjustmentManager = new StockAdjustmentManager();
            stockAdjustmentFormlist = stockAdjustmentManager.Get();
            stockAdjustmentFormlist = stockAdjustmentFormlist.Where(s => s.StockNo.ToString().Contains(filter.ToLower().Trim())).ToList();
            StockAdjustmentModel model = new StockAdjustmentModel();
            return PartialView("~/Views/Stock/StockAdjustment/Partial/StockAdjustmentGrid.cshtml", model);
        }
        #endregion

    }
}
