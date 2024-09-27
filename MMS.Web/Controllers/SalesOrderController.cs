using iText.Layout.Properties;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Repository.Service;
using MMS.Web.Models;
using MMS.Web.Models.Addressdetails;
using MMS.Web.Models.CustomerTransaction;
using MMS.Web.Models.StockModel;
using MMS.Web.Models.TempsalesorderModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using MMS.Data.Mapping;
using MMS.Repository.ViewModel;
using MMS.Repository.Service;
using iText.IO.Image;
using iText.Layout.Borders;
using MMS.Web.Models.BuyerMaserModel;
using iTextSharp.text.html.simpleparser;
using System.Text;
using iText.Html2pdf;
using Antlr.Runtime.Tree;
using MMS.Data.StoredProcedureModel;
namespace MMS.Web.Controllers
{
    public class SalesOrderController : Controller
    {
        #region SalesOrder View
        [HttpGet]

        public ActionResult SalesOrderMaster()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SalesOrderGrid(int page = 1, int pageSize = 15)
        {
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            SalesorderHD_Manager SalesorderHD_Manager = new SalesorderHD_Manager();
            Salesorders model = new Salesorders();
            var totallist = salesorderManager.salesorder_Grid();
            var totalCount = totallist.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totallist = totallist.OrderByDescending(s => s.salesorderid_dt)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            BuyerMasterManager BuyerManager = new BuyerMasterManager();
            var data1 = BuyerManager.Get();
            List<Salesorders> totalListheader = new List<Salesorders>();
            var data = SalesorderHD_Manager.Get();

            foreach (var i in data)
            {
                Salesorders models = new Salesorders();
                var salesorderdt = salesorderManager.GetSOIdS(i.salesorderid_hd);
                var counts = 0;
                decimal? dc_qty = 0;
                decimal? Invoice_qty = 0;
                foreach (var k in salesorderdt)
                {
                    if (k.dc_qty != null)
                    {
                        counts++;
                        dc_qty += k.dc_qty;
                    }
                    if (k.Invoice_qty != null)
                    {
                        Invoice_qty += k.Invoice_qty;
                    }
                }
                models.dc_qty = dc_qty;
                models.itemdc = counts;
                models.invoice_qty = Invoice_qty;
                models.SalesorderId_HD = i.salesorderid_hd;
                models.salesorderdate = i.Salesorderdate;
                models.item = i.items;
                models.quantity = i.quantity;
                models.Status = i.Status;
                models.fullfilldate = i.fullfilldate;
                models.Total_Price = i.Total_price;
                models.Total_discountval = i.Total_disamount;
                models.Total_Grandtotal = i.grand_total;
                models.Total_Subtotal = i.Total_subtotal;
                models.Total_TaxValue = i.Total_taxamount;
                models.BuyerMaster = data1.Where(W => W.BuyerMasterId == i.customerid).ToList().FirstOrDefault();
                totalListheader.Add(models);
            }
            var totalCountsummary = totalListheader.Count();

            int totalPagessummary = (int)Math.Ceiling((double)totalCountsummary / pageSize);

            int startIndexsummary = (page - 1) * pageSize;
            int endIndexsummary = Math.Min(startIndexsummary + totalPagessummary - 1, totalCountsummary - 1);
            totalListheader = totalListheader.OrderByDescending(s => s.SalesorderId_HD)
                         .Skip(startIndexsummary)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPagessummary = totalPagessummary;
            ViewBag.CurrentPagesummary = page;
            ViewBag.PageSizesummary = pageSize;


            model.Salesorder_Gridlist = totallist;
            model.salesorderLists= totalListheader;
            return PartialView("~/Views/SalesOrder/partial/SalesorderGrid.cshtml", model);
        }
        [HttpGet]
        public ActionResult salesorderheader(int page = 1, int pageSize = 15)
        {
            Salesorders model = new Salesorders();
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            SalesorderHD_Manager SalesorderHD_Manager = new SalesorderHD_Manager();
            BuyerMasterManager BuyerManager = new BuyerMasterManager();
            var data1 = BuyerManager.Get();
            List<Salesorders> totalListheader = new List<Salesorders>();
            var data = SalesorderHD_Manager.Get();

            foreach (var i in data)
            {
                Salesorders models = new Salesorders();
                var salesorderdt = salesorderManager.GetSOIdS(i.salesorderid_hd);
                var counts = 0;
                decimal? dc_qty = 0;
                decimal? Invoice_qty = 0;
                foreach (var k in salesorderdt)
                {
                    if (k.dc_qty != null)
                    {
                        counts++;
                        dc_qty += k.dc_qty;
                    }
                    if (k.Invoice_qty != null)
                    {
                        Invoice_qty += k.Invoice_qty;
                    }
                }
                models.dc_qty = dc_qty;
                models.itemdc = counts;
                models.invoice_qty = Invoice_qty;
                models.SalesorderId_HD = i.salesorderid_hd;
                models.salesorderdate = i.Salesorderdate;
                models.item = i.items;
                models.Status = i.Status;
                models.fullfilldate = i.fullfilldate;
                models.quantity = i.quantity;
                models.Total_Price = i.Total_price;
                models.Total_discountval = i.Total_disamount;
                models.Total_Grandtotal = i.grand_total;
                models.Total_Subtotal = i.Total_subtotal;
                models.Total_TaxValue = i.Total_taxamount;
                models.BuyerMaster = data1.Where(W => W.BuyerMasterId == i.customerid).ToList().FirstOrDefault();
                totalListheader.Add(models);
            }
            var totalCountsummary = totalListheader.Count();

            int totalPagessummary = (int)Math.Ceiling((double)totalCountsummary / pageSize);

            int startIndexsummary = (page - 1) * pageSize;
            int endIndexsummary = Math.Min(startIndexsummary + totalPagessummary - 1, totalCountsummary - 1);
            totalListheader = totalListheader.OrderByDescending(s => s.SalesorderId_HD)
                         .Skip(startIndexsummary)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPagessummary = totalPagessummary;
            ViewBag.CurrentPagesummary = page;
            ViewBag.PageSizesummary = pageSize;
            model.salesorderLists = totalListheader;

            return PartialView("~/Views/SalesOrder/partial/SalesorderHeaderGrid.cshtml", model);
        }     
        [HttpGet]
        public ActionResult salesorderDetailsgrid(int page = 1, int pageSize = 15)
        {
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            SalesorderHD_Manager SalesorderHD_Manager = new SalesorderHD_Manager();
            Salesorders model = new Salesorders();
            var totallist = salesorderManager.salesorder_Grid();
            var totalCount = totallist.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totallist = totallist.OrderByDescending(s => s.salesorderid_dt)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            model.Salesorder_Gridlist = totallist;

            return PartialView("~/Views/SalesOrder/partial/SalesOrderDetailsGrid.cshtml", model);
        }
        [HttpGet]
        public ActionResult SalesOrderDetails()
        {
            Salesorders model = new Salesorders();
            return View("SalesOrderDetails",model);
        }
        [HttpGet]
        public ActionResult updateSalesOrderDetails(int id = 0, int salesid = 0)
        {
            Salesorders model = new Salesorders();
            SalesorderManager salesorderManager = new SalesorderManager();
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            var data = custAddressMangers.GetCustAddressbuyerid(id);
            if (id == 0 && salesid == 0)
            {
                //var bOMMaterial = salesorderManager.Putstatus();
            }
            else
            {
                var datas = salesorderManager.GetsalesorderList(id, salesid);
                decimal? price = 0;
                decimal? subtotal = 0;
                decimal? discount = 0;
                decimal? tax = 0;
                decimal? grandtotal = 0;

                foreach (var i in datas)
                {
                    price += i.price;
                    subtotal += i.subtotal;
                    discount += i.discountvalue;
                    tax += i.taxvalue;
                    grandtotal += i.totalprice;
                }
                //model.Billingadd = data.Add1;
                //model.shippingadd = data.Add2;
                model.Total_Price = price;
                model.Total_Subtotal = subtotal;
                model.Total_discountval = discount;
                model.Total_TaxValue = tax;
                model.Total_Grandtotal = grandtotal;
                model.BuyerName = id;
                model.salesordernumber = salesid;
                model.salesorderList = datas;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SalesOrderQtycheck(int id)
        {
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            SalesorderHD_Manager salesorderHD_manager = new SalesorderHD_Manager();
            var st = salesorderManager.GetSO(id);
            var mrp_Material_Lists = salesorderManager.GetmrpmaterialList(st.productid);

            List<ParentBillofMaterial> totalList = new List<ParentBillofMaterial>();

            var i = salesorderHD_manager.salesorder_get(id);

            Salesorders salesorders = new Salesorders();
            salesorders.SalesorderId = i.salesorderid;
            salesorders.SalesorderId_DT = i.salesorderid_dt;
            salesorders.salesorderdate = i.salesorderdate;
            salesorders.quantity = i.quantity;
            salesorders.Total_Price = i.totalprice;
            salesorders.Uomname = i.long_unit_name;
            salesorders.ProductName = i.productname;
            salesorders.ProductCode = i.productcode;
            salesorders.ProductID = i.productid;
            salesorders.BuyerNames = i.buyer_full_name;
            salesorders.Bomno = i.bomno;
            foreach (var item in mrp_Material_Lists)
            {
                ParentBillofMaterial availablestock = new ParentBillofMaterial();
                availablestock.MaterialNames = item.MaterialNames;
                availablestock.Requiredqty = item.RequiredQty;
                availablestock.availablestock = item.AvailableStock;
                availablestock.UOMName = item.UOMName;
                availablestock.MaterialMasterid = item.MaterialMasterId;
                availablestock.mrp_Material_Lists = salesorderManager.GetmrpmaterialList(item.MaterialMasterId);
                availablestock.productype = item.producttype;
                totalList.Add(availablestock);
            }

            var totalist = totalList.OrderByDescending(x => x.MaterialMasterid).ToList();
            salesorders.bOMMaterialListModels = totalist;

            return PartialView("partial/SalesOrderQtycheck", salesorders);
        }
        #endregion
        #region calculatiom 
        [HttpPost]
        public ActionResult Tempsalesorder(int id, int sodt)
        {
            Temp_salesorderManager temp_SalesorderManager1 = new Temp_salesorderManager();
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            var sts = salesorderManager.GetSO(sodt);
            List<Temp_Indent> tempro = new List<Temp_Indent>();
            if (sts != null)
            {
                tempro = temp_SalesorderManager1.GetProd(id, sts.Salesorderid_hd);
            }
            if (tempro.Count() != 0)
            {
                return Json(new { data = "Failer" }, JsonRequestBehavior.AllowGet);
            }
            ProductManager productManager = new ProductManager();
            Parentbom_materialManager bOMMaterialListManager = new Parentbom_materialManager();
            subassemblyManager subassemblyManager = new subassemblyManager();
            var st = salesorderManager.GetSO(sodt);
            var product = productManager.GetId(st.productid);
            var bommaterial = bOMMaterialListManager.GetMaterialList(product.BomNo);
            //var submaterial = subassemblyManager.GetMaterialList(product.BomNo);
            Temp_Indent temp_Salesorder = new Temp_Indent();
            foreach (var item in bommaterial)
            {
                decimal? qty = 0;
                BatchStockManager batchStockManager = new BatchStockManager();
                Temp_salesorderManager temp_SalesorderManager = new Temp_salesorderManager();
                var MaterialOpeningMaster = batchStockManager.GetmaterialOpeningMaterialID(item.ProductId);
                temp_Salesorder.SalesOrderId = st.Salesorderid_hd;
                temp_Salesorder.BuyerId = st.Customerid;
                temp_Salesorder.ProductId = product.ProductId;
                temp_Salesorder.ProductItem = st.quantity;
                if (MaterialOpeningMaster.Count() != 0)
                {
                    foreach (var i in MaterialOpeningMaster)
                    {
                        qty += i.Quantity;
                    }
                    if (qty != null)
                    {
                        temp_Salesorder.AvailableStock = qty;
                        temp_Salesorder.AvailableUnitId = item.UomId;
                    }
                    temp_Salesorder.MaterialId = item.ProductId;
                    temp_Salesorder.AvailableUnitId = item.UomId;
                    var consume = st.quantity * item.RequiredQty;
                    temp_Salesorder.Consume = consume;
                    temp_Salesorder.ConsumeUnitId = item.UomId;
                    var stockRequired = consume - qty;
                    temp_Salesorder.stockRequired = stockRequired;
                    if (qty <= consume)
                    {
                        temp_SalesorderManager.Post(temp_Salesorder);
                    }
                }
                else
                {
                    temp_Salesorder.MaterialId = item.ProductId;
                    var consume = st.quantity * item.RequiredQty;
                    temp_Salesorder.Consume = consume;
                    temp_Salesorder.ConsumeUnitId = item.UomId;
                    var stockRequired = consume;
                    temp_Salesorder.stockRequired = stockRequired;
                    temp_SalesorderManager.Post(temp_Salesorder);
                }
            }
            var data = "Success";

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TempProductionstock(int id, int sodt, int proid)
        {
            Temp_productionManager Temp_productionManager = new Temp_productionManager();
            var st1 = Temp_productionManager.GetPro(id);
            List<temp_production> obj = new List<temp_production>();
            if (st1 != null)
            {
                obj = Temp_productionManager.GetProd(proid, st1.SalesOrderId);
            }

            if (obj.Count() != 0)
            {
                return Json(new { data = "Failer" }, JsonRequestBehavior.AllowGet);
            }
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            subassemblyManager subassemblyManager = new subassemblyManager();
            ProductManager productManager = new ProductManager();
            temp_production temp_production = new temp_production();
            Parentbom_materialManager bOMMaterialListManager = new Parentbom_materialManager();
            var st = salesorderManager.GetSO(sodt);
            var product = productManager.GetId(st.productid);
            var bommaterial = bOMMaterialListManager.GetMaterialList(product.BomNo);
            var submaterial = subassemblyManager.GetMaterialList(product.BomNo);
            preproduction preproduction = new preproduction();
            foreach (var item in bommaterial)
            {
                decimal? qty = 0;
                BatchStockManager batchStockManager = new BatchStockManager();
                Temp_productionManager Temp_productionManagers = new Temp_productionManager();
                var batchStockMaster = batchStockManager.GetmaterialOpeningMaterialID(item.ProductId);
                temp_production.SalesOrderId = st.Salesorderid_hd;
                preproduction.SalesOrderNo = st.Salesorderid_hd;
                temp_production.BuyerId = st.Customerid;
                temp_production.ProductId = product.ProductId;
                temp_production.ProductItem = st.quantity;
                temp_production.Bomid = product.BomNo;
                if (batchStockMaster != null)
                {
                    foreach (var i in batchStockMaster)
                    {
                        qty += i.Quantity;
                    }
                    if (qty != null)
                    {
                        temp_production.AvailableStock = qty;
                    }
                    temp_production.MaterialId =item.ProductId;
                    temp_production.AvailableUnitId = item.UomId;
                    var consume =  item.RequiredQty;
                    temp_production.Consume = consume;
                    temp_production.ConsumeUnitId = item.UomId;
                    preproduction.ProductId = product.ProductId;
                    preproduction.SalesOrderDate = st.salesorderdate;
                    preproduction.BuyerId = st.Customerid;
                    preproduction.Qty = st.quantity;
                    preproduction.Materialid = item.ProductId;
                    Temp_productionManagers.Postpreproduction(preproduction);
                    Temp_productionManagers.Post(temp_production);

                }
            }
            foreach (var item in submaterial)
            {
                decimal? qtu1 = 0;
                BatchStockManager batchStockManager = new BatchStockManager();
                Temp_productionManager Temp_productionManagers = new Temp_productionManager();
                var batchStockMaster = batchStockManager.GetmaterialOpeningMaterialID(item.ProductId);
                temp_production.SalesOrderId = st.Salesorderid_hd;
                preproduction.SalesOrderNo = st.Salesorderid_hd;
                temp_production.BuyerId = st.Customerid;
                temp_production.ProductId = product.ProductId;
                temp_production.ProductItem = st.quantity;
                temp_production.Bomid = product.BomNo;
                if (batchStockMaster.Count() != 0)
                {
                    var products = productManager.GetId(item.ProductId);
                    temp_production.MaterialId = item.ProductId;
                    foreach (var i in batchStockMaster)
                    {
                        qtu1 += i.Quantity;
                    }
                    if (qtu1 != null)
                    {
                        temp_production.AvailableStock = qtu1;
                    }
                    temp_production.AvailableUnitId = products.UomMasterId;
                    var consume =item.RequiredQty;
                    temp_production.Consume = consume;
                    temp_production.ConsumeUnitId = products.UomMasterId;
                    preproduction.ProductId = product.ProductId;
                    preproduction.SalesOrderDate = st.salesorderdate;
                    preproduction.BuyerId = st.Customerid;
                    preproduction.Qty = st.quantity;
                    preproduction.Materialid = item.ProductId;
                    Temp_productionManagers.Postpreproduction(preproduction);
                    Temp_productionManagers.Post(temp_production);

                }
            }
            var data = "Success";

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TempProductionSubassembly(int proid, int SOid)
        {
            Temp_productionManager Temp_productionManager = new Temp_productionManager();
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            List<temp_production> tempro = new List<temp_production>();
            var st = salesorderManager.GetSO(SOid);
            if (st != null)
            {
                tempro = Temp_productionManager.GetProd(proid, st.Salesorderid_hd);
            }
            if (tempro.Count() != 0)
            {
                return Json(new { data = "Failer" }, JsonRequestBehavior.AllowGet);
            }

            ProductManager productManager = new ProductManager();
            Parentbom_materialManager bOMMaterialListManager = new Parentbom_materialManager();
            var product = productManager.GetId(proid);

            var bommaterial = bOMMaterialListManager.GetMaterialList(product.BomNo);
            temp_production temp_production = new temp_production();
            preproduction preproduction = new preproduction();
            subassemblyManager subassemblyManager = new subassemblyManager();
            foreach (var item in bommaterial)
            {
                decimal? qty = 0;
                BatchStockManager batchStockManager = new BatchStockManager();
                Temp_productionManager Temp_productionManagers = new Temp_productionManager();
                var batchStockMaster = batchStockManager.GetmaterialOpeningMaterialID(item.ProductId);
                var parentbom = subassemblyManager.Getproductid(proid);

                temp_production.SalesOrderId = st.Salesorderid_hd;
                preproduction.SalesOrderNo = st.Salesorderid_hd;
                temp_production.BuyerId = st.Customerid;
                temp_production.ProductId = product.ProductId;
                temp_production.producttype = product.ProductType;
                temp_production.ProductItem = parentbom.RequiredQty;
                temp_production.Bomid = product.BomNo;
                temp_production.MaterialId = item.ProductId;
                var consume = item.RequiredQty;
                foreach(var i in batchStockMaster)
                {
                    qty += i.Quantity;
                }
                if(qty != null)
                {
                    temp_production.AvailableStock = qty;
                    temp_production.AvailableUnitId = item.UomId;
                }
                temp_production.Consume = consume;
                temp_production.ConsumeUnitId = item.UomId;
                preproduction.ProductId = product.ProductId;
                preproduction.SalesOrderDate = st.salesorderdate;
                preproduction.BuyerId = st.Customerid;
                preproduction.Qty = (st.quantity * parentbom.RequiredQty);
                preproduction.Materialid = item.ProductId;
       ;
                Temp_productionManagers.Postpreproduction(preproduction);
                Temp_productionManagers.Post(temp_production);

            }
            var data = "Success";

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Tempsalesorder_Subassemblymaterial(int proid, int SOid)
        {
            Temp_salesorderManager Temp_productionManager = new Temp_salesorderManager();
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            var sts = salesorderManager.GetSO(SOid);
            List<Temp_Indent> tempro = new List<Temp_Indent>();
            if (sts != null)
            {
                tempro = Temp_productionManager.GetProd(proid, sts.Salesorderid_hd);
            }
            if (tempro.Count() != 0)
            {
                return Json(new { data = "Failer" }, JsonRequestBehavior.AllowGet);
            }
            ProductManager productManager = new ProductManager();
            Parentbom_materialManager bOMMaterialListManager = new Parentbom_materialManager();
            subassemblyManager subassemblyManager = new subassemblyManager();
            var st = salesorderManager.GetSO(SOid);
            var product = productManager.GetId(proid);
            var bommaterial = bOMMaterialListManager.GetMaterialList(product.BomNo);
            Temp_Indent temp_Salesorder = new Temp_Indent();
            foreach (var item in bommaterial)
            {
                decimal? qty = 0;
                BatchStockManager batchStockManager = new BatchStockManager();
                Temp_salesorderManager temp_SalesorderManager = new Temp_salesorderManager();
                var MaterialOpeningMaster = batchStockManager.GetmaterialOpeningMaterialID(item.BomMaterialId);
                var parentbom = subassemblyManager.Getproductid(proid);
                temp_Salesorder.SalesOrderId = st.Salesorderid_hd;
                temp_Salesorder.BuyerId = st.Customerid;
                temp_Salesorder.ProductId = product.ProductId;
                temp_Salesorder.ProductItem = st.quantity;
                if (MaterialOpeningMaster.Count() != 0)
                {
                    foreach (var i in MaterialOpeningMaster)
                    {
                        qty += i.Quantity;
                    }
                    if (qty != null)
                    {
                        temp_Salesorder.AvailableStock = qty;
                        temp_Salesorder.AvailableUnitId = item.UomId;
                    }
                    temp_Salesorder.MaterialId = item.ProductId;
                    var consume = (st.quantity * parentbom.RequiredQty) * item.RequiredQty;
                    temp_Salesorder.Consume = consume;
                    temp_Salesorder.ConsumeUnitId = item.UomId;
                    var stockRequired = consume - qty;
                    temp_Salesorder.stockRequired = stockRequired;
                    if (qty <= consume)
                    {
                        temp_SalesorderManager.Post(temp_Salesorder);
                    }
                }
                else
                {
                    temp_Salesorder.MaterialId = item.ProductId;
                    var consume = (st.quantity * parentbom.RequiredQty) * item.RequiredQty;
                    temp_Salesorder.Consume = consume;
                    temp_Salesorder.ConsumeUnitId = item.UomId;
                    var stockRequired = consume;
                    temp_Salesorder.stockRequired = stockRequired;
                    temp_SalesorderManager.Post(temp_Salesorder);
                }
            }
            var data = "Success";

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductidDetail(int id)
        {
            ProductManager ProductManager = new ProductManager();
            UOMManager uOMManager = new UOMManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();

            var totalList = (from d in ProductManager.Get()
                             join d1 in uOMManager.Get() on d.UomMasterId equals d1.UomMasterId
                             join d2 in taxTypeManager.Get() on d.TaxMasterId equals d2.TaxMasterID
                             where d.ProductId == id
                             select new Salesorders
                             {
                                 ProductName = d.ProductName,
                                 ProductCode = d.ProductCode,
                                 Price = d.Price,
                                 UomMaster = new UomMaster
                                 {
                                     ShortUnitName = d1.ShortUnitName,
                                 },
                                 TaxTypeMaster = new TaxTypeMaster
                                 {
                                     TaxValue = d2.TaxValue,
                                 },
                             }).FirstOrDefault();

            return Json(totalList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Calculationdetails(Salesorders model)
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
            var discount = model.discountperid;
            var unitprice = product.Price * conversionval;
            int intVal = int.Parse(taxper);

            if ((model.quantity != null) && (discount != null))
            {
                var subtotal = qty * unitprice;
                var disamount = subtotal * discount / 100;
                var subtotals = subtotal - disamount;
                var taxamount1 = subtotals * intVal / 100;
                var total = taxamount1 + subtotals;
                salesorders.Subtotal = subtotals;
                salesorders.TaxValue = taxamount1;
                salesorders.Grandtotal = total;
                salesorders.discountvalue = disamount;
            }
            else if (model.quantity != null)
            {
                var subtotal = qty * unitprice;
                var taxamount = subtotal * intVal / 100;
                var totalprice = taxamount + subtotal;
                var disamount = subtotal * discount / 100;
                salesorders.Subtotal = subtotal;
                salesorders.TaxValue = taxamount;
                salesorders.Grandtotal = totalprice;
                salesorders.discountvalue = disamount;
            }

            var datas = salesorderManager.GetsalesorderList(model.buyerid,model.salesordernumber);
            decimal? price = 0;
            decimal? subtotal1 = 0;
            decimal? discounts = 0;
            decimal? taxs = 0;
            decimal? grandtotal = 0;

            foreach (var i in datas)
            {
                price += i.price;
                subtotal1 += i.subtotal;
                discounts += i.discountvalue;
                taxs += i.taxvalue;
                grandtotal += i.totalprice;
            }
            if (price != 0 && subtotal1 != 0 && discounts != 0 && taxs != 0 && grandtotal != 0)
            {
                salesorders.Total_Price = price + unitprice;
                salesorders.Total_Subtotal = subtotal1 + salesorders.Subtotal;
                salesorders.Total_discountval = discounts + salesorders.discountvalue;
                salesorders.Total_TaxValue = taxs + salesorders.TaxValue;
                salesorders.Total_Grandtotal = grandtotal + salesorders.Grandtotal;
            }
            else
            {
                salesorders.Total_Price = unitprice;
                salesorders.Total_Subtotal = salesorders.Subtotal;
                salesorders.Total_TaxValue = salesorders.TaxValue;
                salesorders.Total_Grandtotal = salesorders.Grandtotal;
                salesorders.Total_discountval = salesorders.discountvalue;
            }

            return Json(salesorders, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Crud Operation
        [HttpPost]
        public ActionResult SalesorderDetails(Salesorders model)
        {
            string AlertMessage = "";
            SalesorderManager salesorderManager = new SalesorderManager();
            salesordercart salesorder = new salesordercart();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            BuyerMasterManager buyerManager = new BuyerMasterManager();

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
            if (model.salesordernumber == 0)
            {
                int? salesno = salesorderManager.GetNextsoNumberFromDatabase();
                if (salesno == 0 || salesno == null)
                {
                    salesorder.salesordernumber = 1;
                }
                else
                {
                    salesorder.salesordernumber = salesno;
                }
            }
            else
            {
                salesorder.salesordernumber = model.salesordernumber;
            }
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            var product = productManager.GetId(model.ProductID);
            var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
            var shipping = custAddressMangers.GetCustAddressbuyeridshipp(model.buyerid);
            var billing = custAddressMangers.GetCustAddressbuyerid(model.buyerid);
            if (shipping == null && billing == null)
            {
                AlertMessage = "Not Existed";
                return Json(new { AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);
            }
            else if (shipping == null)
            {
                AlertMessage = "Not Existed";
                return Json(new { AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);

            }
            else if (billing == null)
            {
                AlertMessage = "Not Existed";
                return Json(new { AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);
            }
            var addreddcode = buyerManager.GetBuyerMasterId(model.buyerid);
            salesorder.ProductNameid = model.ProductID;
            salesorder.ProductCode = product.ProductCode;
            salesorder.customerid = model.buyerid;
            salesorder.UomMasterId = product.UomMasterId;
            salesorder.Taxperid = product.TaxMasterId;
            salesorder.quantity = model.quantity;
            salesorder.Discountperid = model.discountperid;
            salesorder.Additionalcommends = model.Additionalcommends;
            salesorder.Specialinstruction = model.Specialinstruction;
            salesorder.Price = product.Price;
            salesorder.quotedate = DateTime.Now;
            salesorder.salesorderdate = DateTime.Now;
            salesorder.originalquotedate = DateTime.Now;
            salesorder.custaddcode = addreddcode.BuyerCode;
            salesorder.producttype_id = product.ProductType;
            salesorder.custbillcode = billing.Addresshd_id.ToString();
            salesorder.custshipcode = shipping.Addresshd_id.ToString();
            var unitprice = product.Price * conversionval;
            salesorder.taxinclusive = true;
            salesorder.isactive = true;
            var taxper = tax.TaxValue;
            var qty = model.quantity;
            var discount = model.discountperid;
            int intVal = int.Parse(taxper);
            if ((model.quantity != null) && (discount != null))
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
            else if (model.quantity != null)
            {
                var subtotal = qty * unitprice;
                var taxamount = subtotal * intVal / 100;
                var totalprice = taxamount + subtotal;
                var disamount = subtotal * discount / 100;
                salesorder.Subtotal = subtotal;
                salesorder.TaxValue = taxamount;
                salesorder.Grandtotal = totalprice;
                salesorder.Discountvalue = disamount;
            }

            salesorder = salesorderManager.Post(salesorder);

            AlertMessage = "Added Successfully";
            return Json(new { customerid = salesorder.customerid, AlertMessage = AlertMessage, salesorderno = salesorder.salesordernumber }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ConfirmSalesorder(Salesorders model)
        {
            var AlertMessage = "";

            SalesorderManager salesorderManager = new SalesorderManager();
            SalesorderHD_Manager salesorderHD_Manager = new SalesorderHD_Manager();
            SalesorderDT_Manager salesorderDT = new SalesorderDT_Manager();
            CurrencyManager currencyManager = new CurrencyManager();
            var productlist = salesorderManager.GetsalesorderCartList(model.buyerid,model.salesordernumber);
            var count = productlist.Count;
            if (count <= 0)
            {
                AlertMessage = "Not Existed";
                return Json(new { AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                Salesorder_hd salesorder_Hd = new Salesorder_hd();
                salesorder_Hd.customerid = model.buyerid;
                salesorder_Hd.items = productlist.Count();
                salesorder_Hd.Salesorderdate = DateTime.Now;
                salesorder_Hd.Total_price = model.Total_Price;
                salesorder_Hd.Total_subtotal = model.Total_Subtotal;
                salesorder_Hd.Total_taxamount = model.Total_TaxValue;
                salesorder_Hd.Total_disamount = model.Total_discountval;
                salesorder_Hd.grand_total = model.Total_Grandtotal;
                salesorder_Hd.isactive = true;
                decimal? quantity = 0;

                foreach (var i in productlist)
                {
                    quantity += i.quantity;
                }
                salesorder_Hd.quantity = quantity;

                var headerid = salesorderHD_Manager.POST(salesorder_Hd);

                foreach (var i in productlist)
                {
                    var currencyid = currencyManager.GetContainCurrencyid(model.currencyOption);
                    Salesorder_dt salesorder_Dt = new Salesorder_dt();
                    salesorder_Dt.Salesorderid_hd = headerid.salesorderid_hd;
                    salesorder_Dt.Customerid = headerid.customerid;
                    salesorder_Dt.productid = i.ProductNameid;
                    salesorder_Dt.ProductCode = i.ProductCode;
                    salesorder_Dt.UomMasterId = i.UomMasterId;
                    salesorder_Dt.Taxperid = i.Taxperid;
                    salesorder_Dt.TaxValue = i.TaxValue;
                    salesorder_Dt.unitprice = i.Price;
                    salesorder_Dt.quantity = i.quantity;
                    salesorder_Dt.Subtotal = i.Subtotal;
                    salesorder_Dt.totalprice = i.Grandtotal;
                    salesorder_Dt.producttype_id = i.producttype_id;
                    salesorder_Dt.Discountperid = i.Discountperid;
                    salesorder_Dt.Discountvalue = i.Discountvalue;
                    salesorder_Dt.Specialinstruction = i.Specialinstruction;
                    salesorder_Dt.Additionalcommends = i.Additionalcommends;
                    salesorder_Dt.currencyid = currencyid.id;
                    salesorder_Dt.salesorderdate = i.salesorderdate;
                    salesorder_Dt.custaddcode = i.custaddcode;
                    salesorder_Dt.custshipcode = i.custshipcode;
                    salesorder_Dt.custbillcode = i.custbillcode;
                    salesorder_Dt.originalquotedate = i.originalquotedate;
                    salesorder_Dt.taxinclusive = i.taxinclusive;
                    salesorder_Dt.Status = "1";
                    salesorder_Dt.isactive = i.isactive;
                    var data = salesorderDT.Post(salesorder_Dt);
                }
                var bOMMaterial = salesorderManager.Putstatussuccess();
                AlertMessage = "Confirm Order";
                return Json(new { AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SODelete(int SOId,int Buyerid,int Salesordernumber)
        {
            SalesorderManager SalesorderManager = new SalesorderManager();
            string AlertMessage = "";
            salesordercart parentbom = new salesordercart();
            parentbom = SalesorderManager.GetSO(SOId);
            if (parentbom != null)
            {
                AlertMessage = "Success";
                SalesorderManager.Delete(parentbom.SalesorderId);
            }
            var datas = SalesorderManager.GetsalesorderList(Buyerid, Salesordernumber);
            return Json(new { AlertMessage = AlertMessage, SalesOrderList = datas }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region filter

        public ActionResult searchs(int? customerid, int? SOid)
        {
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            List<Salesorders> totaldata = new List<Salesorders>();
            var totallist = salesorderManager.salesorder_Grid();
            var filteredList = (customerid == null || SOid == null)
                ? salesorderManager.salesorder_Grid()
                : totallist.Where(i => i.Buyerid == customerid && i.salesorderid == SOid);

            foreach (var i in filteredList)
            {
                Salesorders salesorder = new Salesorders();
                salesorder.SalesorderId = i.salesorderid;
                salesorder.salesorderdate = i.salesorderdate;
                salesorder.SalesorderId_DT = i.salesorderid_dt;
                salesorder.SalesorderId_HD = i.salesorderid_hd;
                salesorder.quantity = i.quantity;
                salesorder.discountvalue = i.discount_value;
                salesorder.Subtotal = i.subtotal;
                salesorder.TaxValue = i.taxvalue;
                salesorder.Grandtotal = i.totalprice;
                salesorder.Uomname = i.long_unit_name;
                salesorder.BuyerNames = i.buyer_full_name;
                salesorder.ProductName = i.productname;
                salesorder.ProductCode = i.productcode;
                totaldata.Add(salesorder);
            }
            var salesorderdetails = totaldata.OrderByDescending(m => m.SalesorderId_DT);
            return Json(salesorderdetails, JsonRequestBehavior.AllowGet);
        }
        public ActionResult search(Salesorders models)
        {
            SalesorderHD_Manager salesorderManager = new SalesorderHD_Manager();
            SalesorderDT_Manager salesorderDT_manager = new SalesorderDT_Manager();
            BuyerMasterManager BuyerManager = new BuyerMasterManager();
            var data1 = BuyerManager.Get();
            List<Salesorders> totalList = new List<Salesorders>();
            var data = salesorderManager.Get();
            foreach (var i in data)
            {
                Salesorders model = new Salesorders();
                var salesorderdt = salesorderDT_manager.GetSOIdS(i.salesorderid_hd);
                var counts = 0;
                decimal? dc_qty = 0;
                decimal? Invoice_qty = 0;
                foreach (var k in salesorderdt)
                {
                    if (k.dc_qty != null)
                    {
                        counts++;
                        dc_qty += k.dc_qty;
                    }
                    if (k.Invoice_qty != null)
                    {
                        Invoice_qty += k.Invoice_qty;
                    }
                }
                model.invoice_qty = Invoice_qty;
                model.dc_qty = dc_qty;
                model.SalesorderId = i.salesorderid_hd;
                model.salesorderdate = i.Salesorderdate;
                model.item = i.items;
                model.Status = i.Status;
                model.fullfilldate = i.fullfilldate;
                model.quantity = i.quantity;
                model.Total_Price = i.Total_price;
                model.buyerid = i.customerid;
                model.Total_discountval = i.Total_disamount;
                model.Total_Grandtotal = i.grand_total;
                model.Total_Subtotal = i.Total_subtotal;
                model.Total_TaxValue = i.Total_taxamount;
                model.BuyerMaster = data1.Where(W => W.BuyerMasterId == i.customerid).ToList().FirstOrDefault();
                totalList.Add(model);
            }
            if (models.BuyerName != 0)
            {
                if (models.createddate != null && models.fullfilldate != null)
                {
                    var indentpomappingsps = totalList
.Where(x =>
    x.SalesorderId.ToString().ToLower().Contains(models.salesorderno.ToString().ToLower()) &&
    x.buyerid == models.BuyerName &&
    x.createddate.HasValue && models.createddate.HasValue &&
    x.createddate.Value.Date == models.createddate.Value.Date &&
    x.fullfilldate.HasValue && models.fullfilldate.HasValue &&
    x.fullfilldate.Value.Date == models.fullfilldate.Value.Date &&
    x.Status == models.Status)

    .ToList();

                    return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);
                }
                else if (models .createddate == null && models.fullfilldate == null)
                {
                    if (models.createddate == null && models.fullfilldate == null && models.salesorderno == 0)
                    {
                        var indentpomappingsps = totalList
.Where(x =>
x.buyerid == models.BuyerName &&
x.Status == models.Status)
.ToList();
                        return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);
                    }
                    else if (models.createddate == null && models.fullfilldate == null && models.Status == null)
                    {
                        var indentpomappingsps = totalList
.Where(x =>
x.SalesorderId.ToString().ToLower().Contains(models.salesorderno.ToString().ToLower()) &&
x.buyerid == models.BuyerName)
.ToList();

                        return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var indentpomappingsps = totalList
.Where(x =>
x.SalesorderId.ToString().ToLower().Contains(models.salesorderno.ToString().ToLower()) &&
x.buyerid == models.BuyerName &&
x.Status == models.Status)
.ToList();

                        return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var indentpomappingsps = totalList
.Where(x =>
x.SalesorderId.ToString().ToLower().Contains(models.salesorderno.ToString().ToLower()) &&
x.buyerid == models.BuyerName &&
    x.createddate.HasValue && models.createddate.HasValue &&
    x.createddate.Value.Date == models.createddate.Value.Date &&
x.Status == models.Status)
.ToList();
                    return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(totalList, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Alreadychoosenproduct(int Buyerid, int productName,int Salesorder)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            var data = salesorderManager.Get();

            var filter = data.Where(m => m.customerid == Buyerid && m.ProductNameid == productName && m.Status == 1 && m.isdeleted == true && m.salesordernumber == Salesorder).ToList();
            if (filter.Count == 0)
            {
                return View();
            }
            else
            {
                var status = 1;
                return Json(status, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Getcustomeraddress(int id)
        {
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            var data = custAddressMangers.GetCustAddressbuyerid(id);
            string address1 = "";
            if(data != null)
            {
                var data1 = custAddressMangers.GetCustbuyerid(id ,data.Addresshd_id);
                if(data1 != null)
                {
                    address1 = data1.address1;
                }
            }
            return Json(address1, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Getcustomeraddressship(int id)
        {
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            var data = custAddressMangers.GetCustAddressbuyeridshipp(id);
            string address1 = "";
            if(data != null)
            {
                var data1 = custAddressMangers.GetCustbuyeridship(id ,data.Addresshd_id);
                if(data1 != null)
                {
                    address1 = data1.address1;
                }
            }
            return Json(address1, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Getbuyerorderno(int id)
        {
            SalesorderHD_Manager SalesorderHD_Manager = new SalesorderHD_Manager();
            var data = SalesorderHD_Manager.GettypeId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region PDF Download
        private string InsertLineBreaks(string text, int wordsPerLine)
        {
            var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                if (i > 0 && i % wordsPerLine == 0)
                {
                    sb.Append("<br/>"); // Insert a line break
                }
                sb.Append(words[i] + " ");
            }

            return sb.ToString().Trim(); // Trim to remove the last space
        }

        public byte[] GeneratePDF(Salesorders salesordersinvoice)
        {
            var htmlContent = new StringBuilder();
            string imageURL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/assets/images/creative.png");


            htmlContent.Append("<html><head>");
            htmlContent.Append("<style>");
            htmlContent.Append("body { font-family: Arial, sans-serif; margin: 0; padding: 0; height: 100%; }");
            htmlContent.Append(".main-page h1, h3 { margin: 0; }");
            htmlContent.Append("table { width: 100%; border-collapse: collapse; margin: 10px 0; }");
            htmlContent.Append("th, td { padding: 5px; border: 1px solid black; text-align: left; font-size: 12px; border-bottom: 1px solid #fff;}");
            htmlContent.Append(".bottom1 { border-top: 1px solid black; padding: 8px; }");
            htmlContent.Append(".border2 { border-right: 1px solid black;border-bottom: 1px solid #fff; }");
            htmlContent.Append(".inner-section { padding: 10px; background: #FFFFFF; border-radius: 5px; }");
            htmlContent.Append("#rightline { border-left: 1px solid #fff;border-right: 1px solid #fff;border-top: 1px solid #fff; }");

            htmlContent.Append(".edit-section { overflow: auto; }");
            htmlContent.Append(".image-container { text-align: left; }");
            htmlContent.Append(".image-container img { max-width: 200px; margin-left: 50px; }");
            htmlContent.Append("@media print { @page { size: A4; margin: 10mm; } }");
            htmlContent.Append("</style>");
            htmlContent.Append("</head><body>");
            htmlContent.Append("<div class='page'>");
            htmlContent.Append("<div class='content'>");
            htmlContent.Append("<div class='main-page'>");
            htmlContent.Append("<section class='inner-section'>");
            htmlContent.Append("<div class='edit-section'>");
            htmlContent.Append("<div style='display: flex; justify-content: space-between;'>");
            htmlContent.Append("<div>");
            htmlContent.Append("<table style='width: 100%; border-radius: 10px; border: 1px solid black; border-bottom-width: thick; margin-top: 38px;'>");
            htmlContent.Append("<tr><th>Document Number</th><th style='text-align: center;'>Date</th></tr>");
            htmlContent.Append("<tr><td style='text-align: center;'>").Append(salesordersinvoice.SalesorderId_HD).Append("</td>");
            htmlContent.Append("<td>").Append(salesordersinvoice.salesorderdate.ToString("yyyy-MM-dd")).Append("</td></tr>");
            htmlContent.Append("</table>");
            htmlContent.Append("<h3>Sales Order</h3>");
            htmlContent.Append("</div>");
            htmlContent.Append("<div>");
            htmlContent.Append("<h3><u style='margin-left: 100px;'>Creative Flavors International (PTY) Ltd</u></h3>");
            htmlContent.Append("<div style='display: flex; justify-content: space-evenly;'>");
            htmlContent.Append("<div>");
            htmlContent.Append("<header>");
            htmlContent.Append("<div class='clearfix' style='margin-left: 120px; margin-top: 10px;'>");
            htmlContent.Append("<div style='font-size: 13px;'>").Append(InsertLineBreaks(salesordersinvoice.BuyerAddress, 2)).Append("</div>");
            htmlContent.Append("<div style='font-size: 13px;'>").Append(salesordersinvoice.PhysicalCode).Append("</div>");
            htmlContent.Append("</header>");
            htmlContent.Append("</div>");
            htmlContent.Append("<div>");
            htmlContent.Append("<header>");
            htmlContent.Append("<div class='clearfix' style='margin-left: 40px; margin-top: 10px;'>");
            htmlContent.Append("<div style='font-size: 13px;'><b>Tel:</b> ").Append(salesordersinvoice.teli).Append("</div>");   
            htmlContent.Append("<div style='font-size: 13px;'><b>Reg No:</b> ").Append(salesordersinvoice.regnum).Append("</div>");
            htmlContent.Append("<div style='font-size: 13px;'><b>Vat No:</b> ").Append(salesordersinvoice.vatnum).Append("</div>");
            htmlContent.Append("</header>");
            htmlContent.Append("</div>");
            htmlContent.Append("</div>");
            htmlContent.Append("</div>");
            htmlContent.Append($"<div class='image-container'><img src='file:///{imageURL}' alt='Creative Image' style='float: right;' /></div>");
            htmlContent.Append("</div>");
            htmlContent.Append("<table style='margin-top: 40px';    padding: 17px; >");
            htmlContent.Append("<tr><th style='text-align:left;border-right: 1px solid #fff;'>BUYER NAME:</th><th style='text-align:left;border-right: 1px solid #fff;'> ").Append(salesordersinvoice.BuyerNames).Append("</th><th style='text-align:left;border-right: 1px solid #fff;'>Billing Address:</th><th style='text-align:left'>Shipping Address:</th></tr>");
            htmlContent.Append("<tr><th style='text-align:left;border-right: 1px solid #fff;'>ACCOUNT NO:</th><td style='text-align:left;border-right: 1px solid #fff;'> ").Append(salesordersinvoice.accountno).Append("</td><td style='text-align:left;border-right: 1px solid #fff;'>").Append(InsertLineBreaks(salesordersinvoice.Billingadd, 4)).Append("</td><td style='text-align:left;'>").Append(InsertLineBreaks(salesordersinvoice.shippingadd, 4)).Append("</td></tr>");
            htmlContent.Append("<tr><th style='text-align:left;border-right: 1px solid #fff;'>CONTACT:</th><td style='text-align:left;border-right: 1px solid #fff;'> ").Append(salesordersinvoice.contact).Append("</td><td style='text-align:left;border-right: 1px solid #fff;'></td><td style='text-align:left;'></td></tr>");
            htmlContent.Append("<tr><th style='text-align:left;border-right: 1px solid #fff;'>TELEPHONE:</th><td style='text-align:left;border-right: 1px solid #fff;'> ").Append(salesordersinvoice.teli).Append("</td><td style='text-align:left;border-right: 1px solid #fff;'></td><td style='text-align:left;'></td></tr>");
            htmlContent.Append("<tr><th style='text-align:left;border-bottom: 1px solid black;border-right: 1px solid #fff;'>E-MAIL:</th><td style='text-align:left;border-bottom: 1px solid black;border-right: 1px solid #fff;'> ").Append(salesordersinvoice.email).Append("</td><td style='text-align:left;border-bottom: 1px solid black;border-right: 1px solid #fff;'id='rightline'><b>CODE : </b>").Append(salesordersinvoice.Billingcode).Append("</td><td style='text-align:left;border-bottom: 1px solid black;border-top: 1px solid #fff;'><b>CODE : </b> ").Append(salesordersinvoice.shippingcode).Append("</td></tr>");
            htmlContent.Append("<tr class='bottom1'><td style='border-bottom: 1px solid black;border-right: 1px solid #fff;'></td><td style='border-bottom: 1px solid black;border-right: 1px solid #fff;'><b>Currency:</b>  ").Append(salesordersinvoice.currencyOption).Append("</td><td style='border-bottom: 1px solid black;border-right: 1px solid #fff;'><b>ORDER REFERENCE #:</b></td><td style='border-bottom: 1px solid black;'><b>ACCOUNT TERMS:</b> Current</td></tr>");
            htmlContent.Append("</table>");
            htmlContent.Append("<div style='height: 200px; overflow: auto;'>");
            htmlContent.Append("<table >");
            htmlContent.Append("<thead>");
            htmlContent.Append("<tr><th id='rightline'>PRODUCT CODE</th><th id='rightline'>PRODUCT NAME</th><th id='rightline'>QUANTITY</th><th id='rightline'>UNIT PRICE</th><th id='rightline'>DISCOUNT</th><th id='rightline'>TAX</th><th id='rightline'>NET PRICE</th></tr>");
            htmlContent.Append("</thead>");
            htmlContent.Append("<tbody>");
            htmlContent.Append("<tr><td id='rightline'> ").Append(salesordersinvoice.ProductCode).Append("</td><td id='rightline'>").Append(salesordersinvoice.ProductName).Append(" wefw</td><td style='text-align: center;' id='rightline'> ").Append(salesordersinvoice.quantity).Append(" kg</td><td id='rightline' style='text-align: center;'> $").Append(salesordersinvoice.Price).Append("</td><td id='rightline' style='text-align: center;'> $").Append(salesordersinvoice.discountvalue).Append("</td><td id='rightline' style='text-align: center;'> $").Append(salesordersinvoice.TaxValue).Append("</td><td id='rightline' style='text-align: center;'> $").Append(salesordersinvoice.Grandtotal).Append("</td></tr>");
            htmlContent.Append("</tbody>");
            htmlContent.Append("</table>");
            htmlContent.Append("</div>");
            htmlContent.Append("<table>");
            htmlContent.Append("<tr><th colspan='2' style='text-align:center; border-bottom: 1px solid black;'>ZIP CODE: ").Append(salesordersinvoice.PhysicalCode).Append("</th><th class='border2'>Goods received.</th><th class='border2'>Creative Flavors Control:</th><td>Sub Total : $").Append(salesordersinvoice.Subtotal).Append("</td></tr>");
            htmlContent.Append("<tr><td class='border2'><b>RSA BANKING DETAILS</b></td><td class='border2'><b>INTERNATIONAL BANKING DETAILS</b></td><td class='border2'>Date : _______________</td><td class='border2'>Name : _______________</td><td class='border2'>Discount : $").Append(salesordersinvoice.discountvalue).Append("</td></tr>");
            htmlContent.Append("<tr><td class='border2'>").Append(salesordersinvoice.accountname).Append("</td><td class='border2'>").Append(salesordersinvoice.accountname).Append("</td><td class='border2'>Signed : ______________</td><td class='border2'>Signed : ______________</td><td class='border2'>Unit Price : $").Append(salesordersinvoice.Price).Append("</td></tr>");
            htmlContent.Append("<tr><td class='border2'>Account : ").Append(salesordersinvoice.accountno).Append("</td><td class='border2'>Account :").Append(salesordersinvoice.accountno).Append("</td><td class='border2'>Name : _______________</td><td class='border2'></td><td class='border2'>Tax : $").Append(salesordersinvoice.TaxValue).Append("</td></tr>");
            htmlContent.Append("<tr><td style='border-bottom: 1px solid #58606d;' class='border2'>Switch Code : ").Append(salesordersinvoice.SwiftCode).Append("</td><td style='border-bottom: 1px solid #58606d;' class='border2'>Switch Code : ").Append(salesordersinvoice.SwiftCode).Append("</td><td style='border-bottom: 1px solid #58606d;' class='border2'></td><td style='border-bottom: 1px solid #58606d;' class='border2'></td><td style='border-bottom: 1px solid #58606d;' class='border2'><b>TOTAL</b> : <b> $").Append(salesordersinvoice.Grandtotal).Append("</b></td></tr>");
            htmlContent.Append("</table>");
            htmlContent.Append("</div>");
            htmlContent.Append("</section>");
            htmlContent.Append("</div>");
            htmlContent.Append("</div>");
            htmlContent.Append("</div>");
            htmlContent.Append("</body></html>");


            using (var memoryStream = new MemoryStream())
            {
                using (var pdfWriter = new PdfWriter(memoryStream))
                {
                    // Create a PDF document with custom page size
                    var pdfDocument = new PdfDocument(pdfWriter);

                    // Define custom page size (e.g., A4 size with increased width)
                    var pageSize = new iText.Kernel.Geom.PageSize(750, 660); // width, height in points
                    var document = new Document(pdfDocument, pageSize);

                    using (var htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(htmlContent.ToString())))
                    {
                        // Convert HTML to PDF and write to the document
                        HtmlConverter.ConvertToPdf(htmlStream, pdfDocument);
                    }

                    document.Close();
                }

                // Return the PDF as a byte array
                return memoryStream.ToArray();
            }
        }



        [HttpGet]
        public ActionResult OrderDetailDownload(int id)
        {
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            BuyerMasterManager _buyerMasterManager = new BuyerMasterManager();
            CurrencyManager Manager = new CurrencyManager();

            var singleInvoice = salesorderManager.salesorder_Grid().FirstOrDefault(x => x.salesorderid_dt == id);
            if (singleInvoice != null)
            {
                Salesorders salesorders = new Salesorders
                {
                    SalesorderId = singleInvoice.salesorderid,
                    salesorderdate = singleInvoice.salesorderdate,
                    SalesorderId_DT = singleInvoice.salesorderid_dt,
                    SalesorderId_HD = singleInvoice.salesorderid_hd,
                    quantity = singleInvoice.quantity,
                    discountvalue = singleInvoice.discount_value,
                    Subtotal = singleInvoice.subtotal,
                    TaxValue = singleInvoice.taxvalue,
                    Grandtotal = singleInvoice.totalprice,
                    Price = singleInvoice.unitprice,
                    Uomname = singleInvoice.long_unit_name,
                    BuyerNames = singleInvoice.buyer_full_name,
                    ProductName = singleInvoice.productname,
                    ProductCode = singleInvoice.productcode
                };


                var buyermanager = _buyerMasterManager.GetSingleBuyerModel(singleInvoice.Buyerid);
                if (buyermanager != null)
                {
                    CustAddressMangers custAddressMangers = new CustAddressMangers();
                    var data12 = custAddressMangers.GetCustbuyeridship(buyermanager.BuyerMasterId, int.Parse(singleInvoice.shippingadd));
                    if (data12 != null)
                    {
                        salesorders.shippingadd = data12.address1;
                        salesorders.shippingcode = data12.ZipCode;
                    }
                    var data2 = custAddressMangers.GetCustbuyerid(buyermanager.BuyerMasterId, int.Parse(singleInvoice.billingadd));
                    if (data2 != null)
                    {
                        salesorders.Billingadd = data2.address1;
                        salesorders.Billingcode = data2.ZipCode;

                    }
                    var currency = Manager.Getcurrency().Where(m => m.id == buyermanager.ForeignCurrency).FirstOrDefault();
                    salesorders.teli = buyermanager.Telephone1;
                    salesorders.regnum = buyermanager.RegNumber;
                    salesorders.vatnum = buyermanager.VatNumber;
                    salesorders.BuyerAddress = buyermanager.Physical1;
                    salesorders.PhysicalCode = buyermanager.PhysicalCode;
                    salesorders.accountno = buyermanager.Account;
                    salesorders.contact = buyermanager.EmailContact;
                    salesorders.SwiftCode = buyermanager.SwiftCode;
                    salesorders.email = buyermanager.EmailEmergency;
                    salesorders.accountname = buyermanager.AccountName;
                    salesorders.branch = buyermanager.AccountDescription;
                    salesorders.currencyOption = currency.currencyname;
                }

                // Generate the PDF
                var pdfFile = GeneratePDF(salesorders);

                // Store the PDF file in session
                Session["File"] = pdfFile;

                // Convert the PDF file to a base64 string
                string pdfBase64 = Convert.ToBase64String(pdfFile);
                return Json(new { success = true, pdfFile = pdfBase64, fileName = "salesorderInvoice.pdf" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Invoice not found" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadInvoice()
        {
            // Retrieve the PDF file from the session
            byte[] pdfFile = (byte[])Session["File"];

            if (pdfFile == null)
            {
                return HttpNotFound("File not found");
            }
            // Return the PDF file as a FileContentResult
            return File(pdfFile, "application/pdf", "SalesOrder.pdf");
        }

        #endregion
    }


}
