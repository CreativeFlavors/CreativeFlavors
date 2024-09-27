using iTextSharp.text;
using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models;
using MMS.Web.Models.Addressdetails;
using MMS.Web.Models.FinishedGoods;
using MMS.Web.Models.GRNModel;
using MMS.Web.Models.IndentMaterial;
using MMS.Web.Models.Po;
using MMS.Web.Models.SupplierMasterModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using static MMS.Web.Controllers.Report.GrnGstReportController;

namespace MMS.Web.Controllers
{
    public class GRNController : Controller
    {
        #region View
        [HttpGet]
        public ActionResult GRNMaster()
        {
            return View();
        }
        public ActionResult GRNGrid(int page = 1, int pageSize = 15)
        {
            GRNHeaderManager headerManager = new GRNHeaderManager();
            var grndetails = headerManager.GetgrnList();
            List<GRNModel> totalList = new List<GRNModel>();
            foreach (var item in grndetails)
            {
                GRNModel model = new GRNModel();
                model.GrnDate = item.GrnDate;
                model.RefInvoiceNumber = item.RefInvoiceNumber;
                model.item = item.Items;
                model.Quantity = item.Quantity;
                model.grnnumber = item.grnnumber;
                model.UnitPrice = item.UnitPrice;
                model.PoHeaderId = item.PoHeaderId;
                model.SubTotal = item.SubTotal;
                model.TaxValue = item.TaxValue;
                model.DiscountValue = item.DiscountValue;
                model.TotalValue = item.TotalValue;
                model.suppliername = item.SupplierName;
                totalList.Add(model);
            }
            var totalCount = totalList.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalList = totalList.OrderByDescending(s => s.GrnHeaderId)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return PartialView("~/Views/GRN/Partial/GRNGrid.cshtml", totalList);
        }

        public ActionResult GRNDetails(int? POno)
        {
            GRNModel model = new GRNModel();
            GRNHeaderManager GRNHeaderManager = new GRNHeaderManager();
            GRNCartManager gRNCartManager = new GRNCartManager();
            gRNCartManager.Putcancelsuccess();

            if (POno == null)
            {
                model.GrnDate = DateTime.Today;
                int grnno = GRNHeaderManager.GetNextGRNNumberFromDatabase();
                if(grnno == 0)
                {
                    model.grnnumber = 1;
                }
                else
                {
                    model.grnnumber = grnno;
                }
            }
            return View("~/Views/GRN/Partial/GRN_Details.cshtml", model);
        }
        public ActionResult GetPODetails(int POno)
        {
            GRNCartManager gRNCartManager = new GRNCartManager();
            gRNCartManager.Putcancelsuccess();
            GRNModel model = new GRNModel();
            PoManager poManager = new PoManager();
            GRNCartManager GRNCartManager = new GRNCartManager();
            GRNDetailsManager gRNDetailsManager = new GRNDetailsManager();
            GRNHeaderManager gRNHeaderManager = new GRNHeaderManager();
            PoManager PoManager = new PoManager();

            var GRNheader = gRNHeaderManager.GetGRNId(POno);
            if (GRNheader.Count() == 0)
            {
                var POList = poManager.GetPODetails();
                var POHeaderlist = poManager.Get().Where(m => m.PoNumber == POno).FirstOrDefault();
                var totalist = POList.Where(m => m.PoNumber == POno).ToList();
                model.Total_Price = POHeaderlist.TotalPrice;
                model.Total_discountval = POHeaderlist.TotalDiscountValue;
                model.Total_Subtotal = POHeaderlist.TotalSubtotalValue;
                model.Total_TaxValue = POHeaderlist.TotalTaxValue;
                model.Total_Grandtotal = POHeaderlist.TotalTotalValue;

                model.polist = totalist;
            }
            else
            {
                List<PODetails> totaldata = new List<PODetails>();
                var POList = poManager.GetPODetails();
                var POHeaderlist = poManager.Get().Where(m => m.PoNumber == POno).FirstOrDefault();
                var totalist = POList.Where(m => m.PoNumber == POno && m.isactive == true).ToList();

                foreach (var i in totalist)
                {
                    PODetails PoModel = new PODetails();
                    if (i.grnqty != null)
                    {
                        var qtys = (i.PoQty) - (i.grnqty);
                        PoModel.PoQty = qtys;
                        PoModel.DiscountValue = i.DiscountValue;
                        PoModel.UnitPrice = i.UnitPrice;
                        PoModel.UomName = i.UomName;
                        PoModel.ProductName = i.ProductName;
                        PoModel.Productcode = i.Productcode;
                        PoModel.podetail = i.podetail;
                        PoModel.poheader = i.poheader;
                        PoModel.discountpercentage = i.discountpercentage;
                        PoModel.taxpercentage = i.taxpercentage;
                        PoModel.productid = i.productid;
                        var subtotal = qtys * i.UnitPrice;
                        var disamount = subtotal * i.discountpercentage / 100;
                        var subtotals = subtotal - disamount;
                        var taxamount1 = subtotals * (i.taxpercentage) / 100;
                        var total = taxamount1 + subtotals;
                        PoModel.SubtotalValue = subtotals;
                        PoModel.TaxValue = taxamount1;
                        PoModel.TotalValue = total;
                        PoModel.DiscountValue = disamount;
                    }
                    else
                    {
                        var qtys = i.PoQty;
                        PoModel.PoQty = qtys;
                        PoModel.DiscountValue = i.DiscountValue;
                        PoModel.UnitPrice = i.UnitPrice;
                        PoModel.UomName = i.UomName;
                        PoModel.ProductName = i.ProductName;
                        PoModel.Productcode = i.Productcode;
                        PoModel.podetail = i.podetail;
                        PoModel.poheader = i.poheader;
                        PoModel.discountpercentage = i.discountpercentage;
                        PoModel.taxpercentage = i.taxpercentage;
                        PoModel.productid = i.productid;
                        var subtotal = qtys * i.UnitPrice;
                        var disamount = subtotal * i.discountpercentage / 100;
                        var subtotals = subtotal - disamount;
                        var taxamount1 = subtotals * (i.taxpercentage) / 100;
                        var total = taxamount1 + subtotals;
                        PoModel.SubtotalValue = subtotals;
                        PoModel.TaxValue = taxamount1;
                        PoModel.TotalValue = total;
                        PoModel.DiscountValue = disamount;
                    }
                    totaldata.Add(PoModel);
                }
                decimal? subtotals1 = 0;
                decimal? discount = 0;
                decimal? tax = 0;
                decimal? grandtotal = 0;
                decimal? unitprice = 0;

                foreach (var i in totaldata)
                {
                    subtotals1 += i.SubtotalValue;
                    discount += i.DiscountValue;
                    tax += i.TaxValue;
                    grandtotal += i.TotalValue;
                    unitprice += i.UnitPrice;
                }
                model.Total_Price = unitprice;
                model.Total_Subtotal = Math.Round((decimal)subtotals1, 2);
                model.Total_discountval = Math.Round((decimal)discount, 2);
                model.Total_TaxValue = Math.Round((decimal)tax, 2);
                model.Total_Grandtotal = Math.Round((decimal)grandtotal, 2);

                model.polist = totaldata;
            }


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
            PoManager poManager = new PoManager();
            string dateOnly = DateTime.Now.ToString("yyyy-MM-dd");
            decimal? conversionval = 0;
            int id = 0; int forid = 0;
            if (model.currencyOption.ToUpper() != "ZAR")
            {
                var ConversionValue = salesorderManager.Getcurrencyconversion("USD", "ZAR", dateOnly);
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
            var podetails = PoManager.Getdetails().Where(x => x.PodetailId == model.PoDetailId).FirstOrDefault();
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
            GRNCart.GRNNumber = model.grnnumber;
            GRNCart.poquantity = model.PoQuantity;
            GRNCart.Grndate = DateTime.Now;
            GRNCart.ExpiryDate = model.ExpiryDate;
            GRNCart.BatchCode = model.BatchCode;
            GRNCart.StoreCode = podetails.StoreCode;
            GRNCart.unitprice = model.UnitPrice;
            GRNCart.poquantity = podetails.Quantity;
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
            if (list != null)
            {
                decimal? dt_qty = 0;
                decimal? dis = 0;
                decimal? sub = 0;
                decimal? taxs = 0;
                decimal? grand = 0;

                if (list.Quantity != null)
                {
                    dt_qty = list.Quantity + model.Quantity;
                    sub = list.Subtotal + GRNCart.Subtotal;
                    dis = list.DiscountValue + GRNCart.DiscountValue;
                    taxs = list.TaxValue + GRNCart.TaxValue;
                    grand = list.Grandtotal + GRNCart.Grandtotal;

                }
                GRNCart.DiscountValue = dis;
                GRNCart.TaxValue = taxs;
                GRNCart.Subtotal = sub;
                GRNCart.Grandtotal = grand;
                GRNCart.Quantity = dt_qty;
                GRNCart.BatchCode = model.BatchCode;
                GRNCart.ExpiryDate = model.ExpiryDate;
                GRNCart = gRNCartManager.Put(GRNCart);
            }
            else
            {
                GRNCart = gRNCartManager.POST(GRNCart);
            }
            var podt = poManager.GetDTId(model.PoDetailId);
            decimal? grnqty = 0;
            if (podt.grn_qty != null)
            {
                 grnqty = (GRNCart.poquantity - podt.grn_qty) - GRNCart.Quantity;
            }
            else
            {
                 grnqty = GRNCart.poquantity  - GRNCart.Quantity;
            }
            var finsubtotal = podetails.Subtotal > GRNCart.Subtotal ? podetails.Subtotal - GRNCart.Subtotal : GRNCart.Subtotal - podetails.Subtotal;
            var fintaxtotal = podetails.TaxValue > GRNCart.TaxValue ? podetails.TaxValue - GRNCart.TaxValue : GRNCart.TaxValue - podetails.TaxValue;
            var fingrandtotal = podetails.TotalValue > GRNCart.Grandtotal ? podetails.TotalValue - GRNCart.Grandtotal : GRNCart.Grandtotal - podetails.TotalValue;
            var findistotal = podetails.DiscountValue > GRNCart.DiscountValue ? podetails.DiscountValue - GRNCart.DiscountValue : GRNCart.DiscountValue - podetails.DiscountValue;

            decimal? unitprices = GRNCart.unitprice;
            AlertMessage = "Added Successfully";
            return Json(new { Grnqty = grnqty, Unitprices = unitprices, Finsubtotal = finsubtotal, Fintaxtotal = fintaxtotal, Gingrandtotal = fingrandtotal, Findistotal = findistotal, AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ConfirmGRN(GRNModel model)
        {
            var stount = 0;
            var AlertMessage = "";
            PoManager PoManager = new PoManager();
            BatchStockManager batchStockManager = new BatchStockManager();
            GRNCartManager GrnManager = new GRNCartManager();
            GRNHeaderManager GRNHeaderManager = new GRNHeaderManager();
            GRNDetailsManager GRNDetailsManager = new GRNDetailsManager();
            PoDetail poDetail = new PoDetail();
            BatchStock batchStock1 = new BatchStock();
            CurrencyManager currencyManager = new CurrencyManager();
            var grnlist = GrnManager.GetgrncartHD(model.PoHeaderId,model.grnnumber);
            var count = grnlist.Count;
            if (count <= 0)
            {
                AlertMessage = "Not Existed";
                return Json(new { AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var podetails = PoManager.Getdetails().Where(x => x.PoNumber == model.PoHeaderId).FirstOrDefault();
                var currencyids = currencyManager.GetContainCurrencyid(model.currencyOption);
                GRNHeader GRNHeader = new GRNHeader();
                GRNHeader.Items = grnlist.Count();
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
                GRNHeader.GRNNumber = model.grnnumber;
                GRNHeader.IsFulfilled = DateTime.Now;
                GRNHeader.ShipmentDetails = model.ShipmentDetails;
                GRNHeader.Notes = model.Notes;
                GRNHeader.OverallWeight = model.OverallWeight;
                GRNHeader.RefInvoiceDate = model.RefInvoiceDate;
                GRNHeader.RefInvoiceNumber = model.RefInvoiceNumber;
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
                    var grn_dt = GRNDetailsManager.GetGRNdtId(Convert.ToInt32(i.podetailid));
                    decimal? quantitys = 0;
                    foreach (var k in grn_dt)
                    {
                        if (k.Quantity != 0)
                        {
                            quantitys += (k.Quantity);
                        }
                    }
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
                    GRNDetails.Weight = model.Weight;
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
                    if ((Convert.ToInt32(i.Quantity)) != 0 && grn_dt.Count() != 0)
                    {
                        poDetail.grn_qty = i.Quantity + quantitys;
                        poDetail.PodetailId = i.podetailid;
                        var data = GRNDetailsManager.POST(GRNDetails);
                        PoManager.Put(poDetail);
                        var poqty = PoManager.GetDTId(i.podetailid);
                        if(poqty.Quantity > (i.Quantity + quantitys))
                        {
                            stount--;
                        }
                        else if (poqty.Quantity == (i.Quantity + quantitys))
                        {
                            stount++;
                        }
                        BatchStock batchStock = new BatchStock();
                        batchStock.SupplierId = headerid.SupplierId;
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
                    else if ((Convert.ToInt32(i.Quantity)) != 0)
                    {
                        poDetail.grn_qty = i.Quantity;
                        poDetail.PodetailId = i.podetailid;
                        var data = GRNDetailsManager.POST(GRNDetails);
                        PoManager.Put(poDetail);
                        var poqty = PoManager.GetDTId(i.podetailid);
                        if (poqty.Quantity > i.Quantity)
                        {
                            stount--;
                        }
                        else if (poqty.Quantity == i.Quantity)
                        {
                            stount++;
                        }
                        BatchStock batchStock = new BatchStock();
                        batchStock.SupplierId = headerid.SupplierId;
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
                        AlertMessage = "Confirm Order";
                    }
           
                }
                if (count > stount)
                {
                    var PO_partialstatus = PoManager.PutPartialPoHeader(model.PoHeaderId);
                }
                else if (count == stount)
                {
                    var PO_partialstatus = PoManager.PutfulfilledPoHeader(model.PoHeaderId);

                }
                var bOMMaterial = GrnManager.Putstatussuccess(model.grnnumber);


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
                var delete = GRNCartManager.Delete(GRNCart.grncartid);
                list = GRNCartManager.GetGRNCsrtList(delete.poheaderid);
            }

            if (list != null)
            {
                model.grnCarlist = list;
            }
            return Json(new { AlertMessage = AlertMessage, model }, JsonRequestBehavior.AllowGet);
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

            GRNModel.Total_discountval = model.Total_discountval;
            GRNModel.Total_Subtotal = model.Total_Subtotal;
            GRNModel.Total_TaxValue = model.Total_TaxValue;
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
        [HttpGet]
        public ActionResult GRNSearch(string filter)
        {
            GRNHeaderManager headerManager = new GRNHeaderManager();
            var grndetails = headerManager.GetgrnList();
            var list = grndetails.Where(x => x.PoHeaderId.ToString().ToLower().Trim().Contains(filter.ToString().ToLower().Trim())).ToList();
            List<GRNModel> totalList = new List<GRNModel>();

            foreach (var item in list)
            {
                GRNModel model = new GRNModel();
                model.GrnDate = item.GrnDate;
                model.RefInvoiceNumber = item.RefInvoiceNumber;
                model.item = item.Items;
                model.Quantity = item.Quantity;
                model.UnitPrice = item.UnitPrice;
                model.PoHeaderId = item.PoHeaderId;
                model.SubTotal = item.SubTotal;
                model.grnnumber = item.grnnumber;
                model.TaxValue = item.TaxValue;
                model.DiscountValue = item.DiscountValue;
                model.TotalValue = item.TotalValue;
                model.suppliername = item.SupplierName;
                totalList.Add(model);
            }

            return Json(totalList, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
