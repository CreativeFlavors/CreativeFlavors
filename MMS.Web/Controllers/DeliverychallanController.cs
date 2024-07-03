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
        public ActionResult DeliverychallanGrid(int page = 1, int pageSize = 5)
        {
            SalesorderHD_Manager salesorderManager = new SalesorderHD_Manager();
            BuyerManager BuyerManager = new BuyerManager();
            var data1 = BuyerManager.Get();
            List<Salesorders> totalList = new List<Salesorders>();
            var data = salesorderManager.Get();
            foreach (var i in data)
            {
                Salesorders model = new Salesorders();
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
            if (model.currencyOption.ToUpper() != "ZAR")
            {
                model.ConversionValue = salesorderManager.Getcurrencyconversion("ZAR", "USD", dateOnly);
            }
            else
            {
                model.ConversionValue = 1;
            }
            var product = productManager.GetId(model.ProductID);
            var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
            var taxper = tax.TaxValue;
            var qty = model.quantity;
            var discount = model.discountval;
            var discounts = Convert.ToDecimal(discount);
            var unitprice = product.Price * model.ConversionValue;
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
            if (model.currencyOption.ToUpper() != "ZAR")
            {
                model.ConversionValue = salesorderManager.Getcurrencyconversion("ZAR", "USD", dateOnly);
            }
            else
            {
                model.ConversionValue = 1;
            }
            var product = productManager.GetId(model.ProductID);
            var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
            var taxper = tax.TaxValue;
            var qty = model.quantity;
            var discount = model.discountval;
            var discounts = Convert.ToDecimal(discount);
            var unitprice = product.Price * model.ConversionValue;
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
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            var headerdata = salesorderManager.GetSOId(SOId);
            var custadd = custAddressMangers.GetCustAddressbuyerid(headerdata.customerid);
            models.shippingadd = custadd.Add1;
            models.Billingadd = custadd.Add2;
            models.BuyerName = headerdata.customerid;
            models.SalesorderId = headerdata.salesorderid_hd;
            models.SalesorderId_HD = headerdata.salesorderid_hd;
            models.Total_Price = headerdata.Total_price;
            models.salesorderdate = headerdata.Salesorderdate;
            models.Total_Grandtotal = headerdata.grand_total;
            models.quantity = headerdata.quantity;
            models.Total_discountval = headerdata.Total_disamount;
            models.Total_TaxValue = headerdata.Total_taxamount;
            models.Total_Subtotal = headerdata.Total_subtotal;
            models.item = headerdata.items;
            SalesorderDT_Manager SalesorderDT_Manager = new SalesorderDT_Manager();
            List<Salesorders> totaldata = new List<Salesorders>();
            var totallist = SalesorderDT_Manager.salesorder_Grid();

            var filteredList = totallist.Where(i => i.salesorderid == SOId);

            foreach (var i in filteredList)
            {
                Salesorders salesorder = new Salesorders();
                salesorder.SalesorderId = i.salesorderid;
                salesorder.SalesorderId_DT = i.salesorderid_dt;
                salesorder.quantity = i.quantity;
                salesorder.discountperid = i.discountper;
                salesorder.discountvalue = i.discount_value;
                salesorder.Price = i.unitprice;
                salesorder.Subtotal = i.subtotal;
                salesorder.TaxValue = i.taxvalue;
                salesorder.Taxper = i.Taxper;
                salesorder.Grandtotal = i.totalprice;
                salesorder.Uomname = i.long_unit_name;
                salesorder.ProductName = i.productname;
                salesorder.ProductCode = i.productcode;
                salesorder.ProductID = i.productid;

                totaldata.Add(salesorder);
            }

            models.salesorderLists = totaldata;

            return PartialView("Partial/DC_Details", models);
        }
        [HttpPost]
        public JsonResult DC_Post(string buyerid, string SalesorderId_HD, string currencyOption, decimal Total_Price, decimal Total_Subtotal, decimal Total_TaxValue, decimal Total_discountval, decimal Total_Grandtotal, string salesOrderData)
        {
            var salesOrderList = JsonConvert.DeserializeObject<List<SalesOrderItem>>(salesOrderData);

            var AlertMessage = "";

            SalesorderManager salesorderManager = new SalesorderManager();
            SalesorderHD_Manager salesorderHD_Manager = new SalesorderHD_Manager();
            SalesorderDT_Manager salesorderDT = new SalesorderDT_Manager();
            CurrencyManager currencyManager = new CurrencyManager();
            salesorder salesorder = new salesorder();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            BuyerManager buyerManager = new BuyerManager();
            Salesorders model = new Salesorders();
            SalesorderDT_Manager salesorderDT_manager = new SalesorderDT_Manager();
            string dateOnly = DateTime.Now.ToString("yyyy-MM-dd");
            if (currencyOption.ToUpper() != "ZAR")
            {
                model.ConversionValue = salesorderManager.Getcurrencyconversion("ZAR", "USD", dateOnly);
            }
            else
            {
                model.ConversionValue = 1;
            }
            Salesorder_hd salesorder_Hd = new Salesorder_hd();
            salesorder_Hd.customerid = (Convert.ToInt32(buyerid));
            salesorder_Hd.items = salesOrderList.Count();
            salesorder_Hd.Salesorderdate = DateTime.Now;
            salesorder_Hd.Total_price = Total_Price;
            salesorder_Hd.Total_subtotal = Total_Subtotal;
            salesorder_Hd.Total_taxamount = Total_TaxValue;
            salesorder_Hd.Total_disamount = Total_discountval;
            salesorder_Hd.grand_total =Total_Grandtotal;
            salesorder_Hd.isactive = true;
            decimal? quantity = 0;

            foreach (var i in salesOrderList)
            {
                quantity += (Convert.ToInt32(i.Quantity));
            }
            salesorder_Hd.quantity = quantity;

            //var headerid = salesorderHD_Manager.POST(salesorder_Hd);

            foreach (var i in salesOrderList)
            {
                var DT = salesorderDT_manager.GetSO(Convert.ToInt32(i.SalesorderId_DT));
                var product = productManager.GetId(Convert.ToInt32(i.ProductID));
                var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
                var addreddcode = buyerManager.GetBuyerMasterId(Convert.ToInt32(buyerid));

                salesorder.ProductNameid = (Convert.ToInt32(i.ProductID));
                salesorder.ProductCode = product.ProductCode;
                salesorder.customerid = (Convert.ToInt32(buyerid));
                salesorder.UomMasterId = product.UomMasterId;
                salesorder.Taxperid = product.TaxMasterId;
                salesorder.quantity = (Convert.ToInt32(i.Quantity));
                salesorder.Discountperid = DT.Discountperid;
                salesorder.Price = product.Price;
                salesorder.quotedate = DateTime.Now;
                salesorder.salesorderdate = DateTime.Now;
                salesorder.originalquotedate = DateTime.Now;
                salesorder.custaddcode = addreddcode.BuyerCode;
                salesorder.custbillcode = addreddcode.BuyerAddress1;
                salesorder.custshipcode = addreddcode.BuyerAddress2;
                var unitprice = product.Price * model.ConversionValue;
                salesorder.taxinclusive = true;
                salesorder.isactive = true;
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
                    salesorder.Subtotal = subtotals;
                    salesorder.TaxValue = taxamount1;
                    salesorder.Grandtotal = total;
                    salesorder.Discountvalue = disamount;
                }
                if(DT.quantity != qty)
                {
                    //salesorder = salesorderManager.Post(salesorder);
                }
            }

            AlertMessage = "Confirm Order";
            return Json(new { AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
