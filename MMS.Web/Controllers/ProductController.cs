using MMS.Core.Entities;
using MMS.Repository.Managers.StockManager;
using MMS.Repository.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.SupplierTransaction;
using System.Globalization;
using MMS.Web.Models.Product;
using MMS.Web.Models.MaterailCategory;
using static iTextSharp.tool.xml.html.HTML;
using MMS.Core.Entities.Stock;
using static iTextSharp.text.pdf.PRTokeniser;
using MMS.Web.Models.CustomerTransaction;
using MMS.Web.Models.BuyerOrderCreationModel;
using Excel.Log;
using MMS.Web.Models.StockModel;

namespace MMS.Web.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult ProductGrid(int page = 1, int pageSize = 8)
        {
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            UOMManager uOMManager = new UOMManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            ProductManager manager = new ProductManager();
            var cate = materialCategoryManager.Get();
            var products = manager.Get();
            var data1 = uOMManager.Get();
            var data2 = taxTypeManager.Get();
            var bom= billOfMaterialManager.Get();
            List<Product> totalList = new List<Product>();

            foreach (var item in products)
            {
                Product customertransaction = new Product();

                customertransaction.ProductCode = item.ProductCode;
                customertransaction.ProductName = item.ProductName;
                customertransaction.ProductDesc = item.ProductDesc;
                customertransaction.TaxMasterId = item.TaxMasterId;
                customertransaction.UomMasterId = item.UomMasterId;
                customertransaction.Price = item.Price;
                customertransaction.UomMasterId = item.UomMasterId;
                customertransaction.BomNo = item.BomNo;
                customertransaction.Bom = bom.Where(W => W.BomId == item.BomNo).ToList().FirstOrDefault();
                customertransaction.materailCategory = cate.Where(W => W.MaterialCategoryMasterId == item.MaterialCategoryMasterId).ToList().FirstOrDefault();
                customertransaction.UomMaster = data1.Where(W => W.UomMasterId == item.UomMasterId).ToList().FirstOrDefault();
                customertransaction.TaxTypeMaster = data2.Where(W => W.TaxMasterID == item.TaxMasterId).ToList().FirstOrDefault();
                totalList.Add(customertransaction);

            }
            var totalCount = totalList.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalList = totalList.OrderByDescending(d => d.ProductId)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            ViewBag.Productslist = totalList;
            return View();
        }

        [HttpGet]
        public ActionResult Product()
        {

            return PartialView();
        }

        [HttpPost]
        public ActionResult ProductDataInsert(Product model)
        {
            product productEntity = new product();
            string status = "";
            if (model.ProductId == 0)
            {
                ProductManager productManager = new ProductManager();

                productEntity.ProductId = model.ProductId;
                productEntity.ProductCode = model.ProductCode;
                productEntity.ProductName = model.ProductName;
                productEntity.ProductDesc = model.ProductDesc;
                productEntity.TaxMasterId = model.TaxMasterId;
                productEntity.UomMasterId = model.UomMasterId;
                productEntity.Price = model.Price;
                productEntity.BomNo = model.BomNo;
                //productEntity.ImageName = model.ImageName;
                //productEntity.ImagePath = model.ImagePath;
                productEntity.IsActive = true;
                productEntity.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
                productEntity.CreatedDate = DateTime.Now;
                //productEntity.UpdatedDate = DateTime.Now;
                productEntity.CreatedBy = model.CreatedBy;
                productEntity.UpdatedBy = model.UpdatedBy;

                productEntity.ProductType = model.ProductType;
                productEntity.ProductionTime = model.productiontime;
                productEntity.ProductionPerDay = model.productionperday;
                productEntity.StoreId = model.StoreId;
                productEntity.MinStock = 0;
                productEntity.MaxStock = 0;
                //DateTime grnDate = DateTime.ParseExact(model.GrnDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //string formattedGrnDate = grnDate.ToString("yyyy-MM-dd");
                //supplierTransactionEntity.GrnDate = formattedGrnDate;

                productManager.Post(productEntity);
                status = "Inserted";
            }
            if (status == "Inserted" && productEntity.ProductId > 0 && productEntity.ProductType == 2)
            {
                
            }
            //else
            //{
            //    product productEntity = new product();

            //    ProductManager productManager = new ProductManager();


            //    productEntity.ProductId = model.ProductId;
            //    productEntity.ProductCode = model.ProductCode;
            //    productEntity.ProductName = model.ProductName;
            //    productEntity.ProductDesc = model.ProductDesc;
            //    productEntity.TaxMasterId = model.TaxMasterId;
            //    productEntity.UomMasterId = model.UomMasterId;
            //    productEntity.Price = model.Price;
            //    productEntity.BomNo = model.BomNo;
            //    //productEntity.ImageName = model.ImageName;
            //    //productEntity.ImagePath = model.ImagePath;
            //    productEntity.IsActive = true;
            //    productEntity.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
            //    productEntity.CreatedDate = DateTime.Now;
            //    //productEntity.UpdatedDate = DateTime.Now;
            //    productEntity.CreatedBy = model.CreatedBy;
            //    productEntity.UpdatedBy = model.UpdatedBy;

            //    //DateTime grnDate = DateTime.ParseExact(model.GrnDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //string formattedGrnDate = grnDate.ToString("yyyy-MM-dd");
            //    //supplierTransactionEntity.GrnDate = formattedGrnDate;

            //    productManager.Put(productEntity);
            //    status = "Updated";
            //}



            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Productupdate(int productid)
        {
            Product model = new Product();

            ProductManager manager = new ProductManager();

            var data = manager.GetId(productid);

            model.ProductId = data.ProductId;
            model.ProductCode = data.ProductCode;
            model.ProductName = data.ProductName;
            model.ProductDesc = data.ProductDesc;
            model.TaxMasterId = data.TaxMasterId;
            model.UomMasterId = data.UomMasterId;
            model.Price = data.Price;
            model.BomNo = data.BomNo;
            model.IsActive = data.IsActive;
            model.MaterialCategoryMasterId = data.MaterialCategoryMasterId;
            model.ProductType = data.ProductType;
            model.productiontime = data.ProductionTime;
            model.productionperday = data.ProductionPerDay ?? 0;
            return PartialView("Product", model);

        }

        public ActionResult ProductDelete(int productid)
        {
            product products = new product();
            string status = "";
            ProductManager manager = new ProductManager();
            products = manager.GetId(productid);
            if (products.ProductId == productid)
            {
                status = "Success";
                manager.Delete(productid);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ProductSearch(string filter)
        {
            List<product> productList = new List<product>();
            ProductManager manager = new ProductManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            UOMManager uOMManager = new UOMManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            productList = manager.Get();
            productList = productList.Where(x => x.ProductName.ToLower().Contains(filter.ToLower()) || x.ProductCode.ToLower().Contains(filter.ToLower())).ToList();

            var products = manager.Get();
            var data1 = uOMManager.Get();
            var data2 = taxTypeManager.Get();
            List<Product> totalList = new List<Product>();

            foreach (var item in products)
            {
                Product customertransaction = new Product();

                customertransaction.MaterialCategoryMasterId = item.MaterialCategoryMasterId;
                customertransaction.ProductCode = item.ProductCode;
                customertransaction.ProductName = item.ProductName;
                customertransaction.ProductDesc = item.ProductDesc;
                customertransaction.TaxMasterId = item.TaxMasterId;
                customertransaction.UomMasterId = item.UomMasterId;
                customertransaction.Price = item.Price;
                //customertransaction.BomNo = item.BomNo;
                customertransaction.UomMasterId = item.UomMasterId;
                customertransaction.UomMaster = data1.Where(W => W.UomMasterId == item.UomMasterId).ToList().FirstOrDefault();
                customertransaction.TaxTypeMaster = data2.Where(W => W.TaxMasterID == item.TaxMasterId).ToList().FirstOrDefault();
                totalList.Add(customertransaction);

            }
            return Json(productList, JsonRequestBehavior.AllowGet);
        }
    }

}

