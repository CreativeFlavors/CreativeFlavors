using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Web.Models.InvoiceDetails;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.Addressdetails;
using MMS.Repository.Managers;
using static iTextSharp.text.pdf.AcroFields;
using MMS.Web.Models;
using Newtonsoft.Json;

namespace MMS.Web.Controllers.Stock
{
    public class InvoiceDetailsController : Controller
    {
        #region Accounts View
        [HttpPost]
        public ActionResult InvoiceDetails(int SOId)
        {
            Salesorders models = new Salesorders();
            SalesorderHD_Manager salesorderManager = new SalesorderHD_Manager();
            SalesorderDT_Manager SalesorderDT_Manager = new SalesorderDT_Manager();
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            CurrencyManager currencyManager = new CurrencyManager();
            var headerdata = salesorderManager.GetSOId(SOId);
            var salesorderdt = SalesorderDT_Manager.GetSOIdS(SOId);
            var counts = 0;

            foreach (var i in salesorderdt)
            {
                if (i.quantity == i.Invoice_qty)
                {
                    counts++;
                }
            }
            var custadd = custAddressMangers.GetCustAddressbuyerid(headerdata.customerid);
            var ConversionValue = currencyManager.GetCurrencycunversion().Where(m => m.Id == headerdata.currencyid).FirstOrDefault();
            models.itemInvoiced = counts;
            models.BuyerName = headerdata.customerid;
            models.SalesorderId = headerdata.salesorderid_hd;
            models.SalesorderId_HD = headerdata.salesorderid_hd;
            models.Total_Price = Math.Round((decimal)headerdata.Total_price, 2);
            models.salesorderdate = headerdata.Salesorderdate;
            models.quantity = headerdata.quantity;
            models.currencyOption = headerdata.currencyid;
            models.Final_Grandtotal = headerdata.grand_total;
            models.item = headerdata.items;
            List<Salesorders> totaldata = new List<Salesorders>();
            var totallist = SalesorderDT_Manager.salesorder_Grid();
            var filteredList = totallist.Where(i => i.salesorderid == SOId);

            foreach (var i in filteredList)
            {
                Salesorders salesorder = new Salesorders();
                if (i.invoiceqty != null)
                {
                    var qtys = (i.quantity) - (i.invoiceqty);
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
                    salesorder.invoice_qty = i.invoiceqty;
                    var subtotal = qtys * i.unitprice * ConversionValue.ConversionValue;
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
                    salesorder.invoice_qty = i.invoiceqty;

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
            return PartialView("Partial/InvoiceDetails", models);
        }
        public ActionResult Dec_Calculation_Indetails(Salesorders model)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            Salesorders salesorders = new Salesorders();
            CurrencyManager currencyManager = new CurrencyManager();
            string dateOnly = DateTime.Now.ToString("yyyy-MM-dd");
            decimal? conversionval = 0;
            int id = 0;
            if (model.currencyOption != 0)
            {
                var ConversionValue = currencyManager.GetCurrencycunversion().Where(m => m.Id == model.currencyOption).FirstOrDefault();
                conversionval = ConversionValue.ConversionValue;
                id = ConversionValue.Id;
            }
            else
            {
                conversionval = 1;
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
        public ActionResult Inc_Calculation_Indetails(Salesorders model)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            Salesorders salesorders = new Salesorders();
            CurrencyManager currencyManager = new CurrencyManager();

            string dateOnly = DateTime.Now.ToString("yyyy-MM-dd");
            decimal? conversionval = 0;
            int id = 0;
            if (model.currencyOption != 0)
            {
                var ConversionValue = currencyManager.GetCurrencycunversion().Where(m => m.Id == model.currencyOption).FirstOrDefault();
                conversionval = ConversionValue.ConversionValue;
                id = ConversionValue.Id;
            }
            else
            {
                conversionval = 1;
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
        [HttpPost]
        public JsonResult Invoice_Post(string buyerid, int currencyOption, decimal Total_Price, decimal Total_Subtotal, decimal Total_TaxValue, decimal Total_discountval, decimal Total_Grandtotal, string salesOrderData)
        {
            var salesOrderList = JsonConvert.DeserializeObject<List<SalesOrderItem>>(salesOrderData);
            var AlertMessage = "";
            OrderHeaderManager OrderHeaderManager = new OrderHeaderManager();
            OrderDetailsManager OrderDetailsManager = new OrderDetailsManager();
            SalesorderManager salesorderManager = new SalesorderManager();
            CurrencyManager currencyManager = new CurrencyManager();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            BuyerMasterManager buyerManager = new BuyerMasterManager();
            Salesorder_dt salesorder_Dt = new Salesorder_dt();
            Salesorders model = new Salesorders();
            SalesorderDT_Manager salesorderDT_manager = new SalesorderDT_Manager();
            string dateOnly = DateTime.Now.ToString("yyyy-MM-dd");
            decimal? conversionval = 0;
            int id = 0;
            if (currencyOption != 0)
            {
                var ConversionValue = currencyManager.GetCurrencycunversion().Where(m => m.Id == currencyOption).FirstOrDefault();
                conversionval = ConversionValue.ConversionValue;
                id = ConversionValue.Id;
            }
            else
            {
                //var ConversionValue = salesorderManager.Getcurrencyconversion("ZAR", "ZAR", dateOnly);
                //conversionval = ConversionValue.conversionvalue;
                //id = ConversionValue.id;
                conversionval = 1;
            }

            orderheader_hd orderheader = new orderheader_hd();
            orderheader.CustomerId = (Convert.ToInt32(buyerid));
            orderheader.invoicedate = DateTime.Now;
            orderheader.TotalPrice = Total_Price;
            orderheader.currencyid = currencyOption;
            orderheader.TotalSubtotal = Total_Subtotal;
            orderheader.TotalTaxAmount = Total_TaxValue;
            orderheader.TotalDisAmount = Total_discountval;
            orderheader.GrandTotal = Total_Grandtotal;
            orderheader.IsActive = true;
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
            orderheader.invoice_items = count;
            orderheader.Quantity = quantity;

            var headerid = OrderHeaderManager.POST(orderheader);

            orderdetails orderdetails = new orderdetails();
            var invoisedcount = salesOrderList.Count();
            var nowinvoised = 0;
            foreach (var i in salesOrderList)
            {
                var dc_dt = OrderDetailsManager.GetSOId(Convert.ToInt32(i.SalesorderId_DT));
                decimal? quantitys = 0;
                foreach (var k in dc_dt)
                {
                    if (k.Quantity != 0)
                    {
                        quantitys += (k.Quantity);
                        count++;
                    }
                }
                CustAddressMangers custAddressMangers = new CustAddressMangers();

                var DT = salesorderDT_manager.GetSO(Convert.ToInt32(i.SalesorderId_DT));
                var product = productManager.GetId(Convert.ToInt32(i.ProductID));
                var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
                int buyer11 = Convert.ToInt32(buyerid);
                var addreddcode = buyerManager.GetBuyerMasterId(buyer11);

                var shipping = custAddressMangers.GetCustAddressbuyeridshipp(model.buyerid);
                var billing = custAddressMangers.GetCustAddressbuyerid(model.buyerid);
                orderdetails.invoicehd_id = headerid.invoicehd_id;
                orderdetails.SalesOrderId_dt = (Convert.ToInt32(i.SalesorderId_DT));
                orderdetails.ProductId = (Convert.ToInt32(i.ProductID));
                orderdetails.ProductCode = product.ProductCode;
                orderdetails.CustomerId = (Convert.ToInt32(buyerid));
                orderdetails.UomMasterId = product.UomMasterId;
                orderdetails.TaxPerId = product.TaxMasterId;
                orderdetails.Quantity = (Convert.ToInt32(i.Quantity));
                orderdetails.DiscountPer = DT.Discountperid;
                orderdetails.UnitPrice = product.Price;
                orderdetails.invoicedate = DateTime.Now;
                orderdetails.CurrencyId = currencyOption;
                orderdetails.CustAddCode = addreddcode.BuyerCode;
                orderdetails.CustBillCode = billing.Addresshd_id.ToString();
                orderdetails.CustShipCode = shipping.Addresshd_id.ToString();
                var unitprice = product.Price * conversionval;
                orderdetails.IsActive = true;
                var taxper = tax.TaxValue;
                var qty = (Convert.ToInt32(i.Quantity));
                var discount = DT.Discountperid;
                int intVal = int.Parse(taxper);
                if (qty != 0)
                {
                    if ((qty != null) && (discount != null))
                    {
                        var subtotal = qty * unitprice;
                        var disamount = subtotal * discount / 100;
                        var subtotals = subtotal - disamount;
                        var taxamount1 = subtotals * intVal / 100;
                        var total = taxamount1 + subtotals;
                        orderdetails.SubTotal = subtotals;
                        orderdetails.TaxValue = taxamount1;
                        orderdetails.TotalPrice = total;
                        orderdetails.DiscountValue = disamount;
                    }
                    if ((Convert.ToInt32(i.Quantity)) != 0 && dc_dt != null)
                    {
                        salesorder_Dt.Invoice_qty = (Convert.ToInt32(i.Quantity)) + quantitys;
                        salesorder_Dt.Salesorderid_dt = (Convert.ToInt32(i.SalesorderId_DT));
                        AlertMessage = "Confirm Order";
                        OrderDetailsManager.POST(orderdetails);
                        salesorderDT_manager.update(salesorder_Dt);
                    }
                    else if ((Convert.ToInt32(i.Quantity)) != 0)
                    {
                        salesorder_Dt.Invoice_qty = (Convert.ToInt32(i.Quantity));
                        salesorder_Dt.Salesorderid_dt = (Convert.ToInt32(i.SalesorderId_DT));
                        AlertMessage = "Confirm Order";
                        OrderDetailsManager.POST(orderdetails);
                        salesorderDT_manager.update(salesorder_Dt);
                    }
                }
                else
                {
                    nowinvoised++;
                }

            }
            if (nowinvoised == invoisedcount)
            {
                AlertMessage = "Full Invoised";
                return Json(AlertMessage, JsonRequestBehavior.AllowGet);
            }
            return Json(AlertMessage, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
