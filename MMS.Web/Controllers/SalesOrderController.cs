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
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

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
        public ActionResult SalesOrderGrid(int page = 1, int pageSize = 8)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            UOMManager uOMManager = new UOMManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            BuyerManager buyerManager = new BuyerManager();

            var totaldata = from s in salesorderManager.Get()
                            join p in productManager.Get() on s.ProductNameid equals p.ProductId
                            join u in uOMManager.Get() on s.UomMasterId equals u.UomMasterId
                            join b in buyerManager.Get() on s.customerid equals b.BuyerMasterId
                            select new Salesorders
                            {
                                quantity = s.quantity,
                                discountvalue = s.Discountvalue,
                                Subtotal = s.Subtotal,
                                TaxValue = s.TaxValue,
                                Grandtotal = s.Grandtotal,
                                SalesorderId = s.SalesorderId,
                                UomMaster = new UomMaster
                                {
                                    LongUnitName = u.LongUnitName,
                                },
                                BuyerMaster = new BuyerMaster
                                {
                                    BuyerFullName = b.BuyerFullName
                                },
                                product = new product
                                {
                                    ProductName = p.ProductName
                                },
                            };


            var totalCount = totaldata.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totaldata = totaldata.OrderByDescending(s => s.SalesorderId)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return PartialView("partial/SalesOrderGrid", totaldata);
        }
        [HttpGet]
        public ActionResult SalesOrderDetails()
        {
            return View("SalesOrderDetails");
        }
        public ActionResult SalesOrderQtycheck(int? id)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            BuyerManager buyerManager = new BuyerManager();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            MaterialOpeningStockManager materialOpeningStockManager = new MaterialOpeningStockManager();
            BOMMaterialListManager bOMMaterialListManager = new BOMMaterialListManager();
            UOMManager uOMManager = new UOMManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialNameManager MaterialNameManager = new MaterialNameManager();
            var st = salesorderManager.GetSO(id);
            var bomno = productManager.GetId(st.ProductNameid);
            var list = billOfMaterialManager.GetbomId(bomno.BomNo);
            var bommaterial = bOMMaterialListManager.BOMMaterialID(list.BomId);

            List<BOMMaterialListModel> totalList = new List<BOMMaterialListModel>();

            foreach (var item in bommaterial)
            {

                BOMMaterialListModel availablestock = new BOMMaterialListModel();

                availablestock.RequiredQty = item.RequiredQty;

                availablestock.MaterialOpeningMaster = (from m in materialOpeningStockManager.Get()
                                                        join mm in MaterialNameManager.Get() on m.MaterialMasterId equals mm.MaterialMasterID
                                                        join uom in uOMManager.Get() on m.PrimaryUomMasterId equals uom.UomMasterId
                                                        where m.MaterialMasterId == item.MaterialName
                                                        select new MaterialOpeningModel
                                                        {
                                                            MaterialMasterId = m.MaterialMasterId,
                                                            PrimaryUomMasterId = m.PrimaryUomMasterId,
                                                            Qty = m.Qty,
                                                            MaterialNames = mm.MaterialDescription,
                                                            UOMName = uom.LongUnitName
                                                        }).ToList().FirstOrDefault();

                totalList.Add(availablestock);
            }

            ViewBag.TotalList = totalList;

            var totaldata = (from s in salesorderManager.Get()
                             join p in productManager.Get() on s.ProductNameid equals p.ProductId
                             join b in buyerManager.Get() on s.customerid equals b.BuyerMasterId
                             where s.SalesorderId == id
                             select new Salesorders
                             {
                                 quantity = s.quantity,
                                 Grandtotal = s.Grandtotal,
                                 SalesorderId = s.SalesorderId,
                                 salesorderdate = s.salesorderdate,
                                 BuyerMaster = new BuyerMaster
                                 {
                                     BuyerFullName = b.BuyerFullName
                                 },
                                 product = new product
                                 {
                                     ProductName = p.ProductName,
                                     BomNo = p.BomNo,
                                 },
                             }).FirstOrDefault();


            return PartialView("partial/SalesOrderQtycheck", totaldata);
        }
        [HttpPost]
        public ActionResult Tempsalesorder(int id)
        {
            Temp_salesorderManager temp_SalesorderManager1 = new Temp_salesorderManager();
            var st1 = temp_SalesorderManager1.GetSO(id);
            if(st1 != null)
            {
                return Json(new { data = "Failer" }, JsonRequestBehavior.AllowGet);
            }
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterialListManager bOMMaterialListManager = new BOMMaterialListManager();
            var st = salesorderManager.GetSO(id);
            var product = productManager.GetId(st.ProductNameid);
            var list = billOfMaterialManager.GetbomId(product.BomNo);
            var bommaterial = bOMMaterialListManager.BOMMaterialID(list.BomId);
            Temp_salesorder temp_Salesorder = new Temp_salesorder();
            foreach (var item in bommaterial)
            {
                MaterialOpeningStockManager materialOpeningStockManager = new MaterialOpeningStockManager();
                Temp_salesorderManager temp_SalesorderManager = new Temp_salesorderManager();
                var MaterialOpeningMaster = materialOpeningStockManager.GetmaterialOpeningMaterialID(item.MaterialName);
                temp_Salesorder.SalesOrderId = id;
                temp_Salesorder.BuyerId = st.customerid;
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
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterialListManager bOMMaterialListManager = new BOMMaterialListManager();
            var st = salesorderManager.GetSO(id);
            var product = productManager.GetId(st.ProductNameid);
            var list = billOfMaterialManager.GetbomId(product.BomNo);
            var bommaterial = bOMMaterialListManager.BOMMaterialID(list.BomId);
            temp_production temp_production = new temp_production();
            foreach (var item in bommaterial)
            {
                MaterialOpeningStockManager materialOpeningStockManager = new MaterialOpeningStockManager();
                Temp_productionManager Temp_productionManagers = new Temp_productionManager();
                var MaterialOpeningMaster = materialOpeningStockManager.GetmaterialOpeningMaterialID(item.MaterialName);
                temp_production.SalesOrderId = id;
                temp_production.BuyerId = st.customerid;
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
        public ActionResult Getcustomeraddress(int id)
        {
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            var data = custAddressMangers.GetCustAddressbuyerid(id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SalesorderDetails(Salesorders model)
        {
            salesorder salesorder = new salesorder();
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            BuyerManager buyerManager = new BuyerManager();
            var product = productManager.GetId(model.ProductID);
            var tax = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
            var data = custAddressMangers.GetCustAddressbuyerid(model.buyerid);

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
            salesorder.taxinclusive = true;
            salesorder.isactive = true;
            var taxper = tax.TaxValue;
            var qty = model.quantity;
            var discount = model.discountperid;
            int intVal = int.Parse(taxper);

            if ((model.quantity != 0) && (discount != 0))
            {
                var subtotal = qty * product.Price;
                var disamount = subtotal * discount / 100;
                var subtotals = subtotal - disamount;
                var taxamount1 = subtotals * intVal / 100;
                var total = taxamount1 + subtotals;
                salesorder.Subtotal = subtotals;
                salesorder.TaxValue = taxamount1;
                salesorder.Grandtotal = total;
                salesorder.Discountvalue = disamount;
            }
            else if (model.quantity != 0)
            {
                var subtotal = qty * product.Price;
                var taxamount = subtotal * intVal / 100;
                var totalprice = taxamount + subtotal;
                var disamount = subtotal * discount / 100;
                salesorder.Subtotal = subtotal;
                salesorder.TaxValue = taxamount;
                salesorder.Grandtotal = totalprice;
                salesorder.Discountvalue = disamount;
            }
            salesorderManager.Post(salesorder);
            return View();
        }
        public ActionResult searchs(int id)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            ProductManager productManager = new ProductManager();
            UOMManager uOMManager = new UOMManager();
            BuyerManager buyerManager = new BuyerManager();

            var totaldata = (from s in salesorderManager.Get()
                             join p in productManager.Get() on s.ProductNameid equals p.ProductId
                             join u in uOMManager.Get() on s.UomMasterId equals u.UomMasterId
                             join b in buyerManager.Get() on s.customerid equals b.BuyerMasterId
                             where s.customerid == id
                             select new Salesorders
                             {
                                 quantity = s.quantity,
                                 discountvalue = s.Discountvalue,
                                 Subtotal = s.Subtotal,
                                 TaxValue = s.TaxValue,
                                 Grandtotal = s.Grandtotal,
                                 SalesorderId = s.SalesorderId,
                                 UomMaster = new UomMaster
                                 {
                                     LongUnitName = u.LongUnitName,
                                 },
                                 BuyerMaster = new BuyerMaster
                                 {
                                     BuyerFullName = b.BuyerFullName
                                 },
                                 product = new product
                                 {
                                     ProductName = p.ProductName
                                 },
                             }).ToList();

            return Json(totaldata, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Alreadychoosenproduct(int Buyerid, int productName)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            var data = salesorderManager.Get();

            var filter = data.Where(m => m.customerid == Buyerid && m.ProductNameid == productName).ToList();
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
    }
}
