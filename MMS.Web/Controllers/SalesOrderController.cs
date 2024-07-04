using Excel.Log;
using iTextSharp.text;
using Microsoft.Ajax.Utilities;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models;
using MMS.Web.Models.Addressdetails;
using MMS.Web.Models.CustomerTransaction;
using MMS.Web.Models.StockModel;
using MMS.Web.Models.TempsalesorderModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using static iTextSharp.text.pdf.AcroFields;

namespace MMS.Web.Controllers
{
    public class SalesOrderController : Controller
    {
        #region SalesOrder View
        [HttpGet]

        public ActionResult SalesOrderMaster()
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            var bOMMaterial = salesorderManager.Putstatus();
            return View();
        }
        [HttpGet]
        public ActionResult SalesOrderGrid(int page = 1, int pageSize = 8)
        {
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            List<Salesorders> totaldata = new List<Salesorders>();

            var totallist = salesorderManager.salesorder_Grid();
            foreach (var i in totallist)
            {
                Salesorders salesorder = new Salesorders();
                salesorder.SalesorderId = i.salesorderid;
                salesorder.salesorderdate = i.salesorderdate;
                salesorder.SalesorderId_DT = i.salesorderid_dt;
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
            var totalCount = totaldata.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totaldata = totaldata.OrderByDescending(s => s.SalesorderId_DT)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return PartialView("partial/SalesOrderDetailsGrid", totaldata);
        }
        [HttpGet]
        public ActionResult salesorderheader(int page = 1, int pageSize = 8)
        {
            SalesorderHD_Manager salesorderManager = new SalesorderHD_Manager();
            SalesorderDT_Manager SalesorderDT_Manager = new SalesorderDT_Manager();
            BuyerManager BuyerManager = new BuyerManager();
            var data1 = BuyerManager.Get();
            List<Salesorders> totalList = new List<Salesorders>();
            var data = salesorderManager.Get();
            foreach (var i in data)
            {
                Salesorders model = new Salesorders();
                var salesorderdt = SalesorderDT_Manager.GetSOIdS(i.salesorderid_hd);
                var counts = 0;
                decimal? dc_qty = 0;
                foreach (var k in salesorderdt)
                {
                    if(k.dc_qty != null)
                    {
                        counts++;
                        dc_qty += k.dc_qty;
                    }
                }
                model.dc_qty = dc_qty;
                model.itemdc = counts;
                model.SalesorderId_HD = i.salesorderid_hd;
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
            totalList = totalList.OrderByDescending(s => s.SalesorderId_HD)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return PartialView("partial/SalesOrderHeaderGrid", totalList);
        }
        [HttpGet]
        public ActionResult SalesOrderDetails(int id = 0)
        {
            Salesorders model = new Salesorders();
            SalesorderManager salesorderManager = new SalesorderManager();

            if (id == 0)
            {
                var bOMMaterial = salesorderManager.Putstatus();
            }
            else
            {
                var datas = salesorderManager.GetsalesorderList(id);
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
                model.Total_Price = price;
                model.Total_Subtotal = subtotal;
                model.Total_discountval = discount;
                model.Total_TaxValue = tax;
                model.Total_Grandtotal = grandtotal;
                model.BuyerName = id;
                model.salesorderList = datas;
            }
            return View("SalesOrderDetails", model);
        }
        public ActionResult SalesOrderQtycheck(int id)
        {
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            SalesorderHD_Manager salesorderHD_manager = new SalesorderHD_Manager();
            var st = salesorderManager.GetSO(id);
            var mrp_Material_Lists = salesorderManager.GetmrpmaterialList(st.productid);

            List<ParentBillofMaterial> totalList = new List<ParentBillofMaterial>();

            foreach (var item in mrp_Material_Lists)
            {
                ParentBillofMaterial availablestock = new ParentBillofMaterial();
                availablestock.MaterialNames = item.MaterialNames;
                availablestock.Requiredqty = item.RequiredQty;
                availablestock.availablestock = item.AvailableStock;
                availablestock.UOMName = item.UOMName;
                availablestock.MaterialMasterid = item.MaterialMasterId;
                totalList.Add(availablestock);
            }

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
            salesorders.BuyerNames = i.buyer_full_name;
            salesorders.Bomno = i.bomno;

            salesorders.bOMMaterialListModels = totalList;


            return PartialView("partial/SalesOrderQtycheck", salesorders);
        }
        [HttpPost]
        public ActionResult Tempsalesorder(int id)
        {
            Temp_salesorderManager temp_SalesorderManager1 = new Temp_salesorderManager();
            var st1 = temp_SalesorderManager1.GetSO(id);
            if (st1 != null)
            {
                return Json(new { data = "Failer" }, JsonRequestBehavior.AllowGet);
            }
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            ProductManager productManager = new ProductManager();
            Parentbom_materialManager bOMMaterialListManager = new Parentbom_materialManager();
            var st = salesorderManager.GetSO(id);
            var product = productManager.GetId(st.productid);
            var bommaterial = bOMMaterialListManager.GetMaterialList(product.BomNo);
            Temp_Indent temp_Salesorder = new Temp_Indent();
            foreach (var item in bommaterial)
            {
                MaterialOpeningStockManager materialOpeningStockManager = new MaterialOpeningStockManager();
                Temp_salesorderManager temp_SalesorderManager = new Temp_salesorderManager();
                var MaterialOpeningMaster = materialOpeningStockManager.GetmaterialOpeningMaterialID(item.MaterialMasterId);
                temp_Salesorder.SalesOrderId = id;
                temp_Salesorder.BuyerId = st.Customerid;
                temp_Salesorder.ProductId = product.ProductId;
                temp_Salesorder.ProductItem = st.quantity;
                if (MaterialOpeningMaster != null)
                {
                    temp_Salesorder.MaterialId = MaterialOpeningMaster.MaterialMasterId;
                    temp_Salesorder.AvailableStock = MaterialOpeningMaster.Qty;
                    temp_Salesorder.AvailableUnitId = MaterialOpeningMaster.PrimaryUomMasterId;
                    var consume = st.quantity * item.RequiredQty;
                    temp_Salesorder.Consume = consume;
                    temp_Salesorder.ConsumeUnitId = MaterialOpeningMaster.PrimaryUomMasterId;
                    var stockRequired = consume - MaterialOpeningMaster.Qty;
                    temp_Salesorder.stockRequired = stockRequired;
                    if (MaterialOpeningMaster.Qty <= consume)
                    {
                        temp_SalesorderManager.Post(temp_Salesorder);
                    }
                }
            }
            var data = "Success";

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TempProductionstock(int id)
        {
            Temp_productionManager Temp_productionManager = new Temp_productionManager();
            var st1 = Temp_productionManager.GetPro(id);
            if (st1 != null)
            {
                return Json(new { data = "Failer" }, JsonRequestBehavior.AllowGet);
            }
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            ProductManager productManager = new ProductManager();
            Parentbom_materialManager bOMMaterialListManager = new Parentbom_materialManager();
            var st = salesorderManager.GetSO(id);
            var product = productManager.GetId(st.productid);
            var bommaterial = bOMMaterialListManager.GetMaterialList(product.BomNo);
            temp_production temp_production = new temp_production();
            preproduction preproduction = new preproduction();
            foreach (var item in bommaterial)

            {
                MaterialOpeningStockManager materialOpeningStockManager = new MaterialOpeningStockManager();
                Temp_productionManager Temp_productionManagers = new Temp_productionManager();
                var MaterialOpeningMaster = materialOpeningStockManager.GetmaterialOpeningMaterialID(item.MaterialMasterId);
                temp_production.SalesOrderId = id;
                preproduction.SalesOrderNo = id;
                temp_production.BuyerId = st.Customerid;
                temp_production.ProductId = product.ProductId;
                temp_production.ProductItem = st.quantity;
                temp_production.Bomid = product.BomNo;
                if (MaterialOpeningMaster != null)
                {
                    temp_production.MaterialId = MaterialOpeningMaster.MaterialMasterId;
                    temp_production.AvailableStock = MaterialOpeningMaster.Qty;
                    temp_production.AvailableUnitId = MaterialOpeningMaster.PrimaryUomMasterId;
                    var consume = st.quantity * item.RequiredQty;
                    temp_production.Consume = consume;
                    temp_production.ConsumeUnitId = MaterialOpeningMaster.PrimaryUomMasterId;
                    preproduction.ProductId = product.ProductId;
                    preproduction.SalesOrderDate = st.salesorderdate;
                    preproduction.BuyerId = st.Customerid;
                    preproduction.Qty = st.quantity;
                    preproduction.Materialid = MaterialOpeningMaster.MaterialMasterId;
                    Temp_productionManagers.Postpreproduction(preproduction);
                    Temp_productionManagers.Post(temp_production);

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
                                     LongUnitName = d1.LongUnitName,
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
            var discount = model.discountperid;
            var unitprice = product.Price * model.ConversionValue;
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

            var datas = salesorderManager.GetsalesorderList(model.buyerid);
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
        [HttpPost]
        public ActionResult SalesorderDetails(Salesorders model)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            salesorder salesorder = new salesorder();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            BuyerManager buyerManager = new BuyerManager();

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
            salesorder.custbillcode = addreddcode.BuyerAddress1;
            salesorder.custshipcode = addreddcode.BuyerAddress2;
            var unitprice = product.Price * model.ConversionValue;
            salesorder.taxinclusive = true;
            salesorder.isactive = true;
            var taxper = tax.TaxValue;
            var qty = model.quantity;
            var discount = model.discountperid;
            int intVal = int.Parse(taxper);
            string AlertMessage = "";
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
            return Json(new { customerid = salesorder.customerid, AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ConfirmSalesorder(Salesorders model)
        {
            var AlertMessage = "";

            SalesorderManager salesorderManager = new SalesorderManager();
            SalesorderHD_Manager salesorderHD_Manager = new SalesorderHD_Manager();
            SalesorderDT_Manager salesorderDT = new SalesorderDT_Manager();
            CurrencyManager currencyManager = new CurrencyManager();
            salesorder salesorder = new salesorder();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            BuyerManager buyerManager = new BuyerManager();
            var productlist = salesorderManager.GetsalesorderCartList(model.buyerid);
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
        [HttpDelete]
        public ActionResult SODelete(int SOId)
        {
            SalesorderManager SalesorderManager = new SalesorderManager();
            string AlertMessage = "";
            salesorder parentbom = new salesorder();
            parentbom = SalesorderManager.GetSO(SOId);
            if (parentbom != null)
            {
                AlertMessage = "Success";
                SalesorderManager.Delete(parentbom.SalesorderId);
            }
            return Json(new { AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult searchs(int customerid, int SOid)
        {
            SalesorderDT_Manager salesorderManager = new SalesorderDT_Manager();
            List<Salesorders> totaldata = new List<Salesorders>();
            var totallist = salesorderManager.salesorder_Grid();

            var filteredList = totallist.Where(i => i.Buyerid == customerid || i.salesorderid == SOid);

            foreach (var i in filteredList)
            {
                Salesorders salesorder = new Salesorders();
                salesorder.SalesorderId = i.salesorderid;
                salesorder.salesorderdate = i.salesorderdate;
                salesorder.SalesorderId_DT = i.salesorderid_dt;
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

            return Json(totaldata, JsonRequestBehavior.AllowGet);
        }
        public ActionResult search(int customerid, int SOid)
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
                model.buyerid = i.customerid;
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
        public ActionResult Alreadychoosenproduct(int Buyerid, int productName)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            var data = salesorderManager.Get();

            var filter = data.Where(m => m.customerid == Buyerid && m.ProductNameid == productName && m.Status == 1 && m.isdeleted == true).ToList();
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

        #endregion
        #region filter
        public ActionResult Getcustomeraddress(int id)
        {
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            var data = custAddressMangers.GetCustAddressbuyerid(id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Getbuyerorderno(int id)
        {
            SalesorderHD_Manager SalesorderHD_Manager = new SalesorderHD_Manager();
            var data = SalesorderHD_Manager.GettypeId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult buyernamesearch(string filter)
        {
            List<BuyerMaster> buyerMasters = new List<BuyerMaster>();
            BuyerManager buyerManager = new BuyerManager();
            var data = buyerManager.Get();
            buyerMasters = data.Where(x => x.BuyerFullName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            var Addressdetailslist = buyerMasters;
            return Json(Addressdetailslist, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
