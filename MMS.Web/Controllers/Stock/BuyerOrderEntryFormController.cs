using Excel;
using Microsoft.Ajax.Utilities;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using MMS.Web.Models.SupplierMasterModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class BuyerOrderEntryFormController : Controller
    {
        #region Helper Method

        public ActionResult BuyerOrderEntryFormView(int? page)
        {
            List<OrderEntry> orderEntryEntityModellist = new List<OrderEntry>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            orderEntryEntityModellist = buyerOrderEntryManager.GetBuyerOrderGrid("");
            var pager = new Pager(orderEntryEntityModellist.Count(), page);
            var viewModel = new OrderEntryModel
            {
                OrderEntryList = orderEntryEntityModellist.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };
            return View("~/Views/Stock/BuyerOrderEntryForm/Partial/BuyerOrderEntryFormGrid.cshtml", viewModel);
        }
        public ActionResult OrderFeteching()
        {
            return PartialView("~/Views/CommonPartial/OrderFetching.cshtml");
        }

        [HttpGet]
        public ActionResult BuyerOrderEntryFormGrid(int? page)
        {
            List<OrderEntry> orderEntryEntityModellist = new List<OrderEntry>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            orderEntryEntityModellist = buyerOrderEntryManager.GetBuyerOrderGrid("");
            var pager = new Pager(orderEntryEntityModellist.Count(), page);
            var viewModel = new OrderEntryModel
            {
                OrderEntryList = orderEntryEntityModellist.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };
            return PartialView("~/Views/Stock/BuyerOrderEntryForm/Partial/BuyerOrderEntryFormGrid.cshtml", viewModel);
        }
        [HttpPost]
        public JsonResult doesOrderExist(string BuyerOrderSlNo)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            var user = buyerOrderEntryManager.GetBuyerOderPoNo(BuyerOrderSlNo);

            return Json(user == null);
        }
        public ActionResult FindWeekNo()
        {
            DateTime date = DateTime.Now;
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum_ = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            string autoId = GetAutoid();


            return Json(new { weekNum = weekNum_ }, JsonRequestBehavior.AllowGet);

        }
        public string GetAutoid()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoBuyerOrderID();
            ID++;
            return ID.ToString();
        }

        public ActionResult EditBuyerOrderEntryForm(int OrderEntryId)
        {
            OrderEntryModel model = new OrderEntryModel();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
            MultipleScheduleDetailsManager multipleScheduleDetailsManager = new MultipleScheduleDetailsManager();
            CartonDetailsManager cartonDetailsManager = new CartonDetailsManager();

            OrderEntry arg = new OrderEntry();
            OeOtherDetails arg1 = new OeOtherDetails();
            List<OePackingDetails> arg2 = new List<OePackingDetails>();
            OeShipmentDetails arg3 = new OeShipmentDetails();
            List<SizeRangeQtyRate> arg4 = new List<SizeRangeQtyRate>();
            List<MultipleScheduleDetails> arg5 = new List<MultipleScheduleDetails>();
            CartonDetails arg6 = new CartonDetails();

            if (OrderEntryId == 0)
            {
                model.OrderEntryList = null;
            }
            if (OrderEntryId != 0)
            {
                arg = buyerOrderEntryManager.GetOrderEntryId(OrderEntryId);
                arg1 = buyerOrderEntryManager.GetOeOtherDetails(OrderEntryId);
                arg2 = buyerOrderEntryManager.GetOePackingDetails(OrderEntryId);
                arg3 = buyerOrderEntryManager.GetOeShipmentDetails(OrderEntryId);
                arg4 = sizeRangeQtyRateManager.GetSizeRangeByOrderEntryId(OrderEntryId);
                arg5 = multipleScheduleDetailsManager.GetMultipleScheduleDetailsByOrderEntryId(OrderEntryId);
                arg6 = cartonDetailsManager.GetCartonDetailsByOrderEntryId(OrderEntryId);

                if (arg != null)
                {
                    model.OrderEntryId = arg.OrderEntryId;
                    model.BuyerOrderSlNo = arg.BuyerOrderSlNo;
                    model.LotNo = arg.LotNo;
                    model.Count = arg.Count;
                    model.WeekNo = arg.WeekNo;
                    model.Date = Convert.ToDateTime(arg.Date).Date.ToString("dd/MM/yyyy");
                    model.BuyerSeason = arg.BuyerSeason;
                    model.BuyerName = arg.BuyerName;
                    model.OrderProjectionNo = arg.OrderProjectionNo;
                    model.BuyerPoNo = arg.BuyerPoNo;
                    model.OurStyle = arg.OurStyle;
                    model.LeatherDescription = arg.LeatherDescription;
                    model.DiscountPer = arg.DiscountPer;
                    model.QuoteNo = arg.QuoteNo;
                    model.CountryStamp = arg.CountryStamp;
                    model.CustomerPlan = arg.CustomerPlan;
                    model.CustomerDate = Convert.ToDateTime(arg.CustomerDate).Date.ToString("dd/MM/yyyy");
                    model.AgentMasterId = arg.AgentMasterId;
                    model.CommPer = arg.CommPer;
                    model.ExFactoryDate = Convert.ToDateTime(arg.ExFactoryDate).Date.ToString("dd/MM/yyyy");
                    model.ShipmentMode = arg.ShipmentMode;
                    model.SampleReqNo = arg.SampleReqNo;
                    model.Brand = arg.Brand;
                    model.BuyerStyleNo = arg.BuyerStyleNo;
                    model.BarCodeNo = arg.BarCodeNo;
                    model.BomNo = arg.BomNo;
                    model.Last = arg.Last;
                    model.ColorMasterId = arg.ColorMasterId;
                    model.FinishedProdType = arg.FinishedProdType;
                    model.ProductTypeId = arg.ProductTypeId;
                    model.AmendmentNoWithDate = arg.AmendmentNoWithDate;
                    model.TotalOrderForWeek = arg.TotalOrderForWeek;
                    model.OrderType = arg.OrderType;
                    model.Currency = arg.Currency;
                    model.Rs = arg.Rs;
                    model.Parties = arg.Parties;
                    model.PartiesAmount1 = arg.PartiesAmount1;
                    model.ShortUnitID = arg.ShortUnitID;
                    model.PartiesAmount2 = arg.PartiesAmount2;
                    model.LongUnitID = arg.LongUnitID;
                    model.GradeMasterId = arg.GradeMasterId;
                    model.SizeRangeMasterId = arg.SizeRangeMasterId;
                    model.Remarks1 = arg.Remarks1;
                    model.Remarks2 = arg.Remarks2;
                    model.IsBuyer = arg.IsBuyer;
                    model.IsInternal = arg.IsInternal;
                    model.ProductTypeId = arg.ProductTypeId;
                    model.TotalAmount = arg.TotalAmount;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = arg.UpdatedDate;
                    model.CreatedBy = arg.CreatedBy;
                    model.UpdatedBy = arg.UpdatedBy;

                    model.SEASON = arg.SEASON;
                    model.Purchase = arg.Purchase;
                }

                if (arg1 != null)
                {
                    model.PaymentTerms = arg1.PaymentTerms;
                    model.DeliveryTerms = arg1.DeliveryTerms;
                    model.Insurance = arg1.Insurance;
                    model.DeliveryTo = arg1.DeliverTo;
                    model.SpecialInstruction = arg1.SpecialInstructions;
                    model.PackingLabeling = arg1.PackingOrLabelling;
                }

                if (arg2 != null)
                {
                    model.PackingDetailsList = arg2;
                }

                if (arg3 != null)
                {
                    model.ShipmentCountryStamp = arg3.CountryStamp;
                    model.ShipmentFrom = arg3.ShipmentFrom;
                    model.ShipmentTo = arg3.ShipmentTo;
                    model.OtherInstruction = arg3.OtherInstruction;
                }

                if (arg4 != null)
                {
                    model.SizeRangeQtyRateList = arg4;
                }

                if (arg5 != null)
                {
                    model.MultipleScheduleDetailsList = arg5;
                }

                if (arg6 != null)
                {
                    model.packType = arg6.PackType;
                    model.noOfCarton = arg6.NoOfCarton;
                }

                model.Edit = true;
            }
            return PartialView("~/Views/Stock/BuyerOrderEntryForm/Partial/BuyerOrderEntryFormDetails.cshtml", model);
        }
        public ActionResult isExistBuyerOrderSlNO(string BuyerOrderSlNo, string BuyerSeason, string BuyerName)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            OrderEntry arg_ = new OrderEntry();
            arg_ = buyerOrderEntryManager.BuyerOrderExitNo(BuyerOrderSlNo, BuyerSeason, BuyerName);
            string Message = "";
            if (arg_ != null)
            {
                Message = "Already Existed";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BuyerOrderSlNOChange(string BuyerOrderSlNo, string BuyerSeason, string BuyerName, string LotNo)
        {
            OrderEntryModel model = new OrderEntryModel();

            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            OrderEntry arg_ = new OrderEntry();
            OrderEntry buyerOrderEntryForm = new OrderEntry();
            arg_ = buyerOrderEntryManager.InternalOrderExitNo(BuyerOrderSlNo, BuyerSeason, BuyerName);
            //buyerOrderEntryForm = buyerOrderEntryManager.GetBuyerOrderSlNo(BuyerOrderSlNo);
            buyerOrderEntryForm = buyerOrderEntryManager.GetBuyerOrderSlNoWihLot(BuyerOrderSlNo, LotNo);
            Bom billOfMaterial = new Bom();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            if (buyerOrderEntryForm != null)
            {
                billOfMaterial = billOfMaterialManager.getLinkBomNumber(buyerOrderEntryForm.OurStyle);
            }
            string Message = "";
            if (arg_ != null && billOfMaterial != null)
            {
                Message = "Already Existed";
                ViewBag.ExistsRecord = "Record Already Exists";
                model.BOMErrorMessage = "Record Already Exists";
                model.Date = Convert.ToDateTime(DateTime.Now).Date.ToString();
                model.CustomerDate = Convert.ToDateTime(DateTime.Now).Date.ToString();
                model.ExFactoryDate = Convert.ToDateTime(DateTime.Now).Date.ToString();
                model.FinishedProdType = "Full Shoes";
                DateTime date = DateTime.Now;
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum_ = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                model.WeekNo = weekNum_.ToString();
                //return PartialView("~/Views/Stock/InternalOrderForm/Partial/InternalOrderFormDetails.cshtml", model);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else if (arg_ == null && (billOfMaterial == null || billOfMaterial.BomId == 0))
            {
                Message = "BOM not completed for this order";
                ViewBag.BOMError = Message + " " + BuyerOrderSlNo;
                model.Date = Convert.ToDateTime(DateTime.Now).Date.ToString();
                model.CustomerDate = Convert.ToDateTime(DateTime.Now).Date.ToString();
                model.ExFactoryDate = Convert.ToDateTime(DateTime.Now).Date.ToString();
                model.FinishedProdType = "Full Shoes";
                DateTime date = DateTime.Now;
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum_ = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                model.WeekNo = weekNum_.ToString();
                model.BOMErrorMessage = Message + " " + BuyerOrderSlNo;
                // return PartialView("~/Views/Stock/InternalOrderForm/Partial/InternalOrderFormDetails.cshtml", model);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                MultipleScheduleDetailsManager multipleScheduleDetailsManager = new MultipleScheduleDetailsManager();
                CartonDetailsManager cartonDetailsManager = new CartonDetailsManager();
                OrderEntry arg = new OrderEntry();
                OeOtherDetails arg1 = new OeOtherDetails();
                List<OePackingDetails> arg2 = new List<OePackingDetails>();
                OeShipmentDetails arg3 = new OeShipmentDetails();
                List<SizeRangeQtyRate> arg4 = new List<SizeRangeQtyRate>();
                List<MultipleScheduleDetails> arg5 = new List<MultipleScheduleDetails>();
                CartonDetails arg6 = new CartonDetails();
                arg_ = buyerOrderEntryManager.GetBuyerOrderDetails(BuyerOrderSlNo, Convert.ToInt32(BuyerSeason), Convert.ToInt32(BuyerName));

                if (arg_ == null)
                {
                    model.OrderEntryList = null;
                }
                if (arg_ != null && arg_.OrderEntryId != 0)
                {
                    arg = buyerOrderEntryManager.GetOrderEntryId(arg_.OrderEntryId);
                    arg1 = buyerOrderEntryManager.GetOeOtherDetails(arg_.OrderEntryId);
                    arg2 = buyerOrderEntryManager.GetOePackingDetails(arg_.OrderEntryId);
                    arg3 = buyerOrderEntryManager.GetOeShipmentDetails(arg_.OrderEntryId);
                    arg4 = sizeRangeQtyRateManager.GetSizeRangeByOrderEntryId(arg_.OrderEntryId);
                    arg5 = multipleScheduleDetailsManager.GetMultipleScheduleDetailsByOrderEntryId(arg_.OrderEntryId);
                    arg6 = cartonDetailsManager.GetCartonDetailsByOrderEntryId(arg_.OrderEntryId);

                    if (arg != null)
                    {
                        model.OrderEntryId = 0;
                        model.BuyerOrderSlNo = arg.BuyerOrderSlNo;
                        model.LotNo = arg.LotNo;
                        model.Count = arg.Count;
                        model.WeekNo = arg.WeekNo;
                        model.Date = Convert.ToDateTime(arg.Date).Date.ToString("dd/MM/yyyy");
                        model.BuyerSeason = arg.BuyerSeason;
                        model.BuyerName = arg.BuyerName;
                        model.OrderProjectionNo = arg.OrderProjectionNo;
                        model.BuyerPoNo = arg.BuyerPoNo;
                        model.OurStyle = arg.OurStyle;

                        model.LeatherDescription = arg.LeatherDescription;
                        model.DiscountPer = arg.DiscountPer;
                        model.QuoteNo = arg.QuoteNo;
                        model.CountryStamp = arg.CountryStamp;
                        model.CustomerPlan = arg.CustomerPlan;
                        model.CustomerDate = Convert.ToDateTime(arg.CustomerDate).Date.ToString("dd/MM/yyyy");
                        model.AgentMasterId = arg.AgentMasterId;
                        model.CommPer = arg.CommPer;
                        model.ExFactoryDate = Convert.ToDateTime(arg.ExFactoryDate).Date.ToString("dd/MM/yyyy");
                        model.ShipmentMode = arg.ShipmentMode;
                        model.SampleReqNo = arg.SampleReqNo;
                        model.Brand = arg.Brand;
                        if (!string.IsNullOrEmpty(arg.BuyerStyleNo))
                        {
                            model.BuyerStyleNo = arg.BuyerStyleNo;
                        }
                        else
                        {
                            string host = HttpContext.Request.Url.Host;
                            billOfMaterial = billOfMaterialManager.getLinkBomNumber(arg.OurStyle);
                            if (billOfMaterial != null && billOfMaterial.BomNo != null)
                            {
                                model.BuyerStyleNo = billOfMaterial.BomNo;
                            }
                        }
                        model.BarCodeNo = arg.BarCodeNo;
                        model.BomNo = arg.BomNo;
                        model.Last = arg.Last;
                        model.ColorMasterId = arg.ColorMasterId;
                        model.FinishedProdType = arg.FinishedProdType;
                        model.ProductTypeId = arg.ProductTypeId;
                        model.AmendmentNoWithDate = arg.AmendmentNoWithDate;
                        model.TotalOrderForWeek = arg.TotalOrderForWeek;
                        model.OrderType = arg.OrderType;
                        model.Currency = arg.Currency;
                        model.Rs = arg.Rs;
                        model.Parties = arg.Parties;
                        model.PartiesAmount1 = arg.PartiesAmount1;
                        model.ShortUnitID = arg.ShortUnitID;
                        model.PartiesAmount2 = arg.PartiesAmount2;
                        model.LongUnitID = arg.LongUnitID;
                        model.GradeMasterId = arg.GradeMasterId;
                        model.SizeRangeMasterId = arg.SizeRangeMasterId;
                        model.Remarks1 = arg.Remarks1;
                        model.Remarks2 = arg.Remarks2;
                        model.IsBuyer = arg.IsBuyer;
                        model.IsInternal = arg.IsInternal;
                        model.ProductTypeId = arg.ProductTypeId;
                        model.TotalAmount = arg.TotalAmount;
                        model.CreatedDate = arg.CreatedDate;
                        model.UpdatedDate = arg.UpdatedDate;
                        model.CreatedBy = arg.CreatedBy;
                        model.UpdatedBy = arg.UpdatedBy;
                    }

                    if (arg1 != null)
                    {
                        model.PaymentTerms = arg1.PaymentTerms;
                        model.DeliveryTerms = arg1.DeliveryTerms;
                        model.Insurance = arg1.Insurance;
                        model.DeliveryTo = arg1.DeliverTo;
                        model.SpecialInstruction = arg1.SpecialInstructions;
                        model.PackingLabeling = arg1.PackingOrLabelling;
                    }

                    if (arg2 != null)
                    {

                        model.PackingDetailsList = arg2;
                    }

                    if (arg3 != null)
                    {
                        model.ShipmentCountryStamp = arg3.CountryStamp;
                        model.ShipmentFrom = arg3.ShipmentFrom;
                        model.ShipmentTo = arg3.ShipmentTo;
                        model.OtherInstruction = arg3.OtherInstruction;
                    }

                    if (arg4 != null)
                    {
                        model.SizeRangeQtyRateList = arg4;
                    }

                    if (arg5 != null)
                    {
                        model.MultipleScheduleDetailsList = arg5;
                    }

                    if (arg6 != null)
                    {
                        model.packType = arg6.PackType;
                        model.noOfCarton = arg6.NoOfCarton;
                    }

                    model.Edit = true;
                }
                return PartialView("~/Views/Stock/InternalOrderForm/Partial/InternalOrderFormDetails.cshtml", model);
            }

        }

        public ActionResult InternalLotChange(string LotNo, string Season)
        {
            OrderEntryModel model = new OrderEntryModel();

            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            OrderEntry arg_ = new OrderEntry();
            List<OrderEntry> buyerOrderList = new List<OrderEntry>();
            List<OrderEntry> internalOrderList = new List<OrderEntry>();
            buyerOrderList = buyerOrderEntryManager.GetLotNumberWithSeason(LotNo, Season);
            internalOrderList = buyerOrderEntryManager.LotNumberWithOrder(LotNo, Season).Where(x => x.IsInternal == true).ToList();
            string Message = "";
            if (internalOrderList != null && internalOrderList.Count > 0)
            {
                Message = "Lot no already existed";
                ViewBag.ExistsRecord = "Record Already Exists";
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                buyerOrderList = buyerOrderList.OrderByDescending(x => x.BuyerOrderSlNo).ToList();
                if (buyerOrderList != null && buyerOrderList.Count > 0)
                {
                    string error = "";
                    foreach (var iteration in buyerOrderList)
                    {

                        Bom billOfMaterial = new Bom();
                        BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
                        OrderEntry orderEntryForm = new OrderEntry();
                        orderEntryForm = buyerOrderEntryManager.GetBuyerOderSlNo_withSeason(iteration.BuyerOrderSlNo, iteration.LotNo, iteration.BuyerSeason);
                        if (orderEntryForm != null && orderEntryForm.BuyerOrderSlNo != "")
                        {
                            error += orderEntryForm.BuyerOrderSlNo + " " + " Already Existed! Please delete and try again";
                        }
                        billOfMaterial = billOfMaterialManager.getLinkBomNumber(iteration.OurStyle);
                        if (billOfMaterial == null || billOfMaterial.BomId == 0)
                        {
                            error += "BOM is not created this order no" + " " + iteration.BuyerOrderSlNo + ",";
                        }

                    }
                    if (string.IsNullOrEmpty(error))
                    {
                        try
                        {
                            foreach (var item in buyerOrderList)
                            {
                                int orderEntryId = 0;
                                SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                                MultipleScheduleDetailsManager multipleScheduleDetailsManager = new MultipleScheduleDetailsManager();
                                CartonDetailsManager cartonDetailsManager = new CartonDetailsManager();
                                List<OrderEntry> iExistBuyerEntry = new List<OrderEntry>();
                                OrderEntry BuyerEntry = new OrderEntry();
                                OeShipmentDetails oeShipmentDetails = new OeShipmentDetails();
                                OeOtherDetails oeOtherDetails = new OeOtherDetails();
                                CartonDetails cartonDetails = new CartonDetails();
                                BuyerEntry.BuyerOrderSlNo = item.BuyerOrderSlNo;
                                BuyerEntry.LotNo = item.LotNo;
                                BuyerEntry.Count = item.Count;
                                BuyerEntry.WeekNo = item.WeekNo;
                                var format = "dd/MM/yyyy";
                                BuyerEntry.Date = item.Date;
                                BuyerEntry.IsInternal = true;
                                BuyerEntry.IsBuyer = false;
                                BuyerEntry.BuyerSeason = item.BuyerSeason;
                                BuyerEntry.BuyerName = item.BuyerName;
                                BuyerEntry.OrderProjectionNo = item.OrderProjectionNo;
                                BuyerEntry.BuyerPoNo = item.BuyerPoNo;
                                BuyerEntry.OurStyle = item.OurStyle;
                                BuyerEntry.LeatherDescription = item.LeatherDescription;
                                BuyerEntry.DiscountPer = item.DiscountPer;
                                BuyerEntry.QuoteNo = item.QuoteNo;
                                BuyerEntry.CountryStamp = item.CountryStamp;
                                BuyerEntry.CustomerPlan = item.CustomerPlan;
                                BuyerEntry.CustomerDate = item.CustomerDate;
                                BuyerEntry.AgentMasterId = item.AgentMasterId;
                                BuyerEntry.CommPer = item.CommPer;
                                BuyerEntry.ExFactoryDate = item.ExFactoryDate;
                                BuyerEntry.ShipmentMode = item.ShipmentMode;
                                BuyerEntry.SampleReqNo = item.SampleReqNo;
                                BuyerEntry.OrderType = item.OrderType;
                                BuyerEntry.Brand = item.Brand;
                                BuyerEntry.BuyerStyleNo = item.BuyerStyleNo;
                                BuyerEntry.BarCodeNo = item.BarCodeNo;
                                BuyerEntry.BomNo = item.BomNo;

                                BuyerEntry.SEASON = model.SEASON;
                                BuyerEntry.Purchase = model.Purchase;
                                if (item.Last == "undefined")
                                {
                                    BuyerEntry.Last = "";
                                }
                                else
                                {
                                    BuyerEntry.Last = item.Last;
                                }

                                BuyerEntry.ColorMasterId = item.ColorMasterId;
                                BuyerEntry.FinishedProdType = item.FinishedProdType;
                                BuyerEntry.ProductTypeId = item.ProductTypeId;
                                BuyerEntry.AmendmentNoWithDate = item.AmendmentNoWithDate;
                                BuyerEntry.TotalOrderForWeek = item.TotalOrderForWeek;
                                BuyerEntry.Currency = item.Currency;
                                BuyerEntry.Rs = item.Rs;
                                BuyerEntry.Parties = item.Parties;
                                BuyerEntry.GradeMasterId = item.GradeMasterId;
                                BuyerEntry.SizeRangeMasterId = item.SizeRangeMasterId;
                                BuyerEntry.Remarks1 = item.Remarks1;
                                BuyerEntry.Remarks2 = item.Remarks2;
                                BuyerEntry.IsBuyer = item.IsBuyer;
                                BuyerEntry.IsInternal = true;
                                BuyerEntry.CreatedDate = DateTime.Now;
                                BuyerEntry.PartiesAmount1 = item.PartiesAmount1;
                                BuyerEntry.ShortUnitID = item.ShortUnitID;
                                BuyerEntry.PartiesAmount2 = item.PartiesAmount2;
                                BuyerEntry.LongUnitID = item.LongUnitID;
                                BuyerEntry.TotalAmount = item.TotalAmount;

                                BuyerEntry.SEASON = model.SEASON;
                                BuyerEntry.Purchase = model.Purchase;
                                orderEntryId = buyerOrderEntryManager.Post(BuyerEntry);

                                //SizeRange Save                        
                                SizeRangeQtyRateManager sizeItemManager = new SizeRangeQtyRateManager();
                                List<SizeRangeQtyRate> SizeQuantityRateList = new List<SizeRangeQtyRate>();
                                SizeQuantityRateList = sizeItemManager.GetSizeRangeByOrderEntryId(item.OrderEntryId);
                                foreach (var SizeQuantityRateitem in SizeQuantityRateList)
                                {
                                    SizeRangeQtyRate SizeRangeQtyRateDetails = new SizeRangeQtyRate();
                                    SizeRangeQtyRateDetails.SizeRange = SizeQuantityRateitem.SizeRange;
                                    SizeRangeQtyRateDetails.Qty = Convert.ToDecimal(SizeQuantityRateitem.Qty);
                                    SizeRangeQtyRateDetails.Rate = Convert.ToDecimal(SizeQuantityRateitem.Rate);
                                    SizeRangeQtyRateDetails.OrderEntryId = orderEntryId;
                                    SizeRangeQtyRateDetails.CreatedDate = DateTime.Now;
                                    sizeRangeQtyRateManager.Post(SizeRangeQtyRateDetails);
                                }

                                //Multiple Schedule Size
                                MultipleScheduleDetailsManager multipleDetailsManager = new MultipleScheduleDetailsManager();
                                List<MultipleScheduleDetails> multipleScheduleDetails = new List<MultipleScheduleDetails>();
                                multipleScheduleDetails = multipleScheduleDetailsManager.GetMultipleScheduleDetailsByOrderEntryId(item.OrderEntryId);
                                foreach (var items in multipleScheduleDetails)
                                {
                                    MultipleScheduleDetails schedule = new MultipleScheduleDetails();
                                    schedule.Io = items.Io;
                                    schedule.Size = items.Size;
                                    schedule.Qty = items.Qty;
                                    schedule.ExFDt = items.ExFDt;
                                    schedule.OrderEntryId = orderEntryId;
                                    schedule.CreatedDate = DateTime.Now;
                                    multipleScheduleDetailsManager.Post(schedule);
                                }
                                //Single PackType & noOfCarton Save
                                if (model.packType != null && model.noOfCarton != null)
                                {
                                    cartonDetails.PackType = model.packType;
                                    cartonDetails.NoOfCarton = model.noOfCarton;
                                    cartonDetails.OrderEntryId = orderEntryId;
                                    cartonDetails.CreatedDate = DateTime.Now;
                                    // cartonDetails.UpdatedDate = DateTime.Now;
                                    cartonDetailsManager.Post(cartonDetails);
                                }

                                //oePacking Details save

                                InternalOrderFormManager internalOrderFormManager = new InternalOrderFormManager();
                                List<OePackingDetails> oePackingDetailsList = new List<OePackingDetails>();
                                oePackingDetailsList = internalOrderFormManager.GetOePackingDetails(item.OrderEntryId);
                                foreach (var itemoePackingDetailsList in oePackingDetailsList)
                                {
                                    OePackingDetails oePackingDetails = new OePackingDetails();
                                    oePackingDetails.PackingType = itemoePackingDetailsList.PackingType;
                                    oePackingDetails.SizeRangeMasterId = Convert.ToInt32(itemoePackingDetailsList.SizeRangeMasterId);
                                    oePackingDetails.Size = itemoePackingDetailsList.Size;
                                    oePackingDetails.OrderEntryId = orderEntryId;
                                    buyerOrderEntryManager.Post_OePackingDetails(oePackingDetails);
                                }



                            }
                            return Json(new { Message = "Saved successfully" }, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception ex)
                        {
                            error += ex.Message.ToString();
                            return Json(new { Message = error }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { Message = error }, JsonRequestBehavior.AllowGet);
                    }
                    //return PartialView("~/Views/Stock/InternalOrderForm/Partial/InternalOrderFormDetails.cshtml", model);
                }
                else
                {
                    return Json(new { }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult Search(string filter, int? page)
        {

            List<OrderEntry> orderEntryEntityModellist = new List<OrderEntry>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            orderEntryEntityModellist = buyerOrderEntryManager.GetBuyerOrderGrid(filter);
            orderEntryEntityModellist = orderEntryEntityModellist.Where(x => x.IsBuyer == true).ToList();
            OrderEntryModel model = new OrderEntryModel();
            var pager = new Pager(orderEntryEntityModellist.Count(), page);
            var viewModel = new OrderEntryModel
            {
                OrderEntryList = orderEntryEntityModellist.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };
            return PartialView("~/Views/Stock/BuyerOrderEntryForm/Partial/BuyerOrderEntryFormGrid.cshtml", viewModel);
        }
        public ActionResult GetSizeRange(int SizeRangeMasterId)
        {
            SizeRangeMasterManager manager = new SizeRangeMasterManager();
            List<SizeRangeMaster> result = new List<SizeRangeMaster>();
            result = manager.GetSizeRangeMasterList(SizeRangeMasterId);
            if (result.Count > 0)
            {
                result = result.OrderBy(x => decimal.Parse(x.SizeRangeRefValue)).ToList();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetTotalOredrWeek(int BuyerSeasonId, int currentWeek)
        {
            List<OrderEntryEntityModel> orderEntryEntityModellist = new List<OrderEntryEntityModel>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            SizeRangeQtyRate sizeRangeQtyRate = new SizeRangeQtyRate();
            SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();

            var items = (from x in buyerOrderEntryManager.Get()
                         join y in sizeRangeQtyRateManager.Get()
                         on x.OrderEntryId equals y.OrderEntryId
                         where x.BuyerSeason == BuyerSeasonId && x.WeekNo == currentWeek.ToString()
                         select new { x.BuyerSeason, x.WeekNo, y.Qty, x.TotalAmount });
            // decimal totalOrder = items.Sum(x => x.TotalAmount.Value);
            decimal totalOrder = items.Sum(x => x.TotalAmount ?? 0);
            var TotalweekOrders = items.ToList();

            return Json(totalOrder, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBuyerModel(string BuyerModel)
        {

            Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
            BuyerModelManager buyerManager = new BuyerModelManager();
            if (BuyerModel != null && BuyerModel.Trim() != "Please Select")
            {
                var result = from x in product_BuyerStyleManager.Get()
                             where x.BomNo.Trim() == BuyerModel.Trim()
                             select x.BuyerModel_ProductType;

                int item = int.Parse(result.FirstOrDefault().ToString());

                var buyerModel = item;
                var items = from x in buyerManager.Get()
                            where x.BuyerModelID == Convert.ToInt32(buyerModel)
                            select x.BuyerModelName;
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }



        }

        public ActionResult GetBuyerNameOrderCnt(int BuyerSeasonId, int currentWeek, int buyerName)
        {
            List<OrderEntryEntityModel> orderEntryEntityModellist = new List<OrderEntryEntityModel>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            SizeRangeQtyRate sizeRangeQtyRate = new SizeRangeQtyRate();
            SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();

            var items = (from x in buyerOrderEntryManager.Get()
                         join y in sizeRangeQtyRateManager.Get()
                         on x.OrderEntryId equals y.OrderEntryId
                         where x.BuyerSeason == BuyerSeasonId && x.WeekNo == currentWeek.ToString() && x.BuyerName == buyerName
                         select new { x.BuyerSeason, x.WeekNo, y.Qty, x.TotalAmount });
            //decimal totalOrder = items.Sum(x => x.TotalAmount.Value);
            decimal totalOrder = items.Sum(x => x.TotalAmount ?? 0);
            var TotalweekOrders = items.ToList();

            return Json(totalOrder, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult BuyerOrderEntryForm(OrderEntryModel model)
        {

            string Result = "";
            int orderEntryId = 0;
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
            MultipleScheduleDetailsManager multipleScheduleDetailsManager = new MultipleScheduleDetailsManager();
            CartonDetailsManager cartonDetailsManager = new CartonDetailsManager();
            OrderEntry BuyerEntry = new OrderEntry();
            OrderEntry iExistBuyerEntry = new OrderEntry();
            OeShipmentDetails oeShipmentDetails = new OeShipmentDetails();
            OeOtherDetails oeOtherDetails = new OeOtherDetails();
            CartonDetails cartonDetails = new CartonDetails();
            iExistBuyerEntry = buyerOrderEntryManager.BuyerOrderExitNo(model.BuyerOrderSlNo, model.BuyerSeason.ToString(), model.BuyerName.ToString());

            if (model.OrderEntryId == 0 && iExistBuyerEntry == null)
            {
                BuyerEntry.OrderEntryId = model.OrderEntryId;
                BuyerEntry.BuyerOrderSlNo = model.BuyerOrderSlNo;
                BuyerEntry.LotNo = model.LotNo;
                BuyerEntry.Count = model.Count;
                BuyerEntry.WeekNo = model.WeekNo;
                if (model.ExFactoryDate == null)
                {
                    model.ExFactoryDate = model.Date;
                }
                var format = "dd/MM/yyyy";
                DateTime Date = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                DateTime CustomerDate = DateTime.ParseExact(model.CustomerDate, format, CultureInfo.InvariantCulture);
                DateTime ExFactoryDate = DateTime.ParseExact(model.ExFactoryDate, format, CultureInfo.InvariantCulture);


                BuyerEntry.Date = Date;
                BuyerEntry.BuyerSeason = model.BuyerSeason;
                BuyerEntry.BuyerName = model.BuyerName;
                BuyerEntry.OrderProjectionNo = model.OrderProjectionNo;
                BuyerEntry.BuyerPoNo = model.BuyerPoNo;
                BuyerEntry.OurStyle = model.OurStyle;
                BuyerEntry.LeatherDescription = model.LeatherDescription;
                BuyerEntry.DiscountPer = model.DiscountPer;
                BuyerEntry.QuoteNo = model.QuoteNo;
                BuyerEntry.CountryStamp = model.CountryStamp;
                BuyerEntry.CustomerPlan = model.CustomerPlan;
                BuyerEntry.CustomerDate = CustomerDate;
                BuyerEntry.AgentMasterId = model.AgentMasterId;
                BuyerEntry.CommPer = model.CommPer;
                BuyerEntry.ExFactoryDate = ExFactoryDate;
                BuyerEntry.ShipmentMode = model.ShipmentMode;
                BuyerEntry.SampleReqNo = model.SampleReqNo;
                BuyerEntry.OrderType = model.OrderType;
                BuyerEntry.Brand = model.Brand;
                BuyerEntry.BuyerStyleNo = model.BuyerStyleNo;
                BuyerEntry.BarCodeNo = model.BarCodeNo;
                BuyerEntry.BomNo = model.BomNo;
                BuyerEntry.Last = model.Last;
                BuyerEntry.ColorMasterId = model.ColorMasterId;
                BuyerEntry.FinishedProdType = model.FinishedProdType;
                BuyerEntry.ProductTypeId = model.ProductTypeId;
                BuyerEntry.AmendmentNoWithDate = model.AmendmentNoWithDate;
                BuyerEntry.TotalOrderForWeek = model.TotalOrderForWeek;
                BuyerEntry.Currency = model.Currency;
                BuyerEntry.Rs = model.Rs;
                BuyerEntry.Parties = model.Parties;
                BuyerEntry.GradeMasterId = model.GradeMasterId;
                BuyerEntry.SizeRangeMasterId = model.SizeRangeMasterId;
                BuyerEntry.Remarks1 = model.Remarks1;
                BuyerEntry.Remarks2 = model.Remarks2;
                BuyerEntry.IsBuyer = true;
                BuyerEntry.IsInternal = false;
                BuyerEntry.CreatedDate = DateTime.Now;

                BuyerEntry.PartiesAmount1 = model.PartiesAmount1;
                BuyerEntry.ShortUnitID = model.ShortUnitID;
                BuyerEntry.PartiesAmount2 = model.PartiesAmount2;
                BuyerEntry.LongUnitID = model.LongUnitID;
                BuyerEntry.TotalAmount = Convert.ToInt32(model.TotalAmount);

                BuyerEntry.SEASON = model.SEASON;
                BuyerEntry.Purchase = model.Purchase;
                orderEntryId = buyerOrderEntryManager.Post(BuyerEntry);

                //SizeRange Save
                if (model.SizeQuantityRate != null)
                {
                    var SizeQuantityRate = JsonConvert.DeserializeObject<List<SizeQuantityRateObject>>(model.SizeQuantityRate);

                    foreach (var item in SizeQuantityRate)
                    {
                        SizeRangeQtyRate SizeRangeQtyRateDetails = new SizeRangeQtyRate();
                        SizeRangeQtyRateDetails.SizeRange = item.Size;
                        SizeRangeQtyRateDetails.Qty = Convert.ToInt32(item.quantity);
                        SizeRangeQtyRateDetails.Rate = Convert.ToDecimal(item.Rate);
                        SizeRangeQtyRateDetails.OrderEntryId = orderEntryId;
                        SizeRangeQtyRateDetails.CreatedDate = DateTime.Now;
                        SizeRangeQtyRateDetails.UpdatedDate = DateTime.Now;
                        sizeRangeQtyRateManager.Post(SizeRangeQtyRateDetails);

                    }
                }

                //Multiple Schedule Size
                if (model.MultipleSchedule != null)
                {
                    var MultipleSchedule = JsonConvert.DeserializeObject<List<MultipleScheduleObject>>(model.MultipleSchedule);

                    foreach (var item in MultipleSchedule)
                    {
                        MultipleScheduleDetails schedule = new MultipleScheduleDetails();
                        schedule.Io = item.Io;
                        schedule.Size = item.Size;
                        schedule.Qty = item.quantity;
                        schedule.ExFDt = item.Date;
                        schedule.OrderEntryId = orderEntryId;
                        schedule.CreatedDate = DateTime.Now;
                        schedule.UpdatedDate = DateTime.Now;
                        multipleScheduleDetailsManager.Post(schedule);
                    }
                }

                //Single PackType & noOfCarton Save
                if (model.packType != null && model.noOfCarton != null)
                {
                    cartonDetails.PackType = model.packType;
                    cartonDetails.NoOfCarton = model.noOfCarton;
                    cartonDetails.OrderEntryId = orderEntryId;
                    cartonDetails.CreatedDate = DateTime.Now;
                    cartonDetails.UpdatedDate = DateTime.Now;
                    cartonDetailsManager.Post(cartonDetails);
                }

                //oePacking Details save
                if (model.PackingDetail != null)
                {
                    var PackingDetail = JsonConvert.DeserializeObject<List<PackingDetailObject>>(model.PackingDetail);

                    foreach (var item in PackingDetail)
                    {
                        OePackingDetails oePackingDetails = new OePackingDetails();
                        oePackingDetails.PackingType = item.PackingType;
                        oePackingDetails.SizeRangeMasterId = Convert.ToInt32(item.SizeRangeMasterId);
                        oePackingDetails.Size = item.Size;
                        oePackingDetails.OrderEntryId = orderEntryId;
                        buyerOrderEntryManager.Post_OePackingDetails(oePackingDetails);
                    }
                }

                //OeShipment Details Save
                oeShipmentDetails.CountryStamp = model.ShipmentCountryStamp;
                oeShipmentDetails.ShipmentFrom = model.ShipmentFrom;
                oeShipmentDetails.ShipmentTo = model.ShipmentTo;
                oeShipmentDetails.OtherInstruction = model.OtherInstruction;
                oeShipmentDetails.OrderEntryId = BuyerEntry.OrderEntryId;
                buyerOrderEntryManager.Post_OeShipmentDetails(oeShipmentDetails);

                //OeOther Deatils save
                oeOtherDetails.Insurance = model.Insurance;
                oeOtherDetails.OrderEntryId = BuyerEntry.OrderEntryId;
                oeOtherDetails.DeliverTo = model.DeliveryTo;
                oeOtherDetails.PackingOrLabelling = model.PackingLabeling;
                oeOtherDetails.PaymentTerms = model.PaymentTerms;
                oeOtherDetails.DeliveryTerms = model.DeliveryTerms;
                oeOtherDetails.SpecialInstructions = model.SpecialInstruction;
                buyerOrderEntryManager.Post_OeOtherDetails(oeOtherDetails);

                if (orderEntryId != 0)
                {
                    Result = "Saved";
                }
            }
            else if (model.OrderEntryId != 0 && iExistBuyerEntry != null)
            {
                BuyerEntry = buyerOrderEntryManager.GetOrderEntryId(model.OrderEntryId);
                BuyerEntry.OrderEntryId = model.OrderEntryId;
                BuyerEntry.BuyerOrderSlNo = model.BuyerOrderSlNo;
                BuyerEntry.LotNo = model.LotNo;
                BuyerEntry.Count = model.Count;
                BuyerEntry.WeekNo = model.WeekNo;

                var format = "dd/MM/yyyy";
                DateTime Date = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                DateTime CustomerDate = DateTime.ParseExact(model.CustomerDate, format, CultureInfo.InvariantCulture);
                DateTime ExFactoryDate = DateTime.ParseExact(model.ExFactoryDate, format, CultureInfo.InvariantCulture);


                BuyerEntry.Date = Date;
                BuyerEntry.BuyerSeason = model.BuyerSeason;
                BuyerEntry.BuyerName = model.BuyerName;
                BuyerEntry.OrderProjectionNo = model.OrderProjectionNo;
                BuyerEntry.BuyerPoNo = model.BuyerPoNo;
                BuyerEntry.OurStyle = model.OurStyle;
                BuyerEntry.LeatherDescription = model.LeatherDescription;
                BuyerEntry.DiscountPer = model.DiscountPer;
                BuyerEntry.QuoteNo = model.QuoteNo;
                BuyerEntry.CountryStamp = model.CountryStamp;
                BuyerEntry.CustomerPlan = model.CustomerPlan;
                BuyerEntry.CustomerDate = CustomerDate;
                BuyerEntry.AgentMasterId = model.AgentMasterId;
                BuyerEntry.CommPer = model.CommPer;
                BuyerEntry.ExFactoryDate = ExFactoryDate;
                BuyerEntry.ShipmentMode = model.ShipmentMode;
                BuyerEntry.SampleReqNo = model.SampleReqNo;
                BuyerEntry.Brand = model.Brand;
                BuyerEntry.BuyerStyleNo = model.BuyerStyleNo;
                BuyerEntry.BarCodeNo = model.BarCodeNo;
                BuyerEntry.BomNo = model.BomNo;
                BuyerEntry.Last = model.Last;
                BuyerEntry.ColorMasterId = model.ColorMasterId;
                BuyerEntry.FinishedProdType = model.FinishedProdType;
                BuyerEntry.ProductTypeId = model.ProductTypeId;
                BuyerEntry.AmendmentNoWithDate = model.AmendmentNoWithDate;
                BuyerEntry.TotalOrderForWeek = model.TotalOrderForWeek;
                BuyerEntry.OrderType = model.OrderType;
                BuyerEntry.Currency = model.Currency;
                BuyerEntry.Rs = model.Rs;
                BuyerEntry.Parties = model.Parties;
                BuyerEntry.GradeMasterId = model.GradeMasterId;
                BuyerEntry.SizeRangeMasterId = model.SizeRangeMasterId;
                BuyerEntry.Remarks1 = model.Remarks1;
                BuyerEntry.Remarks2 = model.Remarks2;
                BuyerEntry.IsBuyer = true;
                BuyerEntry.IsInternal = false;
                BuyerEntry.PartiesAmount1 = model.PartiesAmount1;
                BuyerEntry.ShortUnitID = model.ShortUnitID;
                BuyerEntry.PartiesAmount2 = model.PartiesAmount2;
                BuyerEntry.LongUnitID = model.LongUnitID;
                BuyerEntry.TotalAmount = Convert.ToInt32(model.TotalAmount);
                BuyerEntry.CreatedDate = DateTime.Now;
                BuyerEntry.SEASON = model.SEASON;
                BuyerEntry.Purchase = model.Purchase;
                orderEntryId = buyerOrderEntryManager.Post(BuyerEntry);

                //SizeRange Save
                if (model.SizeQuantityRate != null)
                {
                    var SizeQuantityRate = JsonConvert.DeserializeObject<List<SizeQuantityRateObject>>(model.SizeQuantityRate);

                    List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
                    SizeRangeQtyRateManager sizeRangeQtyRateMgr = new SizeRangeQtyRateManager();

                    var SizeQuantityRt = sizeRangeQtyRateMgr.GetSizeRangeByOrderEntryId(orderEntryId).Where(x => x.SizeQRId == orderEntryId).Select(x => x.SizeQRId).ToList();
                    model.SizeQuantityRate = SizeQuantityRt.ToString();

                    sizeRangeQtyRateManager.Delete(orderEntryId);

                    foreach (var item in SizeQuantityRate)
                    {
                        SizeRangeQtyRate SizeRangeQtyRateDetails = new SizeRangeQtyRate();
                        SizeRangeQtyRateDetails.SizeRange = item.Size;
                        if (item.quantity == "0.00")
                        {
                            item.quantity = "0";
                        }
                        SizeRangeQtyRateDetails.Qty = Convert.ToDecimal(item.quantity);
                        SizeRangeQtyRateDetails.Rate = Convert.ToDecimal(item.Rate);
                        SizeRangeQtyRateDetails.OrderEntryId = orderEntryId;
                        SizeRangeQtyRateDetails.CreatedDate = DateTime.Now;
                        SizeRangeQtyRateDetails.UpdatedDate = DateTime.Now;
                        sizeRangeQtyRateManager.Post(SizeRangeQtyRateDetails);
                    }
                }

                //Multiple Schedule Size
                if (model.MultipleSchedule != null)
                {
                    var MultipleSchedule = JsonConvert.DeserializeObject<List<MultipleScheduleObject>>(model.MultipleSchedule);

                    multipleScheduleDetailsManager.Delete(orderEntryId);

                    foreach (var item in MultipleSchedule)
                    {
                        MultipleScheduleDetails schedule = new MultipleScheduleDetails();
                        schedule.Io = item.Io;
                        schedule.Size = item.Size;
                        schedule.Qty = item.quantity;
                        schedule.ExFDt = item.Date;
                        schedule.OrderEntryId = orderEntryId;
                        schedule.CreatedDate = DateTime.Now;
                        schedule.UpdatedDate = DateTime.Now;
                        multipleScheduleDetailsManager.Post(schedule);
                    }
                }

                //Single PackType & noOfCarton Save
                if (model.packType != null && model.noOfCarton != null)
                {
                    cartonDetails = cartonDetailsManager.GetCartonDetailsByOrderEntryId(orderEntryId);
                    if (cartonDetails != null)
                    {
                        cartonDetails.PackType = model.packType;
                        cartonDetails.NoOfCarton = model.noOfCarton;
                        cartonDetails.OrderEntryId = orderEntryId;
                        cartonDetails.CreatedDate = DateTime.Now;
                        cartonDetails.UpdatedDate = DateTime.Now;
                        cartonDetailsManager.Post(cartonDetails);
                    }
                    else
                    {
                        CartonDetails cartonDetails1 = new CartonDetails();
                        cartonDetails1.PackType = model.packType;
                        cartonDetails1.NoOfCarton = model.noOfCarton;
                        cartonDetails1.OrderEntryId = orderEntryId;
                        cartonDetails1.CreatedDate = DateTime.Now;
                        cartonDetails1.UpdatedDate = DateTime.Now;
                        cartonDetailsManager.Post(cartonDetails1);
                    }
                }

                //oePacking Details save
                if (model.PackingDetail != null)
                {
                    var PackingDetail = JsonConvert.DeserializeObject<List<PackingDetailObject>>(model.PackingDetail);

                    buyerOrderEntryManager.Delete_OePackingDetails(orderEntryId);

                    foreach (var item in PackingDetail)
                    {
                        OePackingDetails oePackingDetails = new OePackingDetails();
                        oePackingDetails.PackingType = item.PackingType;
                        oePackingDetails.SizeRangeMasterId = Convert.ToInt32(item.SizeRangeMasterId);
                        oePackingDetails.Size = item.Size;
                        oePackingDetails.OrderEntryId = orderEntryId;
                        buyerOrderEntryManager.Post_OePackingDetails(oePackingDetails);
                    }
                }

                //OeShipment Details Save
                oeShipmentDetails = buyerOrderEntryManager.GetOeShipmentDetails(model.OrderEntryId);
                if (oeShipmentDetails != null)
                {
                    oeShipmentDetails.CountryStamp = model.ShipmentCountryStamp;
                    oeShipmentDetails.ShipmentFrom = model.ShipmentFrom;
                    oeShipmentDetails.ShipmentTo = model.ShipmentTo;
                    oeShipmentDetails.OtherInstruction = model.OtherInstruction;
                    oeShipmentDetails.OrderEntryId = BuyerEntry.OrderEntryId;
                    buyerOrderEntryManager.Post_OeShipmentDetails(oeShipmentDetails);
                }
                else
                {
                    OeShipmentDetails oeShipmentDetails1 = new OeShipmentDetails();
                    oeShipmentDetails1.CountryStamp = model.ShipmentCountryStamp;
                    oeShipmentDetails1.ShipmentFrom = model.ShipmentFrom;
                    oeShipmentDetails1.ShipmentTo = model.ShipmentTo;
                    oeShipmentDetails1.OtherInstruction = model.OtherInstruction;
                    oeShipmentDetails1.OrderEntryId = BuyerEntry.OrderEntryId;
                    buyerOrderEntryManager.Post_OeShipmentDetails(oeShipmentDetails1);
                }

                //OeOther Deatils save
                oeOtherDetails = buyerOrderEntryManager.GetOeOtherDetails(model.OrderEntryId);
                if (oeOtherDetails != null)
                {
                    oeOtherDetails.Insurance = model.Insurance;
                    oeOtherDetails.OrderEntryId = BuyerEntry.OrderEntryId;
                    oeOtherDetails.DeliverTo = model.DeliveryTo;
                    oeOtherDetails.PackingOrLabelling = model.PackingLabeling;
                    oeOtherDetails.PaymentTerms = model.PaymentTerms;
                    oeOtherDetails.DeliveryTerms = model.DeliveryTerms;
                    oeOtherDetails.SpecialInstructions = model.SpecialInstruction;
                    buyerOrderEntryManager.Post_OeOtherDetails(oeOtherDetails);
                }
                else
                {
                    OeOtherDetails oeOtherDetails1 = new OeOtherDetails();
                    oeOtherDetails1.Insurance = model.Insurance;
                    oeOtherDetails1.OrderEntryId = BuyerEntry.OrderEntryId;
                    oeOtherDetails1.DeliverTo = model.DeliveryTo;
                    oeOtherDetails1.PackingOrLabelling = model.PackingLabeling;
                    oeOtherDetails1.PaymentTerms = model.PaymentTerms;
                    oeOtherDetails1.DeliveryTerms = model.DeliveryTerms;
                    oeOtherDetails1.SpecialInstructions = model.SpecialInstruction;
                    buyerOrderEntryManager.Post_OeOtherDetails(oeOtherDetails1);
                }

                if (orderEntryId != 0)
                {
                    Result = "Updated";
                }
            }
            else if (model.OrderEntryId == 0 && iExistBuyerEntry != null)
            {
                Result = "Already Exist";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int OrderEntryId)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            string status = "";
            OrderEntry orderEntryEntityModel = new OrderEntry();
            List<OePackingDetails> oePackingDetails = new List<OePackingDetails>();
            OeShipmentDetails oeShipmentDetails = new OeShipmentDetails();
            OeOtherDetails oeOtherDetails = new OeOtherDetails();

            oePackingDetails = buyerOrderEntryManager.GetOePackingDetails(OrderEntryId);
            if (oePackingDetails.Count > 0)
            {
                buyerOrderEntryManager.Delete_OePackingDetails(OrderEntryId);
            }
            oeShipmentDetails = buyerOrderEntryManager.GetOeShipmentDetails(OrderEntryId);
            if (oeShipmentDetails != null)
            {
                buyerOrderEntryManager.Delete_OeShipmentDetails(OrderEntryId);
            }
            oeOtherDetails = buyerOrderEntryManager.GetOeOtherDetails(OrderEntryId);
            if (oeOtherDetails != null)
            {
                buyerOrderEntryManager.Delete_OeOtherDetails(OrderEntryId);
            }
            orderEntryEntityModel = buyerOrderEntryManager.GetOrderEntryId(OrderEntryId);
            if (orderEntryEntityModel != null)
            {
                status = "Success";
                buyerOrderEntryManager.Delete(orderEntryEntityModel.OrderEntryId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Excel Upload
        public ActionResult RemoveSession()
        {
            @Session["BOMerror_"] = null;
            return View("~/Views/Stock/BuyerOrderEntryForm/BuyerOrderEntryForm.cshtml");
        }
        [HttpPost]

        public ActionResult Upload(HttpPostedFileBase upload)
        {

            string Url = Request.Url.AbsoluteUri;
            SupplierMasterModel model = new SupplierMasterModel();
            if (ModelState.IsValid)
            {
                DataSet result = null;
                if (upload != null && upload.ContentLength > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    Stream stream = upload.InputStream;

                    // We return the interface, so that
                    IExcelDataReader reader = null;
                    if (upload.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        TempData["successBody"] = "This file format is not supported";
                        return RedirectToAction("BuyerOrderEntryForm");
                    }
                    reader.IsFirstRowAsColumnNames = true;
                    result = reader.AsDataSet();
                    reader.Close();
                }
                else
                {
                    TempData["successBody"] = "Please Upload Your file";
                    return RedirectToAction("BuyerOrderEntryForm");
                }
                int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoOrderEntryID();
                string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
                var table = new DataTable();
                table = result.Tables[0];
                ID = ID++;
                DataTable table_ = new DataTable();
                table_.Columns.Add("OrderEntryId", typeof(int));
                table_.Columns.Add("BuyerOrderSlNo", typeof(string));
                table_.Columns.Add("LotNo", typeof(string));
                table_.Columns.Add("Count", typeof(string));
                table_.Columns.Add("WeekNo", typeof(string));
                table_.Columns.Add("Date", typeof(DateTime));
                table_.Columns.Add("BuyerSeason", typeof(int));
                table_.Columns.Add("BuyerName", typeof(int));
                table_.Columns.Add("OrderProjectionNo", typeof(string));
                table_.Columns.Add("BuyerPoNo", typeof(string));
                table_.Columns.Add("OurStyle", typeof(string));
                table_.Columns.Add("LeatherDescription", typeof(string));
                table_.Columns.Add("DiscountPer", typeof(decimal));
                table_.Columns.Add("QuoteNo", typeof(string));
                table_.Columns.Add("CountryStamp", typeof(string));
                table_.Columns.Add("CustomerPlan", typeof(string));
                table_.Columns.Add("CustomerDate", typeof(DateTime));
                table_.Columns.Add("AgentMasterId", typeof(int));
                table_.Columns.Add("CommPer", typeof(string));
                table_.Columns.Add("ExFactoryDate", typeof(DateTime));
                table_.Columns.Add("ShipmentMode", typeof(string));
                table_.Columns.Add("SampleReqNo", typeof(string));
                table_.Columns.Add("Brand", typeof(string));
                table_.Columns.Add("BuyerStyleNo", typeof(string));
                table_.Columns.Add("BarCodeNo", typeof(string));
                table_.Columns.Add("BomNo", typeof(string));
                table_.Columns.Add("Last", typeof(string));
                table_.Columns.Add("ColorMasterId", typeof(string));
                table_.Columns.Add("FinishedProdType", typeof(string));
                table_.Columns.Add("ProductTypeId", typeof(int));
                table_.Columns.Add("AmendmentNoWithDate", typeof(string));
                table_.Columns.Add("TotalOrderForWeek", typeof(string));
                table_.Columns.Add("OrderType", typeof(int));
                table_.Columns.Add("Currency", typeof(int));
                table_.Columns.Add("Rs", typeof(int));
                table_.Columns.Add("Parties", typeof(int));
                table_.Columns.Add("GradeMasterId", typeof(int));
                table_.Columns.Add("SizeRangeMasterId", typeof(int));
                table_.Columns.Add("Remarks1", typeof(string));
                table_.Columns.Add("Remarks2", typeof(string));
                table_.Columns.Add("LineNo_1", typeof(string));
                table_.Columns.Add("IsBuyer", typeof(bool));
                table_.Columns.Add("IsInternal", typeof(bool));
                table_.Columns.Add("CreatedDate", typeof(DateTime));
                table_.Columns.Add("UpdatedDate", typeof(DateTime));
                table_.Columns.Add("CreatedBy", typeof(string));
                table_.Columns.Add("UpdatedBy", typeof(string));
                table.Columns.Add("PartiesAmount1", typeof(int));
                table.Columns.Add("ShortUnitID", typeof(int));
                table_.Columns.Add("PartiesAmount2", typeof(int));
                table_.Columns.Add("LongUnitID", typeof(int));
                table_.Columns.Add("TotalAmount", typeof(int));
                table_.Columns.Add("IsDeleted", typeof(bool));
                List<InternalOrderForm> listOrderEntryForm = new List<InternalOrderForm>();
                int lotCount = 1;
                string BOMError = "";
                foreach (DataRow dr in table.Rows)
                {

                    if (dr.ItemArray[0].ToString() != "" && dr.ItemArray[0].ToString() != "Buyer")
                    {
                        InternalOrderForm orderEntryForm = new InternalOrderForm();
                        BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
                        BuyerManager buyerManager = new BuyerManager();
                        BuyerMaster buyermaster = new BuyerMaster();
                        buyermaster = buyerManager.GetBuyerFullName(dr[0].ToString());
                        SeasonManager seasonManager = new SeasonManager();
                        SeasonMaster seasonMaster = new SeasonMaster();
                        seasonMaster = seasonManager.GetSeasonFullName(dr[1].ToString());
                        SizeRangeDetails sizeRangeMaster = new SizeRangeDetails();
                        SizeRangeDetailsManager sizeRangemasterManager = new SizeRangeDetailsManager();
                        sizeRangeMaster = sizeRangemasterManager.GetSizeRangeName(dr[10].ToString());
                        UOMManager uomManager = new UOMManager();
                        UomMaster uomMaster = new UomMaster();
                        uomMaster = uomManager.GetUnitName(dr[9].ToString());
                        Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
                        Product_BuyerStyleMaster productStyleMaster = new Product_BuyerStyleMaster();
                        productStyleMaster = product_BuyerStyleManager.GetOurStyle(dr[7].ToString());
                        MaterialManager materialManager = new MaterialManager();
                        MaterialMaster materialMaster = new MaterialMaster();
                        MaterialNameManager materialNameManager = new MaterialNameManager();
                        tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
                        var BomID = dr[7].ToString();
                        if (BomID == "2028 208 7066 N17")
                        {
                            string test = "";
                        }
                        if (buyermaster != null && seasonMaster != null && sizeRangeMaster != null && uomMaster != null && productStyleMaster != null)
                        {
                            ID++;

                            orderEntryForm.BuyerOrderSlNo = dr[5].ToString();
                            OrderEntry orderentry = new OrderEntry();
                            orderentry = buyerOrderEntryManager.BuyerOrderExitNo(dr[5].ToString(), seasonMaster.SeasonMasterId.ToString(), buyermaster.BuyerMasterId.ToString());
                            if (orderentry != null && orderentry.BuyerOrderSlNo != "")
                            {
                                BOMError += orderentry.BuyerOrderSlNo + " " + "Already existed! Please delete and try again." + ",";
                            }
                            orderEntryForm.LotNo = dr[2].ToString();
                            orderEntryForm.Count = lotCount.ToString();
                            orderEntryForm.WeekNo = dr[2].ToString();
                            string date1 = DateTime.FromOADate(Convert.ToDouble(dr.ItemArray[3].ToString())).ToString();
                            orderEntryForm.Date = Convert.ToDateTime(date1);
                            orderEntryForm.BuyerSeason = seasonMaster.SeasonMasterId;
                            orderEntryForm.BuyerName = buyermaster.BuyerMasterId;
                            orderEntryForm.OrderProjectionNo = "";
                            orderEntryForm.BuyerPoNo = dr[5].ToString();
                            orderEntryForm.OurStyle = productStyleMaster.ProductOrBuyerStyleId.ToString();
                            orderEntryForm.LeatherDescription = productStyleMaster.LeatherName_1;
                            orderEntryForm.DiscountPer = 0;
                            orderEntryForm.QuoteNo = null;
                            orderEntryForm.CountryStamp = "";
                            orderEntryForm.CustomerPlan = "0";
                            orderEntryForm.CustomerDate = Convert.ToDateTime(date1);
                            orderEntryForm.AgentMasterId = 1;
                            orderEntryForm.CommPer = string.Empty;
                            orderEntryForm.ExFactoryDate = null;
                            orderEntryForm.ShipmentMode = "1";
                            orderEntryForm.SampleReqNo = string.Empty;
                            orderEntryForm.Brand = string.Empty;
                            orderEntryForm.BarCodeNo = string.Empty;
                            orderEntryForm.BomNo = productStyleMaster.ProductOrBuyerStyleId.ToString();
                            orderEntryForm.Last = "";
                            orderEntryForm.ColorMasterId = "";
                            if (Url.ToLower().Contains("unit2"))
                            {
                                orderEntryForm.BuyerStyleNo = "";
                                orderEntryForm.Last = "";
                                orderEntryForm.ColorMasterId = "";
                            }
                            else if (Url.ToLower().Contains("endura"))
                            {
                                orderEntryForm.BuyerStyleNo = "";
                                orderEntryForm.Last = "";
                                orderEntryForm.ColorMasterId = "";
                            }
                            else if (Url.ToLower().Contains("localhost"))
                            {
                                orderEntryForm.BuyerStyleNo = BomID.ToString();
                                orderEntryForm.Last = Regex.Replace(BomID.Substring(4, 5), @"\s+", "");
                                orderEntryForm.ColorMasterId = Regex.Replace(BomID.Substring(8, 5), @"\s+", "");
                            }
                            else
                            {
                                orderEntryForm.BuyerStyleNo = BomID.ToString();
                                orderEntryForm.Last = Regex.Replace(BomID.Substring(4, 5), @"\s+", "");
                                orderEntryForm.ColorMasterId = Regex.Replace(BomID.Substring(8, 5), @"\s+", "");
                            }
                            orderEntryForm.FinishedProdType = productStyleMaster.FinishedProductType;
                            orderEntryForm.ProductTypeId = productStyleMaster.BuyerModel_ProductType;
                            orderEntryForm.AmendmentNoWithDate = string.Empty;
                            orderEntryForm.TotalOrderForWeek = string.Empty;
                            orderEntryForm.OrderType = 0;
                            orderEntryForm.Currency = null;
                            orderEntryForm.Rs = null;
                            orderEntryForm.Parties = null;
                            orderEntryForm.GradeMasterId = 1;
                            orderEntryForm.SizeRangeMasterId = sizeRangeMaster.SizeRangeDetailsId;
                            orderEntryForm.Remarks1 = string.Empty;
                            orderEntryForm.Remarks2 = string.Empty;
                            orderEntryForm.LineNo_1 = string.Empty;
                            orderEntryForm.IsBuyer = true;
                            orderEntryForm.IsInternal = false;
                            orderEntryForm.CreatedDate = DateTime.Now;
                            orderEntryForm.UpdatedDate = null;
                            orderEntryForm.CreatedBy = HttpContext.Session["UserName"].ToString();
                            orderEntryForm.UpdatedBy = string.Empty;
                            orderEntryForm.PartiesAmount1 = null;
                            orderEntryForm.ShortUnitID = uomMaster.UomMasterId;
                            orderEntryForm.PartiesAmount2 = null;
                            orderEntryForm.LongUnitID = uomMaster.UomMasterId;
                            orderEntryForm.TotalAmount = 0;
                            orderEntryForm.IsDeleted = false;

                            listOrderEntryForm.Add(orderEntryForm);

                            lotCount++;
                        }
                        else
                        {

                            BOMError += BomID + ",";


                        }
                    }
                }
                if (string.IsNullOrEmpty(BOMError))
                {
                    Session["SuccessOrder"] = "Excel data imported Successfully";
                    List<OrderEntry> listOrderEntryFormList = new List<OrderEntry>();
                    foreach (DataRow dr in table.Rows)
                    {

                        if (dr.ItemArray[0].ToString() != "" && dr.ItemArray[0].ToString() != "Buyer")
                        {
                            OrderEntry orderEntryForm = new OrderEntry();
                            BuyerManager buyerManager = new BuyerManager();
                            BuyerMaster buyermaster = new BuyerMaster();
                            buyermaster = buyerManager.GetBuyerFullName(dr[0].ToString());
                            SeasonManager seasonManager = new SeasonManager();
                            SeasonMaster seasonMaster = new SeasonMaster();
                            seasonMaster = seasonManager.GetSeasonFullName(dr[1].ToString());
                            SizeRangeDetails sizeRangeMaster = new SizeRangeDetails();
                            SizeRangeDetailsManager sizeRangemasterManager = new SizeRangeDetailsManager();
                            sizeRangeMaster = sizeRangemasterManager.GetSizeRangeName(dr[10].ToString());
                            UOMManager uomManager = new UOMManager();
                            UomMaster uomMaster = new UomMaster();
                            uomMaster = uomManager.GetUnitName(dr[9].ToString());
                            Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
                            Product_BuyerStyleMaster productStyleMaster = new Product_BuyerStyleMaster();
                            productStyleMaster = product_BuyerStyleManager.GetOurStyle(dr[7].ToString());
                            MaterialManager materialManager = new MaterialManager();
                            MaterialMaster materialMaster = new MaterialMaster();
                            MaterialNameManager materialNameManager = new MaterialNameManager();
                            tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
                            var BomID = dr[7].ToString();
                            if (buyermaster != null && seasonMaster != null && sizeRangeMaster != null && uomMaster != null && productStyleMaster != null)
                            {
                                ID++;
                                orderEntryForm.BuyerOrderSlNo = dr[5].ToString();
                                orderEntryForm.LotNo = dr[2].ToString();
                                orderEntryForm.Count = lotCount.ToString();
                                orderEntryForm.WeekNo = dr[2].ToString();
                                orderEntryForm.Date = DateTime.FromOADate(Convert.ToDouble(dr.ItemArray[3].ToString()));
                                orderEntryForm.BuyerSeason = seasonMaster.SeasonMasterId;
                                orderEntryForm.BuyerName = buyermaster.BuyerMasterId;
                                orderEntryForm.OrderProjectionNo = "";
                                orderEntryForm.BuyerPoNo = dr[5].ToString();
                                orderEntryForm.OurStyle = productStyleMaster.ProductOrBuyerStyleId.ToString();
                                orderEntryForm.LeatherDescription = productStyleMaster.LeatherName_1;
                                orderEntryForm.DiscountPer = 0;
                                orderEntryForm.QuoteNo = null;
                                orderEntryForm.CountryStamp = "";
                                orderEntryForm.CustomerPlan = "0";
                                orderEntryForm.CustomerDate = DateTime.FromOADate(Convert.ToDouble(dr.ItemArray[3].ToString()));
                                orderEntryForm.AgentMasterId = 1;
                                orderEntryForm.CommPer = string.Empty;
                                orderEntryForm.ExFactoryDate = null;
                                orderEntryForm.ShipmentMode = "1";
                                orderEntryForm.SampleReqNo = string.Empty;
                                orderEntryForm.Brand = string.Empty;
                                orderEntryForm.BarCodeNo = string.Empty;
                                orderEntryForm.BomNo = productStyleMaster.ProductOrBuyerStyleId.ToString();

                                if (Url.ToLower().Contains("unit2"))
                                {
                                    orderEntryForm.Last = "";
                                    orderEntryForm.ColorMasterId = "";
                                    orderEntryForm.BuyerStyleNo = "";
                                }
                                else if (Url.ToLower().Contains("endura"))
                                {
                                    orderEntryForm.Last = "";
                                    orderEntryForm.ColorMasterId = "";
                                    orderEntryForm.BuyerStyleNo = "";
                                }
                                else if (Url.ToLower().Contains("localhost"))
                                {
                                    orderEntryForm.Last = Regex.Replace(BomID.Substring(4, 5), @"\s+", "");
                                    orderEntryForm.ColorMasterId = Regex.Replace(BomID.Substring(8, 5), @"\s+", "");
                                    orderEntryForm.BuyerStyleNo = BomID.ToString();
                                }
                                else
                                {
                                    orderEntryForm.Last = Regex.Replace(BomID.Substring(4, 5), @"\s+", "");
                                    orderEntryForm.ColorMasterId = Regex.Replace(BomID.Substring(8, 5), @"\s+", "");
                                    orderEntryForm.BuyerStyleNo = BomID.ToString();
                                }
                                orderEntryForm.FinishedProdType = productStyleMaster.FinishedProductType;
                                orderEntryForm.ProductTypeId = productStyleMaster.BuyerModel_ProductType;
                                orderEntryForm.AmendmentNoWithDate = string.Empty;
                                orderEntryForm.TotalOrderForWeek = string.Empty;
                                orderEntryForm.OrderType = 0;
                                orderEntryForm.Currency = null;
                                orderEntryForm.Rs = null;
                                orderEntryForm.Parties = null;
                                orderEntryForm.GradeMasterId = 1;
                                orderEntryForm.SizeRangeMasterId = sizeRangeMaster.SizeRangeDetailsId;
                                orderEntryForm.Remarks1 = string.Empty;
                                orderEntryForm.Remarks2 = string.Empty;
                                orderEntryForm.LineNo_1 = string.Empty;
                                orderEntryForm.IsBuyer = true;
                                orderEntryForm.IsInternal = false;
                                orderEntryForm.CreatedDate = DateTime.Now;
                                orderEntryForm.UpdatedDate = null;
                                orderEntryForm.CreatedBy = HttpContext.Session["UserName"].ToString();
                                orderEntryForm.UpdatedBy = string.Empty;
                                orderEntryForm.PartiesAmount1 = null;
                                orderEntryForm.ShortUnitID = uomMaster.UomMasterId;
                                orderEntryForm.PartiesAmount2 = null;
                                orderEntryForm.LongUnitID = uomMaster.UomMasterId;
                                orderEntryForm.TotalAmount = 0;
                                orderEntryForm.IsDeleted = false;
                                listOrderEntryFormList.Add(orderEntryForm);
                                lotCount++;
                                BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
                                SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                                MultipleScheduleDetailsManager multipleScheduleDetailsManager = new MultipleScheduleDetailsManager();
                                CartonDetailsManager cartonDetailsManager = new CartonDetailsManager();
                                OrderEntry internalOrderEntryForm = new OrderEntry();
                                int orderEntryid = buyerOrderEntryManager.Post(orderEntryForm);
                                int i = 12;
                                while (i <= 38)
                                {
                                    SizeRangeQtyRate SizeRangeQtyRateDetails = new SizeRangeQtyRate();
                                    SizeRangeQtyRateDetails.SizeRange = dr.Table.Columns[i].ToString();
                                    if (string.IsNullOrEmpty(dr[i].ToString()))
                                    {
                                        SizeRangeQtyRateDetails.Qty = 0;
                                    }
                                    else
                                    {
                                        SizeRangeQtyRateDetails.Qty = Convert.ToDecimal(dr[i].ToString());
                                    }
                                    SizeRangeQtyRateDetails.SizeQRId = 0;
                                    SizeRangeQtyRateDetails.Rate = 0;
                                    SizeRangeQtyRateDetails.OrderEntryId = orderEntryid;
                                    SizeRangeQtyRateDetails.CreatedDate = DateTime.Now;
                                    SizeRangeQtyRateDetails.UpdatedDate = null;
                                    sizeRangeQtyRateManager.Post_(SizeRangeQtyRateDetails);
                                    i++;
                                }
                                OrderEntry internalOrderForm_ = new OrderEntry();
                                internalOrderForm_ = buyerOrderEntryManager.GetOrderEntryId(orderEntryid);
                                internalOrderForm_.TotalAmount = Convert.ToInt32(dr[11].ToString());
                                buyerOrderEntryManager.Post(internalOrderForm_);

                            }
                            else
                            {
                                BOMError += BomID;
                            }
                        }
                    }
                }
                else
                {
                    Session["BOMerror_"] = null;
                    Session["BOMerror_"] = BOMError.TrimEnd(',');
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            return RedirectToAction("BuyerOrderEntryFormView");
        }
        protected string GetBulkCopyColumnException(Exception ex, SqlBulkCopy bulkcopy)

        {
            string message = string.Empty;
            if (ex.Message.Contains("Received an invalid column length from the bcp client for colid"))

            {
                string pattern = @"\d+";
                Match match = Regex.Match(ex.Message.ToString(), pattern);
                var index = Convert.ToInt32(match.Value) - 1;

                FieldInfo fi = typeof(SqlBulkCopy).GetField("_sortedColumnMappings", BindingFlags.NonPublic | BindingFlags.Instance);
                var sortedColumns = fi.GetValue(bulkcopy);
                var items = (Object[])sortedColumns.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sortedColumns);

                FieldInfo itemdata = items[index].GetType().GetField("_metadata", BindingFlags.NonPublic | BindingFlags.Instance);
                var metadata = itemdata.GetValue(items[index]);
                var column = metadata.GetType().GetField("column", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                var length = metadata.GetType().GetField("length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                message = (String.Format("Column: {0} contains data with a length greater than: {1}", column, length));
            }
            return message;
        }
        #endregion
    }
}
