using CrystalDecisions.Shared.Json;
using iTextSharp.text;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Repository.Service;
using MMS.Web.Models;
using MMS.Web.Models.Addressdetails;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MMS.Web.Controllers
{
    public class DeliverychallanController : Controller
    {
        #region view

        [HttpGet]
        public ActionResult DCMaster()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DeliverychallanGrid(int page = 1, int pageSize = 5)
        {
            SalesorderHD_Manager salesorderManager = new SalesorderHD_Manager();
            BuyerManager BuyerManager = new BuyerManager();
            SalesorderDT_Manager SalesorderDT_Manager = new SalesorderDT_Manager();
            var data1 = BuyerManager.Get();
            List<Salesorders> totalList = new List<Salesorders>();
            var data = salesorderManager.Get();
            foreach (var i in data)
            {
                Salesorders model = new Salesorders();
                var salesorderdt = SalesorderDT_Manager.GetSOIdS(i.salesorderid_hd);
                var counts = 0;
                var count = 0;
                foreach (var k in salesorderdt)
                {
                    if (k.quantity == k.dc_qty)
                    {
                        counts++;
                    }
                }
                foreach (var j in salesorderdt)
                {
                    if (j.quantity == j.Invoice_qty)
                    {
                        count++;
                    }
                }
                model.itemInvoiced = count;
                model.itemdc = counts;
                model.SalesorderId = i.salesorderid_hd;
                model.salesorderdate = i.Salesorderdate;
                model.item = i.items;
                model.quantity = i.quantity;
                model.Total_Price = i.Total_price;
                model.Total_discountval = i.Total_disamount;
                model.Total_Grandtotal = i.grand_total;
                model.Total_Subtotal = i.Total_subtotal;
                model.Total_TaxValue = i.Total_taxamount;
                model.BuyerMaster = data1.Where(W => W.BuyerMasterId == i.customerid).ToList().FirstOrDefault();
                totalList.Add(model);
            }
            var totalCount = totalList.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalList = totalList.OrderByDescending(s => s.SalesorderId_DT)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return PartialView("Partial/DeliverychallanGrid", totalList);
        }
        public ActionResult DeliverychallanDetails()
        {
            return View("Partial/DC_Details");
        }
        #endregion
        #region Calculation
        [HttpPost]
        public ActionResult Dec_Calculationdetails(Salesorders model)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            Salesorders salesorders = new Salesorders();
            string dateOnly = DateTime.Now.ToString("yyyy-MM-dd");
            decimal? conversionval = 0;
            int id = 0;
            if (model.currencyOption.ToUpper() != "ZAR")
            {
                var ConversionValue = salesorderManager.Getcurrencyconversion("USD", "ZAR", dateOnly);
                conversionval = ConversionValue.conversionvalue;
                id = ConversionValue.id;
            }
            else
            {
                var ConversionValue = salesorderManager.Getcurrencyconversion("ZAR", "ZAR", dateOnly);
                conversionval = ConversionValue.conversionvalue;
                id = ConversionValue.id;
            }

            var product = productManager.GetId(model.ProductID);
            var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);

            var taxper = tax.TaxValue;
            var qty = model.quantity;
            var discount = model.discountval;
            var discounts = Convert.ToDecimal(discount);
            var unitprice = product.Price * conversionval;
            int intVal = int.Parse(taxper);
            var subtotal = qty * unitprice;
            var disamount = subtotal * discounts / 100;
            var subtotals = subtotal - disamount;
            var taxamount1 = subtotals * intVal / 100;
            var total = taxamount1 + subtotals;
            salesorders.Subtotal = subtotals;
            salesorders.TaxValue = taxamount1;
            salesorders.Grandtotal = total;
            salesorders.discountvalue = disamount;

            var subtotal1 = (qty + 1) * unitprice;
            var disamount1 = subtotal1 * discounts / 100;
            var subtotals1 = subtotal1 - disamount1;
            var taxamounts = subtotals1 * intVal / 100;
            var total1 = taxamounts + subtotals1;

            var subvalue = subtotals1 - subtotals;
            var disvalue = disamount1 - disamount;
            var taxvalue = taxamounts - taxamount1;
            var totalvalue = total1 - total;

            salesorders.Total_discountval = model.Total_discountval - disvalue;
            salesorders.Total_Subtotal = model.Total_Subtotal - subvalue;
            salesorders.Total_TaxValue = model.Total_TaxValue - taxvalue;
            salesorders.Total_Grandtotal = model.Total_Grandtotal - totalvalue;

            return Json(salesorders, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Inc_Calculationdetails(Salesorders model)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            Salesorders salesorders = new Salesorders();
            string dateOnly = DateTime.Now.ToString("yyyy-MM-dd");
            decimal? conversionval = 0;
            int id = 0;
            if (model.currencyOption.ToUpper() != "ZAR")
            {
                var ConversionValue = salesorderManager.Getcurrencyconversion("USD", "ZAR", dateOnly);
                conversionval = ConversionValue.conversionvalue;
                id = ConversionValue.id;
            }
            else
            {
                var ConversionValue = salesorderManager.Getcurrencyconversion("ZAR", "ZAR", dateOnly);
                conversionval = ConversionValue.conversionvalue;
                id = ConversionValue.id;
            }

            var product = productManager.GetId(model.ProductID);
            var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
            var taxper = tax.TaxValue;
            var qty = model.quantity;
            var discount = model.discountval;
            var discounts = Convert.ToDecimal(discount);
            var unitprice = product.Price * conversionval;
            int intVal = int.Parse(taxper);
            var subtotal = qty * unitprice;
            var disamount = subtotal * discounts / 100;
            var subtotals = subtotal - disamount;
            var taxamount1 = subtotals * intVal / 100;
            var total = taxamount1 + subtotals;
            salesorders.Subtotal = subtotals;
            salesorders.TaxValue = taxamount1;
            salesorders.Grandtotal = total;
            salesorders.discountvalue = disamount;

            var subtotal1 = (qty + 1) * unitprice;
            var disamount1 = subtotal1 * discounts / 100;
            var subtotals1 = subtotal1 - disamount1;
            var taxamounts = subtotals1 * intVal / 100;
            var total1 = taxamounts + subtotals1;

            var subvalue = subtotals1 - subtotals;
            var disvalue = disamount1 - disamount;
            var taxvalue = taxamounts - taxamount1;
            var totalvalue = total1 - total;

            salesorders.Total_discountval = model.Total_discountval + disvalue;
            salesorders.Total_Subtotal = model.Total_Subtotal + subvalue;
            salesorders.Total_TaxValue = model.Total_TaxValue + taxvalue;
            salesorders.Total_Grandtotal = model.Total_Grandtotal + totalvalue;

            return Json(salesorders, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Crud operation
        [HttpPost]
        public ActionResult EditDeliverychallanDetails(int SOId)
        {
            Salesorders models = new Salesorders();
            SalesorderHD_Manager salesorderManager = new SalesorderHD_Manager();
            SalesorderDT_Manager SalesorderDT_Manager = new SalesorderDT_Manager();
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            var headerdata = salesorderManager.GetSOId(SOId);
            var salesorderdt = SalesorderDT_Manager.GetSOIdS(SOId);
            var counts = 0;

            foreach( var i in salesorderdt)
            {
                if(i.quantity == i.dc_qty)
                {
                    counts++;
                }
            }
            var custadd = custAddressMangers.GetCustAddressbuyerid(headerdata.customerid);
            models.itemdc = counts;
            models.shippingadd = custadd.Add1;
            models.Billingadd = custadd.Add2;
            models.BuyerName = headerdata.customerid;
            models.SalesorderId = headerdata.salesorderid_hd;
            models.SalesorderId_HD = headerdata.salesorderid_hd;
            models.Total_Price = Math.Round((decimal)headerdata.Total_price, 2);
            models.salesorderdate = headerdata.Salesorderdate;
            models.quantity = headerdata.quantity;
            models.Final_Grandtotal = headerdata.grand_total;
            models.item = headerdata.items;
            List<Salesorders> totaldata = new List<Salesorders>();
            var totallist = SalesorderDT_Manager.salesorder_Grid();
            var filteredList = totallist.Where(i => i.salesorderid == SOId);

            foreach (var i in filteredList)
            {
                Salesorders salesorder = new Salesorders();
                if (i.dc_qty != null)
                {
                    var qtys = (i.quantity) - (i.dc_qty);
                    salesorder.quantity = qtys;
                    salesorder.SalesorderId = i.salesorderid;
                    salesorder.SalesorderId_DT = i.salesorderid_dt;
                    salesorder.discountperid = i.discountper;
                    salesorder.Price = i.unitprice;
                    salesorder.Taxper = i.Taxper;
                    salesorder.Uomname = i.long_unit_name;
                    salesorder.ProductName = i.productname;
                    salesorder.ProductCode = i.productcode;
                    salesorder.ProductID = i.productid;
                    salesorder.dc_qty = i.dc_qty;
                    var subtotal = qtys * i.unitprice;
                    var disamount = subtotal * i.discountper / 100;
                    var subtotals = subtotal - disamount;
                    var taxamount1 = subtotals * int.Parse(i.Taxper) / 100;
                    var total = taxamount1 + subtotals;
                    salesorder.Subtotal = subtotals;
                    salesorder.TaxValue = taxamount1;
                    salesorder.Grandtotal = total;
                    salesorder.discountvalue = disamount;
                }
                else
                {
                    salesorder.quantity = i.quantity;
                    salesorder.SalesorderId = i.salesorderid;
                    salesorder.SalesorderId_DT = i.salesorderid_dt;
                    salesorder.discountperid = i.discountper;
                    salesorder.Price = i.unitprice;
                    salesorder.Taxper = i.Taxper;
                    salesorder.Uomname = i.long_unit_name;
                    salesorder.ProductName = i.productname;
                    salesorder.ProductCode = i.productcode;
                    salesorder.ProductID = i.productid;
                    salesorder.dc_qty = i.dc_qty;

                    salesorder.discountvalue = i.discount_value;
                    salesorder.Subtotal = i.subtotal;
                    salesorder.TaxValue = i.taxvalue;
                    salesorder.Grandtotal = i.totalprice;

                }
                totaldata.Add(salesorder);
            }
            decimal? subtotals1 = 0;
            decimal? discount = 0;
            decimal? tax = 0;
            decimal? grandtotal = 0;

            foreach (var i in totaldata)
            {   
                subtotals1 += i.Subtotal;
                discount += i.discountvalue;
                tax += i.TaxValue;
                grandtotal += i.Grandtotal;
            }
            models.Total_Subtotal = Math.Round((decimal)subtotals1, 2);
            models.Total_discountval = Math.Round((decimal)discount, 2);
            models.Total_TaxValue = Math.Round((decimal)tax, 2);
            models.Total_Grandtotal = Math.Round((decimal)grandtotal, 2);

            models.salesorderLists = totaldata;

            return PartialView("Partial/DC_Details", models);
        }
        [HttpPost]
        public JsonResult DC_Post(string buyerid, string currencyOption, decimal Total_Price, decimal Total_Subtotal, decimal Total_TaxValue, decimal Total_discountval, decimal Total_Grandtotal, string salesOrderData)
        {
            var salesOrderList = JsonConvert.DeserializeObject<List<SalesOrderItem>>(salesOrderData);
            var AlertMessage = "";
            DeliveryChallanHD_Manager deliveryChallanHD_Manager = new DeliveryChallanHD_Manager();
            DeliveryChallanDt_Manager deliveryChallanDt_Manager = new DeliveryChallanDt_Manager();
            SalesorderManager salesorderManager = new SalesorderManager();
            CurrencyManager currencyManager = new CurrencyManager();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            BuyerManager buyerManager = new BuyerManager();
            Salesorder_dt salesorder_Dt = new Salesorder_dt();
            Salesorders model = new Salesorders();
            SalesorderDT_Manager salesorderDT_manager = new SalesorderDT_Manager();
            string dateOnly = DateTime.Now.ToString("yyyy-MM-dd");
            decimal? conversionval = 0;
            int id = 0;
            if (currencyOption.ToUpper() != "ZAR")
            {
                var ConversionValue = salesorderManager.Getcurrencyconversion("USD", "ZAR", dateOnly);
                conversionval = ConversionValue.conversionvalue;
                id = ConversionValue.id;
            }
            else
            {
                var ConversionValue = salesorderManager.Getcurrencyconversion("ZAR", "ZAR", dateOnly);
                conversionval = ConversionValue.conversionvalue;
                id = ConversionValue.id;
            }

            DeliveryChallan_hd DeliveryChallanHd = new DeliveryChallan_hd();
            DeliveryChallanHd.CustomerId = (Convert.ToInt32(buyerid));
            DeliveryChallanHd.DeliveryChallandate = DateTime.Now;
            DeliveryChallanHd.TotalPrice = Total_Price;
            DeliveryChallanHd.TotalSubtotal = Total_Subtotal;
            DeliveryChallanHd.TotalTaxAmount = Total_TaxValue;
            DeliveryChallanHd.TotalDisAmount = Total_discountval;
            DeliveryChallanHd.GrandTotal =Total_Grandtotal;
            DeliveryChallanHd.IsActive = true;
            decimal? quantity = 0;
            var count = 0;

            foreach (var i in salesOrderList)
            {
                if ((Convert.ToInt32(i.Quantity)) != 0)
                {
                    quantity += (Convert.ToInt32(i.Quantity));
                    count++;
                }
            }
            DeliveryChallanHd.ItemsDc = count;
            DeliveryChallanHd.Quantity = quantity;

            var headerid = deliveryChallanHD_Manager.POST(DeliveryChallanHd);

            DeliveryChallan_dt deliveryChallanDt = new DeliveryChallan_dt();
            var currencyid = currencyManager.GetContainCurrencyid(currencyOption);
            foreach (var i in salesOrderList)
            {
                var dc_dt = deliveryChallanDt_Manager.GetSOId(Convert.ToInt32(i.SalesorderId_DT));
                decimal? quantitys = 0;
                foreach (var k in dc_dt)
                {
                    if (k.Quantity != 0)
                    {
                        quantitys += (k.Quantity);
                        count++;
                    }
                }
                var DT = salesorderDT_manager.GetSO(Convert.ToInt32(i.SalesorderId_DT));
                var product = productManager.GetId(Convert.ToInt32(i.ProductID));
                var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
                var addreddcode = buyerManager.GetBuyerMasterId(Convert.ToInt32(buyerid));

                deliveryChallanDt.DCid_hd = headerid.DCid_hd;
                deliveryChallanDt.SalesOrderId_dt =(Convert.ToInt32(i.SalesorderId_DT));
                deliveryChallanDt.ProductId = (Convert.ToInt32(i.ProductID));
                deliveryChallanDt.ProductCode = product.ProductCode;
                deliveryChallanDt.CustomerId = (Convert.ToInt32(buyerid));
                deliveryChallanDt.UomMasterId = product.UomMasterId;
                deliveryChallanDt.TaxPerId = product.TaxMasterId;
                deliveryChallanDt.Quantity = (Convert.ToInt32(i.Quantity));
                deliveryChallanDt.DiscountPer = DT.Discountperid;
                deliveryChallanDt.UnitPrice = product.Price;
                deliveryChallanDt.DCDate = DateTime.Now;
                deliveryChallanDt.CurrencyId = currencyid.id;
                deliveryChallanDt.CustAddCode = addreddcode.BuyerCode;
                deliveryChallanDt.CustBillCode = addreddcode.BuyerAddress1;
                deliveryChallanDt.CustShipCode = addreddcode.BuyerAddress2;
                var unitprice = product.Price * conversionval;
                deliveryChallanDt.IsActive = true;
                var taxper = tax.TaxValue;
                var qty = (Convert.ToInt32(i.Quantity));
                var discount = DT.Discountperid;
                int intVal = int.Parse(taxper);

                if ((qty != null) && (discount != null))
                {
                    var subtotal = qty * unitprice;
                    var disamount = subtotal * discount / 100;
                    var subtotals = subtotal - disamount;
                    var taxamount1 = subtotals * intVal / 100;
                    var total = taxamount1 + subtotals;
                    deliveryChallanDt.SubTotal = subtotals;
                    deliveryChallanDt.TaxValue = taxamount1;
                    deliveryChallanDt.TotalPrice = total;
                    deliveryChallanDt.DiscountValue = disamount;
                }
                if ((Convert.ToInt32(i.Quantity)) != 0 && dc_dt != null)
                {
                    salesorder_Dt.dc_qty = (Convert.ToInt32(i.Quantity)) + quantitys;
                    salesorder_Dt.Salesorderid_dt = (Convert.ToInt32(i.SalesorderId_DT));

                    deliveryChallanDt_Manager.POST(deliveryChallanDt);
                    salesorderDT_manager.Put(salesorder_Dt);
                }
                else if ((Convert.ToInt32(i.Quantity)) != 0)
                {
                    salesorder_Dt.dc_qty = (Convert.ToInt32(i.Quantity)) ;
                    salesorder_Dt.Salesorderid_dt = (Convert.ToInt32(i.SalesorderId_DT));

                    deliveryChallanDt_Manager.POST(deliveryChallanDt);
                    salesorderDT_manager.Put(salesorder_Dt);
                    AlertMessage = "Confirm Order";
                }
            }

            return Json(new { AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Filter
        public ActionResult Search(int customerid, int SOid)
        {
            SalesorderHD_Manager salesorderManager = new SalesorderHD_Manager();
            BuyerManager BuyerManager = new BuyerManager();
            SalesorderDT_Manager SalesorderDT_Manager = new SalesorderDT_Manager();
            var data1 = BuyerManager.Get();
            List<Salesorders> totalList = new List<Salesorders>();
            var data = salesorderManager.Get();
            foreach (var i in data)
            {
                Salesorders model = new Salesorders();
                var salesorderdt = SalesorderDT_Manager.GetSOIdS(i.salesorderid_hd);
                var counts = 0;
                var count = 0;
                foreach (var k in salesorderdt)
                {
                    if (k.quantity == k.dc_qty)
                    {
                        counts++;
                    }
                }
                foreach (var j in salesorderdt)
                {
                    if (j.quantity == j.Invoice_qty)
                    {
                        count++;
                    }
                }
                model.itemInvoiced = count;
                model.itemdc = counts;
                model.SalesorderId = i.salesorderid_hd;
                model.salesorderdate = i.Salesorderdate;
                model.item = i.items;
                model.quantity = i.quantity;
                model.Total_Price = i.Total_price;
                model.Total_discountval = i.Total_disamount;
                model.Total_Grandtotal = i.grand_total;
                model.Total_Subtotal = i.Total_subtotal;
                model.Total_TaxValue = i.Total_taxamount;
                model.BuyerMaster = data1.Where(W => W.BuyerMasterId == i.customerid).ToList().FirstOrDefault();
                totalList.Add(model);
            }
            var filteredList = totalList.Where(J => J.buyerid == customerid || J.SalesorderId == SOid);

            return Json(filteredList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Getbuyerorderno(int id)
        {
            SalesorderHD_Manager SalesorderHD_Manager = new SalesorderHD_Manager();
            var data = SalesorderHD_Manager.GettypeId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
