using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Core.Entities;
using static iTextSharp.text.pdf.AcroFields;
using MMS.Core.Entities.Stock;
using MMS.Web.Models;
using MMS.Web.Models.StockModel;
using System.Web.UI;
using MMS.Data.Mapping;
using Excel.Log;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Web.Providers.Entities;

namespace MMS.Web.Controllers
{
    public class InvoiceController : Controller
    {
        #region Accounts View
        [HttpGet]
        public ActionResult InvoiceMaster()
        {
            Models.Invoice.order_header model = new Models.Invoice.order_header();
            return View(model);
        }
        [HttpGet]
        public ActionResult InvoiceFormView(int? page)
        {
            OrderEntryModel model = new OrderEntryModel();
            BuyerOrderEntryManager oManager = new BuyerOrderEntryManager();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            MMS.Core.Entities.BuyerMaster obj = new Core.Entities.BuyerMaster();
            var custaddlist = oCustAddressMangers.Get();
            model.OrderEntryList = oManager.BuyerOrderforInvoiceList();

            BuyerManager oBuyerManager = new BuyerManager();
            int buyerid = 0;
            foreach (var item1 in model.OrderEntryList)
            {
                buyerid = item1.BuyerName ?? 0;
                obj = oBuyerManager.GetBuyerMasterId(buyerid);
                if (buyerid == obj.BuyerMasterId)
                {
                    item1.CustomerName = obj.BuyerFullName;
                }
            }
            foreach (var item1 in model.OrderEntryList)
            {
                foreach (var item2 in custaddlist)
                {
                    if (Convert.ToInt32(obj.BuyerAddress1) == item2.AddressType)
                    {
                        item1.Add1 = item2.Add1;
                    }
                    if (Convert.ToInt32(obj.BuyerAddress2) == item2.AddressType)
                    {
                        item1.Add2 = item2.Add2;
                    }
                }
            }
            var pager = new Pager(model.OrderEntryList.Count(), page);
            var viewModel = new OrderEntryModel
            {
                OrderEntryList = model.OrderEntryList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };
            return View("Partial/InvoiceDetailsGrid", viewModel);
        }
        [HttpGet]
        public ActionResult InvoiceDetailsGrid(int buyer = 0, int ProcessStatus = 0)
        {
            var viewModel = new OrderEntryModel();
            OrderEntryModel model = new OrderEntryModel();
            BuyerOrderEntryManager oManager = new BuyerOrderEntryManager();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            MMS.Core.Entities.BuyerMaster obj = new Core.Entities.BuyerMaster();
            var custaddlist = oCustAddressMangers.Get();
            if (buyer == 0 && ProcessStatus == 0)
            {
                model.OrderEntryList = oManager.BuyerOrderforInvoiceList();
            }
            else
            {
                if (buyer > 0 && ProcessStatus == 0)
                {
                    model.OrderEntryList = oManager.BuyerOrderforBuyerList(buyer);
                }
                else if (ProcessStatus > 0 && buyer == 0)
                {
                    model.OrderEntryList = oManager.BuyerOrderforProcessList(ProcessStatus);
                }
                else if (ProcessStatus > 0 && buyer > 0)
                {
                    model.OrderEntryList = oManager.BuyerOrderforbuyerandProcessList(buyer, ProcessStatus);
                }
            }
            BuyerManager oBuyerManager = new BuyerManager();
            int buyerid = 0;
            foreach (var item1 in model.OrderEntryList)
            {
                buyerid = item1.BuyerName ?? 0;
                obj = oBuyerManager.GetBuyerMasterId(buyerid);
                if (buyerid == obj.BuyerMasterId)
                {
                    item1.CustomerName = obj.BuyerFullName;
                }
            }
            foreach (var item1 in model.OrderEntryList)
            {
                foreach (var item2 in custaddlist)
                {
                    if (obj.BuyerMasterId == item2.BuyerId)
                    {
                        if (Convert.ToInt32(obj.BuyerAddress1) == item2.AddressType)
                        {
                            item1.Add1 = item2.Add1;
                        }
                        if (Convert.ToInt32(obj.BuyerAddress2) == item2.AddressType)
                        {
                            item1.Add2 = item2.Add2;
                        }
                    }
                }
            }

            if (model.OrderEntryList.Count() > 9)
            {
                var pager = new Pager(model.OrderEntryList.Count(), 5);
                viewModel = new OrderEntryModel
                {
                    OrderEntryList = model.OrderEntryList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                    Pager = pager
                };
            }
            else
            {
                viewModel = model;
            }
            return View("Partial/InvoiceDetailsGrid", viewModel);
        }
        public ActionResult InvoiceNewGridbind(int buyer = 0, int ProcessStatus = 0, int orderid = 0, DateTime? from_date = null, DateTime? todate = null)
        {
            var viewModel = new OrderEntryModel();

            OrderEntryModel model = new OrderEntryModel();
            BuyerOrderEntryManager oManager = new BuyerOrderEntryManager();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            MMS.Core.Entities.BuyerMaster obj = new Core.Entities.BuyerMaster();
            var custaddlist = oCustAddressMangers.Get();
            model.OrderEntryList = oManager.GetbuyerOrderprocessEntryList(buyer, ProcessStatus, orderid, from_date, todate);

            BuyerManager oBuyerManager = new BuyerManager();
            int buyerid = 0;
            foreach (var item1 in model.OrderEntryList)
            {
                buyerid = item1.BuyerName ?? 0;
                obj = oBuyerManager.GetBuyerMasterId(buyerid);
                if (buyerid == obj.BuyerMasterId)
                {
                    item1.CustomerName = obj.BuyerFullName;
                }
            }
            foreach (var item1 in model.OrderEntryList)
            {
                foreach (var item2 in custaddlist)
                {
                    if (Convert.ToInt32(obj.BuyerAddress1) == item2.AddressType)
                    {
                        item1.Add1 = item2.Add1;
                    }
                    if (Convert.ToInt32(obj.BuyerAddress2) == item2.AddressType)
                    {
                        item1.Add2 = item2.Add2;
                    }
                }
            }


            var pager = new Pager(model.OrderEntryList.Count(), 5);
            viewModel = new OrderEntryModel
            {
                OrderEntryList = model.OrderEntryList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult InvoiceDetails()
        {
            Models.Invoice.order_header model = new Models.Invoice.order_header();
            InvoiceManager manager = new InvoiceManager();
            List<OrderEntry> OrderEntryList = new List<OrderEntry>();
            MMS.Core.Entities.BuyerMaster obj = new Core.Entities.BuyerMaster();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            OrderEntryList = buyerOrderEntryManager.GetBuyerOrderGrid("");
            //.Select(item => new Models.StockModel.OrderEntryModel
            //{
            //    OrderEntryId = item.OrderEntryId,
            //    CustomerName = item.CustomerName,
            //    BuyerOrderSlNo = item.BuyerOrderSlNo,
            //    LotNo = item.LotNo,
            //    Date = Convert.ToDateTime(item.Date).ToString("dd/MM/yyyy"),
            //    DiscountPer = item.DiscountPer,
            //    TotalOrderForWeek = item.TotalOrderForWeek,
            //    TotalAmount = item.TotalAmount,
            //    BomNo = item.BomNo,
            //    BuyerPoNo = item.BuyerPoNo
            //})
            //       .ToList();
            //BuyerManager oBuyerManager = new BuyerManager();
            //foreach (var item1 in model.Invoicedetailslist)
            //{
            //    obj = oBuyerManager.GetBuyerMasterId(item1.customerid);
            //    if (item1.customerid == obj.BuyerMasterId)
            //    {
            //        item1.CustomerName = obj.BuyerFullName;
            //    }
            //}

            return View("Partial/InvoiceDetails", model);
        }
        [HttpGet]
        public ActionResult GetBuyerOrderlist(string buyername)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<Core.Entities.Stock.GetBuyerOrderlist> result = new List<Core.Entities.Stock.GetBuyerOrderlist>();
            result = buyerOrderEntryManager.GetBuyerOrderlist(Convert.ToInt32(buyername));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInvoiceAddressDetailbind(int orderid)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<Core.Entities.Stock.getbuyerorderaddressdetails> result = new List<Core.Entities.Stock.getbuyerorderaddressdetails>();
            result = buyerOrderEntryManager.getbuyerorderaddressdetails(orderid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOrderGridDetailbind(int orderid)
        {
            OrderEntry model = new OrderEntry();
            BuyerOrderEntryManager manager = new BuyerOrderEntryManager();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            MMS.Core.Entities.BuyerMaster obj = new Core.Entities.BuyerMaster();
            model = manager.GetOrderEntryId(orderid);
            BuyerManager oBuyerManager = new BuyerManager();
            int buyerid = model.BuyerName ?? 0;
            obj = oBuyerManager.GetBuyerMasterId(buyerid);
            var custaddlist = oCustAddressMangers.GetCustAddressbuyerid(buyerid);
            if (buyerid == obj.BuyerMasterId)
            {
                model.CustomerName = obj.BuyerFullName;
            }
            if (Convert.ToInt32(obj.BuyerAddress1) == custaddlist.AddressType)
            {
                model.Add1 = custaddlist.Add1;
            }
            if (Convert.ToInt32(obj.BuyerAddress2) == custaddlist.AddressType)
            {
                model.Add2 = custaddlist.Add2;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InvoiceNewGrid(int orderid)
        {
            int buyerid = 0;
            InvoiceManager inv = new InvoiceManager();
            List<Core.Entities.Order_Details> orderdetails = new List<Core.Entities.Order_Details>();
            List<Core.Entities.order_header> oOrderheader = new List<Core.Entities.order_header>();
            OrderDetailsManager orderdetailtsmanger = new OrderDetailsManager();
            OrderEntryModel model = new OrderEntryModel();
            BuyerOrderEntryManager manager = new BuyerOrderEntryManager();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            MMS.Core.Entities.BuyerMaster obj = new Core.Entities.BuyerMaster();
            model.OrderEntryList = manager.GetOrderListEntryId(orderid);
            //model.ObjOrderEntry = manager.GetOrderEntryIdbyFilter(orderid, processstatus);
            BuyerManager oBuyerManager = new BuyerManager();

            foreach (var item in model.OrderEntryList)
            {
                buyerid = item.BuyerName ?? 0;
            }
            obj = oBuyerManager.GetBuyerMasterId(buyerid);
            var custaddlist = oCustAddressMangers.GetCustAddressbuyerid(buyerid);
            if (buyerid == obj.BuyerMasterId)
            {
                model.CustomerName = obj.BuyerFullName;
            }
            if (Convert.ToInt32(obj.BuyerAddress1) == custaddlist.AddressType)
            {
                model.OrderEntryList[0].Add1 = custaddlist.Add1;
            }
            if (Convert.ToInt32(obj.BuyerAddress2) == custaddlist.AddressType)
            {
                model.OrderEntryList[0].Add2 = custaddlist.Add2;
            }
            if (Convert.ToInt32(obj.BuyerAddress2) == 3)
            {
                model.OrderEntryList[0].Add2 = custaddlist.Add2;
            }
            oOrderheader = inv.GetInvoiceListId(orderid);
            if ((oOrderheader.Count > 0))
            {
                //for (int i = 0; i < model.OrderEntryList.Count && i < oOrderheader.Count; i++)
                //{
                //    model.OrderEntryList[i].Id = oOrderheader[i].Id;
                //}
                //orderdetails = orderdetailtsmanger.GetOrderDetailsbyId(oOrderheader.Id);
                foreach (var item in oOrderheader)
                {
                    orderdetails.AddRange(orderdetailtsmanger.GetOrderDetailsbyId(item.Id).ToList());
                }
                if (orderdetails.Count > 0)
                {
                    // model.ObjOrderEntry.InvoiceQty = orderdetails.Sum(item => item.QtyProcessed);
                    foreach (var item1 in model.OrderEntryList)
                    {
                        item1.InvoiceQty = orderdetails.Sum(item => item.QtyProcessed);
                    }
                }
            }
            //model.OrderEntryList = new List<OrderEntry>();
            return Json(model, JsonRequestBehavior.AllowGet);
            //return View("Partial/InvoiceDetailsGrid", model);
        }
        public ActionResult OrderHeaderGridBind(int buerid = 0, int Process = 0, int OrderEntryId = 0, int orderheaderid = 0, DateTime? from_date = null, DateTime? to_date = null)
        {
            var viewModel = new OrderEntryModel();
            OrderEntryModel model = new OrderEntryModel();
            InvoiceManager oManager = new InvoiceManager();
            model.orderheaderList = oManager.GetOrderheaderListEntryId(buerid, Process, OrderEntryId, orderheaderid, from_date, to_date);

            if (model.orderheaderList.Count() > 9)
            {
                var pager = new Pager(model.orderheaderList.Count(), 5);
                viewModel = new OrderEntryModel
                {
                    orderheaderList = model.orderheaderList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                    Pager = pager
                };
            }
            else
            {
                viewModel = model;
            }
            return View("Partial/OrderHeaderGridBind", viewModel);
        }
        public ActionResult OrderHeaderGridBindList(int buerid = 0, int Process = 0, int OrderEntryId = 0, int orderheaderid = 0, DateTime? from_date = null, DateTime? to_date = null)
        {
            var viewModel = new OrderEntryModel();
            OrderEntryModel model = new OrderEntryModel();
            InvoiceManager oManager = new InvoiceManager();
            model.orderheaderList = oManager.GetOrderheaderListEntryId(buerid, Process, OrderEntryId, orderheaderid, from_date, to_date);
            if (model.orderheaderList.Count() > 9)
            {
                var pager = new Pager(model.orderheaderList.Count(), 5);
                viewModel = new OrderEntryModel
                {
                    orderheaderList = model.orderheaderList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                    Pager = pager
                };
            }
            else
            {
                viewModel = model;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult CustInvoiceInsert(Models.Invoice.order_header model)
        {
            string status = "";
            Core.Entities.order_header order_header = new Core.Entities.order_header();
            Core.Entities.Order_Details Oorder_details = new Core.Entities.Order_Details();
            InvoiceManager manager = new InvoiceManager();
            OrderDetailsManager omanager = new OrderDetailsManager();
            string dateOnly = DateTime.Now.ToString("yyyy-MM-dd");
            if (model.courencyoption.ToUpper() == "ZAR")
            {
                model.ConversionValue = manager.Getcurrencyconversion("ZAR", "USD", dateOnly);
            }
            else
            {
                model.ConversionValue = manager.Getcurrencyconversion("USD", "ZAR", dateOnly);
            }
            order_header.Id = model.Id;
            order_header.CustomerId = model.customerid;
            order_header.OrderId = model.orderid;
            order_header.DoctypeId = model.doctypeid;
            order_header.Revision = model.revision;
            order_header.DocStateId = model.docstateid;
            order_header.QuoteRef = model.quoteref;
            order_header.SoRef = model.soref;
            order_header.RefDate = model.refdate;
            order_header.RefItems = model.refitems;
            order_header.RefQuantity = model.refquantity;
            order_header.RefDueDate = model.refduedate;
            order_header.RefExtendedValue = model.refextendedvalue;
            order_header.RefDiscountValue = model.refdiscountvalue;
            order_header.RefGrossValue = model.refgrossvalue;
            order_header.RefTaxValue = model.reftaxvalue;
            order_header.RefNetValue = model.refnetvalue;
            order_header.DocDate = model.docdate;
            order_header.DocItems = model.docitems;
            order_header.DocQuantity = model.docquantity;
            order_header.DocDueDate = model.docduedate;
            order_header.DocExtendedValue = model.docextendedvalue;
            order_header.DocDiscountValue = model.docdiscountvalue;
            order_header.DocGrossValue = model.docgrossvalue;
            order_header.DocTaxValue = model.doctaxvalue;
            order_header.DocNetValue = model.docnetvalue;
            order_header.CustAddCode = model.custaddcode;
            order_header.CustShipCode = model.custshipcode;
            order_header.CustBillCode = model.custbillcode;
            order_header.HeaderDiscountPerc = model.headerdiscountperc;
            order_header.ShippingNotes = model.shippingnotes;
            order_header.Status = 1;
            order_header.DocDescription = model.docdescription;
            order_header.OriginalQuoteDate = model.originalquotedate;
            order_header.QuoteDate = model.quotedate;
            order_header.DueDate = model.duedate;
            order_header.TaxInclusive = model.taxinclusive;
            order_header.EmailSent = model.emailsent;
            order_header.ExternalRef = model.externalref;
            order_header.CreditLimit = model.creditlimit;
            order_header.DCBalance = model.dcbalance;
            order_header.ForeignDCBalance = model.foreigndcbalance;
            order_header.CurrencyId = model.currencyid;
            order_header.IsActive = model.isactive;
            order_header.CreatedDate = DateTime.Now;

            manager.Post(order_header);

            //Oorder_details.Id = model.Id;
            if (model.Id == 0)
            {
                Oorder_details.CustomerDocsId = order_header.Id;
            }
            else
            {
                Oorder_details.CustomerDocsId = model.Id;
            }
            Oorder_details.LineId = 1;
            Oorder_details.MaterialId = 1;
            Oorder_details.Code = "M21";
            Oorder_details.DisplayName = "9630 905 6865 N17";
            Oorder_details.PoName = "9630 905 6865 N17";
            Oorder_details.LatCost = 0;
            Oorder_details.AveCost = 0;
            Oorder_details.UomIdBuy = 1;
            Oorder_details.UomIdSell = 1;
            Oorder_details.UomFactor = "1";
            Oorder_details.TaxTypeId = 5;
            Oorder_details.TaxRate = model.reftaxvalue;
            //Oorder_details.QtyRequired = model.QtyProcessed;
            Oorder_details.QtyProcessed = Convert.ToDecimal(model.QtyProcessed);
            Oorder_details.QtyLastProcessed = model.docquantity;
            Oorder_details.QtyReserved = Convert.ToDecimal(model.QtyProcessed);
            Oorder_details.Note = "";
            Oorder_details.Discount = model.docdiscountvalue;
            Oorder_details.PriceIncl = (model.docgrossvalue);
            decimal taxrate = (model.docgrossvalue * Convert.ToDecimal(0.15));
            Oorder_details.PriceExcl = (model.docgrossvalue - (model.docgrossvalue * taxrate) / 100);
            Oorder_details.LineTotalExcl = (model.docgrossvalue - (model.docgrossvalue * taxrate) / 100);
            Oorder_details.LineTotalIncl = model.docgrossvalue;
            Oorder_details.LineTotalTax = (model.docgrossvalue - (model.docgrossvalue * taxrate) / 100);
            Oorder_details.ProcessedLineTotalExcl = model.docgrossvalue;
            Oorder_details.ProcessedLineTotalIncl = (model.docgrossvalue * taxrate) / 100;
            decimal unitprice = (model.UnitPrice * model.ConversionValue);
            decimal grocessprice = Convert.ToDecimal((model.QtyProcessed)) * unitprice;
            decimal discount = (model.Discontpercentage / 100);
            decimal discountvalue = grocessprice * discount;
            decimal netvalue = grocessprice - discountvalue;
            decimal foreigntaxrate = (grocessprice * model.Discontpercentage) / 100;
            Oorder_details.ForeignPriceExcl = (grocessprice - foreigntaxrate) + discountvalue;
            Oorder_details.ForeignPriceIncl = grocessprice;
            Oorder_details.ForeignLineTotalIncl = (grocessprice + foreigntaxrate) - discount;
            Oorder_details.ForeignLineTotalExcl = (grocessprice - foreigntaxrate) + discount;
            Oorder_details.ForeignLineTotalTax = (Oorder_details.ForeignLineTotalExcl * model.Discontpercentage) / 100;
            Oorder_details.ProcessedForeignLineTotalExcl = (Oorder_details.ForeignLineTotalTax + Oorder_details.ForeignLineTotalExcl) - discountvalue;
            Oorder_details.ProcessedForeignLineTotalIncl = (Oorder_details.ForeignLineTotalTax + Oorder_details.ForeignLineTotalExcl);
            Oorder_details.ProcessedForeignLineTotalTax = (Oorder_details.ForeignLineTotalTax - discount);
            Oorder_details.IsActive = true;
            Oorder_details.CreatedDate = DateTime.Now;
            omanager.Post(Oorder_details);
            status = "Success";
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            Core.Entities.order_header oorder_header = new Core.Entities.order_header();
            string status = "";
            InvoiceManager manager = new InvoiceManager();
            oorder_header = manager.GetInvoiceId(id);
            if (oorder_header.Id == id)
            {
                status = "Success";
                manager.Delete(id);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
