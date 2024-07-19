using iTextSharp.text;
using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models;
using MMS.Web.Models.Addressdetails;
using MMS.Web.Models.GRNModel;
using MMS.Web.Models.IndentMaterial;
using MMS.Web.Models.SupplierMasterModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class GRNController : Controller
    {
        #region View
        [HttpGet]
        public ActionResult GRNMaster()
        {
                GRNCartManager gRNCartManager = new GRNCartManager();
                gRNCartManager.Putcancelsuccess();
            return View();
        }
        public ActionResult GRNGrid(int page = 1, int pageSize = 15)
        {
            GRNModel model = new GRNModel();
            GRNHeaderManager headerManager = new GRNHeaderManager();
            SupplierMasterManager supplierMaster = new SupplierMasterManager();
            var totaldata = (from d in headerManager.Get()
                             join d1 in supplierMaster.Get() on d.SupplierId equals d1.SupplierMasterId
                             select new GRNModel
                             {
                                 GrnDate = d.GrnDate,
                                 RefInvoiceNumber = d.RefInvoiceNumber,
                                 item=d.Items,
                                 Quantity = d.Quantity,
                                 UnitPrice=d.total_unitprice,
                                 SubTotal=d.SubtotalValue,
                                 TaxValue=d.TaxValue,
                                 DiscountValue=d.DiscountValue,
                                 TotalValue=d.TotalValue,
                                 SupplierMaster = new SupplierMaster
                                 {
                                     SupplierName = d1.SupplierName,
                                 },

                             }).ToList();
            var totalCount = totaldata.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totaldata = totaldata.OrderByDescending(s => s.GrnHeaderId)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;


            return PartialView("~/Views/GRN/Partial/GRNGrid.cshtml", totaldata);
        }
        public ActionResult GRNDetails(int? POno)
        {
            GRNModel model = new GRNModel();
            GRNHeaderManager GRNHeaderManager = new GRNHeaderManager();

            if (POno == null)
            {
                model.GrnDate = DateTime.Today;
                int intentno = GRNHeaderManager.GetNextGRNNumberFromDatabase();
                model.GrnHeaderId = intentno;
            }
            return View("~/Views/GRN/Partial/GRN_Details.cshtml",model);
        }
        public ActionResult GetPODetails(int POno)
        {
            GRNModel model = new GRNModel();
            PoManager poManager = new PoManager();
            GRNCartManager GRNCartManager = new GRNCartManager();
            var POList = poManager.GetPODetails();
            var POHeaderlist= poManager.Get().Where(m=>m.PoheaderId == POno).FirstOrDefault();
            var totalist = POList.Where(m=>m.poheader==POno).ToList();
            model.Total_Price = POHeaderlist.TotalPrice;
            model.Total_discountval = POHeaderlist.TotalDiscountValue;
            model.Total_Subtotal = POHeaderlist.TotalSubtotalValue;
            model.Total_TaxValue = POHeaderlist.TotalTaxValue;
            model.Total_Grandtotal = POHeaderlist.TotalTotalValue;

            model.polist = totalist;

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GRNcART(int poheaderid)
        {
            GRNModel model = new GRNModel();
            GRNCartManager GRNCartManager = new GRNCartManager();
            var list = GRNCartManager.GetGRNCsrtList(poheaderid);
            if (list != null)
            {
                model.grnCarlist = list;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region crud operation
        [HttpPost]
        public ActionResult GRNCartDetails(GRNModel model)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            PoManager PoManager = new PoManager();
            GRNCartManager gRNCartManager = new GRNCartManager();
            GRNCart GRNCart = new GRNCart();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            BuyerManager buyerManager = new BuyerManager();

            string dateOnly = DateTime.Now.ToString("yyyy-MM-dd");
            decimal? conversionval = 0;
            int id = 0;int forid = 0;
            if (model.currencyOption.ToUpper() != "ZAR")
            {
                var ConversionValue = salesorderManager.Getcurrencyconversion("USD","ZAR", dateOnly);
                conversionval = ConversionValue.conversionvalue;
                forid = ConversionValue.id;
            }
            else
            {
                var ConversionValue = salesorderManager.Getcurrencyconversion("ZAR", "ZAR", dateOnly);
                conversionval = ConversionValue.conversionvalue;
                id = ConversionValue.id;
            }

            var product = productManager.GetId(model.ProductNameId);
            var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
            var podetails= PoManager.Getdetails().Where(x => x.PodetailId == model.PoDetailId).FirstOrDefault();
            GRNCart.ProductNameId = model.ProductNameId;
            GRNCart.ProductCode = product.ProductCode;
            GRNCart.UomMasterId = product.UomMasterId;
            GRNCart.UomMasterId = product.UomMasterId;
            GRNCart.TaxPerId = product.TaxMasterId;
            GRNCart.Quantity = model.Quantity;
            GRNCart.DiscountPerId = podetails.DiscountPercentage;
            GRNCart.unitprice = model.UnitPrice;
            GRNCart.podetailid = model.PoDetailId;
            GRNCart.poheaderid = model.PoHeaderId;
            GRNCart.poquantity = model.PoQuantity;
            GRNCart.Grndate = DateTime.Now;
            GRNCart.ExpiryDate = model.ExpiryDate;
            GRNCart.BatchCode = model.BatchCode;
            GRNCart.StoreCode = podetails.StoreCode;
            GRNCart.unitprice =model.UnitPrice;
            GRNCart.poquantity=podetails.Quantity;
            GRNCart.currencyconid = id;
            var unitprice = model.UnitPrice * conversionval;
            GRNCart.IsActive = true;
            var taxper = tax.TaxValue;
            var qty = model.Quantity;
            var discount = podetails.DiscountPercentage;
            int intVal = int.Parse(taxper);
            string AlertMessage = "";
            if (model.Quantity != null)
            {
                var subtotal = qty * unitprice;
                var disamount = subtotal * discount / 100;
                var subtotals = subtotal - disamount;
                var taxamount1 = subtotals * intVal / 100;
                var total = taxamount1 + subtotals;
                GRNCart.Subtotal = subtotals;
                GRNCart.TaxValue = taxamount1;
                GRNCart.Grandtotal = total;
                GRNCart.DiscountValue = disamount;
            }
            if (model.currencyOption.ToUpper() == "ZAR")
            {
                var ConversionValue = salesorderManager.Getcurrencyconversion("ZAR", "ZAR", dateOnly);
                conversionval = ConversionValue.conversionvalue;
                forid = ConversionValue.id;

                var unitpriceS = model.UnitPrice * conversionval;
                var subtotal = qty * unitpriceS;
                var taxamount = subtotal * intVal / 100;
                var totalprice = taxamount + subtotal;
                var disamount = subtotal * discount / 100;
                GRNCart.ForSubtotalValue = subtotal;
                GRNCart.ForTaxValue = taxamount;
                GRNCart.ForTotalValue = totalprice;
                GRNCart.ForDiscountValue = disamount;
                GRNCart.for_currencyconid = forid;
                GRNCart.for_totalunitprice = unitpriceS;
            } 
            else if (model.currencyOption.ToUpper() == "USD")
            {
                var ConversionValue = salesorderManager.Getcurrencyconversion("USD", "ZAR", dateOnly);
                conversionval = ConversionValue.conversionvalue;
                id = ConversionValue.id;

                var unitpriceS = model.UnitPrice * conversionval;
                var subtotal = qty * unitpriceS;
                var taxamount = subtotal * intVal / 100;
                var totalprice = taxamount + subtotal;
                var disamount = subtotal * discount / 100;
                GRNCart.ForSubtotalValue = subtotal;
                GRNCart.ForTaxValue = taxamount;
                GRNCart.ForTotalValue = totalprice;
                GRNCart.ForDiscountValue = disamount;
                GRNCart.for_currencyconid = id;
                GRNCart.for_totalunitprice = unitpriceS;

            }
            GRNCartManager GRNCartManager = new GRNCartManager();

            var list = GRNCartManager.Getgrncartdt(model.PoDetailId);
            if (list != null )
            {
                decimal? dt_qty = 0;

                    if (list.Quantity != null)
                    {
                    dt_qty = list.Quantity + model.Quantity;
                    }
                GRNCart.Quantity = dt_qty;
                GRNCart.BatchCode=model.BatchCode;
                GRNCart.ExpiryDate = model.ExpiryDate;
                GRNCart = gRNCartManager.Put(GRNCart);
            }
            else
            {
                GRNCart = gRNCartManager.POST(GRNCart);
            }
            var grnqty =  GRNCart.poquantity- GRNCart.Quantity ;
            decimal? unitprices = GRNCart.unitprice;
            AlertMessage = "Added Successfully";
            return Json(new { Grnqty= grnqty, Unitprices= unitprices, AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ConfirmGRN(GRNModel model)
        {
            var AlertMessage = "";
            PoManager PoManager = new PoManager();
            BatchStockManager batchStockManager = new BatchStockManager();
            GRNCartManager GrnManager = new GRNCartManager();
            GRNHeaderManager GRNHeaderManager = new GRNHeaderManager();
            GRNDetailsManager GRNDetailsManager = new GRNDetailsManager();
            CurrencyManager currencyManager = new CurrencyManager();
            var grnlist = GrnManager.GetgrncartHD(model.PoHeaderId);
            var count = grnlist.Count;
            if (count <= 0)
            {
                AlertMessage = "Not Existed";
                return Json(new { AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var podetails = PoManager.Getdetails().Where(x => x.PoheaderId == model.PoHeaderId).FirstOrDefault();
                var currencyids = currencyManager.GetContainCurrencyid(model.currencyOption);
                GRNHeader GRNHeader = new GRNHeader();
                GRNHeader.Items =grnlist.Count();
                GRNHeader.GrnDate = DateTime.Now;
                GRNHeader.PoNumber = model.PoHeaderId;
                GRNHeader.total_unitprice = model.Total_Price;
                GRNHeader.SupplierId = model.SupplierId;
                GRNHeader.DiscountValue = model.Total_discountval;
                GRNHeader.PoDate = podetails.PoDate;
                GRNHeader.TaxValue = model.Total_TaxValue;
                GRNHeader.DiscountValue = model.Total_discountval;
                GRNHeader.SubtotalValue = model.Total_Subtotal;
                GRNHeader.TotalValue = model.Total_Grandtotal;
                GRNHeader.IsFulfilled = DateTime.Now;
                GRNHeader.ShipmentDetails = model.ShipmentDetails;
                GRNHeader.Notes = model.Notes;
                GRNHeader.OverallWeight = model.OverallWeight;
                GRNHeader.RefInvoiceDate = model.RefInvoiceDate;
                GRNHeader.RefInvoiceNumber=model.RefInvoiceNumber;
                GRNHeader.currencyid = currencyids.id;
                decimal? quantity = 0;
                decimal? taxvalue = 0;
                decimal? grandtotal = 0;
                decimal? unitprice = 0;
                decimal? subtotal = 0;
                decimal? discountval = 0;
                foreach (var i in grnlist)
                {
                    quantity += i.Quantity;
                    taxvalue += i.ForTaxValue;
                    grandtotal += i.ForTotalValue;
                    unitprice += i.for_totalunitprice;
                    subtotal += i.ForSubtotalValue;
                    discountval += i.ForDiscountValue;
                    
                }
                GRNHeader.Quantity = quantity;
                GRNHeader.for_totalunitprice = unitprice;
                GRNHeader.ForTotalValue = grandtotal;
                GRNHeader.ForSubtotalValue = subtotal;
                GRNHeader.ForTaxValue = taxvalue;
                GRNHeader.ForDiscountValue = discountval;
                var headerid = GRNHeaderManager.POST(GRNHeader);

                foreach (var i in grnlist)
                {
                    var currencyid = currencyManager.GetContainCurrencyid(model.currencyOption);
                    GRNDetails GRNDetails = new GRNDetails();
                    GRNDetails.GrnHeaderId = headerid.GrnHeaderId;
                    GRNDetails.SupplierId = headerid.SupplierId;
                    GRNDetails.productid = i.ProductNameId;
                    GRNDetails.PoDetailId = i.podetailid;
                    GRNDetails.Pounitprice = podetails.UnitPrice;
                    GRNDetails.PoQuantity = i.poquantity;
                    GRNDetails.UnitPrice = i.unitprice;
                    GRNDetails.TaxValue = i.TaxValue;
                    GRNDetails.Quantity = i.Quantity;
                    GRNDetails.SubtotalValue = i.Subtotal;
                    GRNDetails.TaxValue = i.TaxValue;
                    GRNDetails.DiscountValue = i.DiscountValue;
                    GRNDetails.TotalValue = i.Grandtotal;
                    GRNDetails.ExpiryDate = i.ExpiryDate;
                    GRNDetails.BatchCode = i.BatchCode;
                    GRNDetails.StoreCode = i.StoreCode;
                    GRNDetails.Weight =model.Weight;
                    GRNDetails.currencyid = currencyid.id;
                    GRNDetails.currencyconid = i.currencyconid;
                    GRNDetails.for_currencyconid = i.for_currencyconid;
                    GRNDetails.IsFulfilled = DateTime.Now;
                    GRNDetails.OverallWeight = model.OverallWeight;
                    GRNDetails.ForDiscountValue = i.ForDiscountValue;
                    GRNDetails.for_totalunitprice = i.for_totalunitprice;
                    GRNDetails.ForSubtotalValue = i.ForSubtotalValue;
                    GRNDetails.ForTaxValue = i.ForTaxValue;
                    GRNDetails.ForTotalValue = i.ForTotalValue;
                    GRNDetails.Status = "1";
                    GRNDetails.IsFulfilled = DateTime.Now;
                    var data = GRNDetailsManager.POST(GRNDetails);
                    BatchStock batchStock = new BatchStock();
                    batchStock.SupplierId= headerid.SupplierId;
                    batchStock.StoreCode = i.StoreCode;
                    batchStock.productid = i.ProductNameId;
                    batchStock.BatchCode = i.BatchCode;
                    batchStock.AltBatchCode = i.BatchCode;
                    batchStock.ExpiryDate = i.ExpiryDate;
                    batchStock.Quantity = i.Quantity;
                    batchStock.GrnNumber = headerid.GrnHeaderId;
                    batchStock.GrnDate = headerid.GrnDate;
                    batchStock.GrnDetailId = data.GrnDetailId;
                    batchStock.Price = i.Grandtotal;
                    batchStock.Cost = 0;
                    batchStock.TaxCode = i.TaxPerId;
                    batchStock.UomId = i.UomMasterId;
                    batchStock.producttype = 3;
                    batchStockManager.POST(batchStock);
                }
                var bOMMaterial = GrnManager.Putstatussuccess();


                AlertMessage = "Confirm Order";
                return Json(new { AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpDelete]
        public ActionResult SODelete(int SOId)
        {
            GRNCartManager GRNCartManager = new GRNCartManager();
            string AlertMessage = "";
            GRNCart GRNCart = new GRNCart();
            GRNCart = GRNCartManager.GetSO(SOId);
            GRNModel model = new GRNModel();
            var list = new List<MMS.Data.StoredProcedureModel.GRNCarlist>();
            if (GRNCart != null)
            {
                AlertMessage = "Success";
                var delete=GRNCartManager.Delete(GRNCart.grncartid);
                 list = GRNCartManager.GetGRNCsrtList(delete.poheaderid);
            }

            if (list != null)
            {
                model.grnCarlist = list;
            }
            return Json(new { AlertMessage = AlertMessage, model}, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Calculation
        [HttpPost]
        public ActionResult Dec_Calculationdetails(GRNModel model)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            GRNModel GRNModel = new GRNModel();
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
                     conversionval= ConversionValue.conversionvalue;
                    id= ConversionValue.id;
            }


            var product = productManager.GetId(model.ProductNameId);
            var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);

            var taxper = tax.TaxValue;
            var qty = model.Quantity;
            var discount = model.DiscountPerId;
            var discounts = Convert.ToDecimal(discount);
            var unitprice = model.UnitPrice * conversionval;
            int intVal = int.Parse(taxper);
            var subtotal = qty * unitprice;
            var disamount = subtotal * discounts / 100;
            var subtotals = subtotal - disamount;
            var taxamount1 = subtotals * intVal / 100;
            var total = taxamount1 + subtotals;
            GRNModel.SubTotal = subtotals;
            GRNModel.TaxValue = taxamount1;
            GRNModel.GrandTotal = total;
            GRNModel.DiscountValue = disamount;

            var subtotal1 = (qty + 1) * unitprice;
            var disamount1 = subtotal1 * discounts / 100;
            var subtotals1 = subtotal1 - disamount1;
            var taxamounts = subtotals1 * intVal / 100;
            var total1 = taxamounts + subtotals1;

            var subvalue = subtotals1 - subtotals;
            var disvalue = disamount1 - disamount;
            var taxvalue = taxamounts - taxamount1;
            var totalvalue = total1 - total;

            GRNModel.Total_discountval = model.Total_discountval - disvalue;
            GRNModel.Total_Subtotal = model.Total_Subtotal - subvalue;
            GRNModel.Total_TaxValue = model.Total_TaxValue - taxvalue;
            GRNModel.Total_Grandtotal = model.Total_Grandtotal - totalvalue;

            return Json(GRNModel, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult input_Calculationdetails(GRNModel model)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            GRNModel GRNModel = new GRNModel();
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
                     conversionval= ConversionValue.conversionvalue;
                    id= ConversionValue.id;
            }


            var product = productManager.GetId(model.ProductNameId);
            var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);

            var taxper = tax.TaxValue;
            var qty = model.Quantity;
            var discount = model.DiscountPerId;
            var discounts = Convert.ToDecimal(discount);
            var unitprice = model.UnitPrice * conversionval;
            int intVal = int.Parse(taxper);
            var subtotal = qty * unitprice;
            var disamount = subtotal * discounts / 100;
            var subtotals = subtotal - disamount;
            var taxamount1 = subtotals * intVal / 100;
            var total = taxamount1 + subtotals;
            GRNModel.SubTotal = subtotals;
            GRNModel.TaxValue = taxamount1;
            GRNModel.GrandTotal = total;
            GRNModel.DiscountValue = disamount;

            var subtotal1 = (qty + 1) * unitprice;
            var disamount1 = subtotal1 * discounts / 100;
            var subtotals1 = subtotal1 - disamount1;
            var taxamounts = subtotals1 * intVal / 100;
            var total1 = taxamounts + subtotals1;

            GRNModel.Total_discountval = model.Total_discountval;
            GRNModel.Total_Subtotal = model.Total_Subtotal ;
            GRNModel.Total_TaxValue = model.Total_TaxValue ;
            GRNModel.Total_Grandtotal = model.Total_Grandtotal;

            return Json(GRNModel, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Inc_Calculationdetails(GRNModel model)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            GRNModel GRNModel = new GRNModel();
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
            var product = productManager.GetId(model.ProductNameId);
            var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
            var taxper = tax.TaxValue;
            var qty = model.Quantity;
            var discount = model.DiscountPerId;
            var discounts = Convert.ToDecimal(discount);
            var unitprice = model.UnitPrice * conversionval;
            int intVal = int.Parse(taxper);
            var subtotal = qty * unitprice;
            var disamount = subtotal * discounts / 100;
            var subtotals = subtotal - disamount;
            var taxamount1 = subtotals * intVal / 100;
            var total = taxamount1 + subtotals;
            GRNModel.SubTotal = subtotals;
            GRNModel.TaxValue = taxamount1;
            GRNModel.GrandTotal = total;
            GRNModel.DiscountValue = disamount;

            var subtotal1 = (qty + 1) * unitprice;
            var disamount1 = subtotal1 * discounts / 100;
            var subtotals1 = subtotal1 - disamount1;
            var taxamounts = subtotals1 * intVal / 100;
            var total1 = taxamounts + subtotals1;

            var subvalue = subtotals1 - subtotals;
            var disvalue = disamount1 - disamount;
            var taxvalue = taxamounts - taxamount1;
            var totalvalue = total1 - total;

            GRNModel.Total_discountval = model.Total_discountval + disvalue;
            GRNModel.Total_Subtotal = model.Total_Subtotal + subvalue;
            GRNModel.Total_TaxValue = model.Total_TaxValue + taxvalue;
            GRNModel.Total_Grandtotal = model.Total_Grandtotal + totalvalue;

            return Json(GRNModel, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Filteration
        public ActionResult Getpono(int id)
        {
            PoManager PoManager = new PoManager();
            var data = PoManager.GetPOId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
