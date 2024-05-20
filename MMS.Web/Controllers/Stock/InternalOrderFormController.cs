using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using InternalOrderForm = MMS.Core.Entities.Stock.InternalOrderForm;

namespace MMS.Web.Controllers.Stock
{
    public class InternalOrderFormController : Controller
    {
        #region Helper Method

        public ActionResult BuyerOrderEntryFormView()
        {
            return View("~/Views/Stock/InternalOrderForm/InternalOrderForm.cshtml");
        }
        public ActionResult InternalOrderEntryFormGrid()
        {

            List<OrderEntry> orderEntryEntityModellist = new List<OrderEntry>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            orderEntryEntityModellist = buyerOrderEntryManager.GetIntenalOrderGrid("");

            OrderEntryModel model = new OrderEntryModel();
            var pager = new Pager(orderEntryEntityModellist.Count(), 1);
            var viewModel = new OrderEntryModel
            {
                OrderEntryList = orderEntryEntityModellist.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };
            //return PartialView("~/Views/Stock/InternalOrderForm/Partial/InternalOrderFormGrid.cshtml", model);
            return PartialView("~/Views/Stock/InternalOrderForm/Partial/InternalOrderEntryFormGrid.cshtml", viewModel);
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
                    model.IsBuyer = false;
                    model.IsInternal = true;
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
            return PartialView("~/Views/Stock/InternalOrderForm/Partial/InternalOrderEntryFormDetails.cshtml", model);
        }
        public ActionResult Search(string filter, int? page)
        {
            List<OrderEntry> orderEntryEntityModellist = new List<OrderEntry>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            orderEntryEntityModellist = buyerOrderEntryManager.GetIntenalOrderGrid(filter);
            OrderEntryModel model = new OrderEntryModel();
            //model.OrderEntryList = orderEntryEntityModellist;
            var pager = new Pager(orderEntryEntityModellist.Count(), page);
            var viewModel = new OrderEntryModel
            {
                OrderEntryList = orderEntryEntityModellist.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };
            return PartialView("~/Views/Stock/InternalOrderForm/Partial/InternalOrderFormGrid.cshtml", viewModel);
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
            decimal totalOrder = items.Sum(x => x.TotalAmount ?? 0);
            var TotalweekOrders = items.ToList();

            return Json(totalOrder, JsonRequestBehavior.AllowGet);
        }
        public ActionResult isExistBuyerOrderSlNO(string OrderSlNo)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            OrderEntry arg_ = new OrderEntry();
            arg_ = buyerOrderEntryManager.GetBuyerOderSlNo(OrderSlNo);
            string Message = "";
            if (arg_ != null)
            {
                Message = "Already Existed";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
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
            decimal totalOrder = items.Sum(x => x.TotalAmount ?? 0);
            var TotalweekOrders = items.ToList();

            return Json(totalOrder, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult InternalOrderForm(OrderEntryModel model)
        {

            int Result = 0;
            int orderEntryId = 0;
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
            MultipleScheduleDetailsManager multipleScheduleDetailsManager = new MultipleScheduleDetailsManager();
            CartonDetailsManager cartonDetailsManager = new CartonDetailsManager();
            OrderEntry iExistBuyerEntry = new OrderEntry();
            OrderEntry BuyerEntry = new OrderEntry();
            OeShipmentDetails oeShipmentDetails = new OeShipmentDetails();
            OeOtherDetails oeOtherDetails = new OeOtherDetails();
            CartonDetails cartonDetails = new CartonDetails();
            iExistBuyerEntry = buyerOrderEntryManager.GetBuyerOrderDetails(model.BuyerOrderSlNo, model.BuyerSeason, model.BuyerName);
            if (model.OrderEntryId == 0 && (iExistBuyerEntry == null || iExistBuyerEntry.IsInternal == false))
            {
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
                BuyerEntry.IsInternal = true;
                BuyerEntry.IsBuyer = false;
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
                if (model.Last == "undefined")
                {
                    BuyerEntry.Last = "";
                }
                else
                {
                    BuyerEntry.Last = model.Last;
                }

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
                BuyerEntry.IsBuyer = model.IsBuyer;
                BuyerEntry.IsInternal = true;
                BuyerEntry.CreatedDate = DateTime.Now;
                BuyerEntry.PartiesAmount1 = model.PartiesAmount1;
                BuyerEntry.ShortUnitID = model.ShortUnitID;
                BuyerEntry.PartiesAmount2 = model.PartiesAmount2;
                BuyerEntry.LongUnitID = model.LongUnitID;
                BuyerEntry.TotalAmount = model.TotalAmount;
                orderEntryId = buyerOrderEntryManager.Post(BuyerEntry);

                //SizeRange Save
                if (model.SizeQuantityRate != null)
                {
                    var SizeQuantityRate = JsonConvert.DeserializeObject<List<SizeQuantityRateObject>>(model.SizeQuantityRate);

                    foreach (var item in SizeQuantityRate)
                    {
                        SizeRangeQtyRate SizeRangeQtyRateDetails = new SizeRangeQtyRate();
                        SizeRangeQtyRateDetails.SizeRange = item.Size;
                        //if (item.quantity != "" && item.Rate != "")
                        //{
                        SizeRangeQtyRateDetails.Qty = Convert.ToDecimal(item.quantity);
                        SizeRangeQtyRateDetails.Rate = Convert.ToDecimal(item.Rate);
                        SizeRangeQtyRateDetails.OrderEntryId = orderEntryId;
                        SizeRangeQtyRateDetails.CreatedDate = DateTime.Now;
                        SizeRangeQtyRateDetails.UpdatedDate = DateTime.Now;
                        sizeRangeQtyRateManager.Post(SizeRangeQtyRateDetails);
                        //}
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
                    Result = 1;
                }
            }
            else if (model.OrderEntryId != 0 && iExistBuyerEntry.IsInternal != false)
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
                BuyerEntry.IsBuyer = false;
                BuyerEntry.IsInternal = true;
                BuyerEntry.PartiesAmount1 = model.PartiesAmount1;
                BuyerEntry.ShortUnitID = model.ShortUnitID;
                BuyerEntry.PartiesAmount2 = model.PartiesAmount2;
                BuyerEntry.LongUnitID = model.LongUnitID;
                BuyerEntry.TotalAmount = model.TotalAmount;
                BuyerEntry.CreatedDate = DateTime.Now;
                //BuyerEntry.UpdatedDate = DateTime.Now;
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
                    Result = 2;
                }
            }
            else if (model.OrderEntryId == 0 && iExistBuyerEntry.IsInternal != false)
            {
                Result = 3;
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
    }
}
