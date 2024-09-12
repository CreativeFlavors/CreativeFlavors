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
using MMS.Web.Models;

namespace MMS.Web.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Productmaster()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ProductGrid(int page = 1, int pageSize = 15)
        {
            ProductManager ProductManager = new ProductManager();
            List<Product> totaldata = new List<Product>();

            var totallist = ProductManager.Product_Grid();
            foreach (var i in totallist)
            {
                Product product = new Product();
                product.CategoryName = i.CategoryName;
                product.ProductCode = i.product_code;
                product.ProductName = i.product_name;
                product.ProductDesc = i.product_desc;
                product.ProductId = i.ProductId;
                product.taxname = i.TaxValue;
                product.uom= i.uom;
                product.Price = i.Price;
                product.BomNo1 = i.bom_no;
                totaldata.Add(product);
            }
            var totalCount = totaldata.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totaldata = totaldata.OrderByDescending(d => d.ProductId)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            return PartialView("Partial/ProductGrid", totaldata);
        }

        [HttpGet]
        public ActionResult Product()
        {
            return PartialView("/Views/Product/Partial/Product.cshtml");
        }
        [HttpPost]
        public ActionResult ProductDataInsert(Product model)
        {
            product productEntity = new product();
            string status = "";
            ProductManager ProductManager = new ProductManager();

            if (model.ProductId == 0)
            {
                var totallist = ProductManager.Product_Grid();
                var productList = totallist.Where(x => x.product_name.ToLower().Contains(model.ProductName.ToLower()) && x.Productype.ToString().ToLower().Contains(model.ProductType.ToString().ToLower())).ToList();
                if (productList.Count() != 0)
                {
                    status = "ProductThere";
                    return Json(status, JsonRequestBehavior.AllowGet);
                }
                var productListcode = totallist.Where(x => x.product_code.ToLower().Contains(model.ProductCode.ToLower()) && x.Productype.ToString().ToLower().Contains(model.ProductType.ToString().ToLower())).ToList();
                if (productList.Count() != 0)
                {
                    status = "ProductcodetThere";
                    return Json(status, JsonRequestBehavior.AllowGet);
                }
                ProductManager productManager = new ProductManager();
                productEntity.ProductCode = model.ProductCode;
                productEntity.ProductName = model.ProductName;
                productEntity.ProductDesc = model.ProductDesc;
                productEntity.TaxMasterId = model.TaxMasterId;
                productEntity.cost = model.cost;
                productEntity.UomMasterId = model.UomMasterId;
                productEntity.Price = model.Price;
                productEntity.BomNo = model.BomNo;
                productEntity.weight = model.weight;
                productEntity.IsActive = true;
                productEntity.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
                productEntity.ProductType = model.ProductType;
                productEntity.ProductionTime = model.productiontime;
                productEntity.ProductionPerDay = model.productionperday;
                productEntity.StoreId = model.StoreId;
                productEntity.MinStock = model.MinStock;
                productEntity.MaxStock = model.MaxStock;
                productManager.Post(productEntity);
                status = "Inserted";
            }
            else
            {
                //var totallist = ProductManager.Product_Grid();
                //var productList = totallist.Where(x => x.product_name.ToLower().Contains(model.ProductName.ToLower()) && x.Productype.ToString().ToLower().Contains(model.ProductType.ToString().ToLower())).ToList();
                //if (productList.Count() >= 2)
                //{
                //    status = "ProductThere";
                //    return Json(status, JsonRequestBehavior.AllowGet);
                //}
                //var productListcode = totallist.Where(x => x.product_code.ToLower().Contains(model.ProductCode.ToLower()) && x.Productype.ToString().ToLower().Contains(model.ProductType.ToString().ToLower())).ToList();
                //if (productList.Count() >= 2)
                //{
                //    status = "ProductcodetThere";
                //    return Json(status, JsonRequestBehavior.AllowGet);
                //}
                ProductManager productManager = new ProductManager();
                productEntity.ProductId = model.ProductId;
                productEntity.ProductCode = model.ProductCode;
                productEntity.ProductName = model.ProductName;
                productEntity.ProductDesc = model.ProductDesc;
                productEntity.TaxMasterId = model.TaxMasterId;
                productEntity.UomMasterId = model.UomMasterId;
                productEntity.ProductType = model.ProductType;
                productEntity.ProductionTime = model.productiontime;
                productEntity.ProductionPerDay = model.productionperday;
                productEntity.StoreId = model.StoreId;
                productEntity.MinStock = model.MinStock;
                productEntity.MaxStock = model.MaxStock;
                productEntity.cost = model.cost;
                productEntity.weight = model.weight;
                productEntity.Price = model.Price;
                productEntity.BomNo = model.BomNo;
                productEntity.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
                productManager.Put(productEntity);
                status = "Updated";
            }
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
            model.cost = data.cost;
            model.weight = data.weight;
            model.StoreId = data.StoreId;
            model.MinStock = data.MinStock;
            model.MaxStock = data.MaxStock;
            model.IsActive = data.IsActive;
            model.MaterialCategoryMasterId = data.MaterialCategoryMasterId;
            model.ProductType = data.ProductType;
            model.productiontime = data.ProductionTime;
            model.productionperday = data.ProductionPerDay;
            return PartialView("Partial/Product", model);

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
            ProductManager ProductManager = new ProductManager();
            List<Product> totaldata = new List<Product>();

            var totallist = ProductManager.Product_Grid();
            var productList = totallist.Where(x => x.product_name.ToLower().Contains(filter.ToLower()) || x.product_code.ToLower().Contains(filter.ToLower())).ToList();

            foreach (var i in productList)
            {
                Product product = new Product();
                product.CategoryName = i.CategoryName;
                product.ProductCode = i.product_code;
                product.ProductName = i.product_name;
                product.ProductDesc = i.product_desc;
                product.taxname = i.TaxValue;
                product.uom = i.uom;
                product.Price = i.Price;
                product.BomNo1 = i.bom_no;
                totaldata.Add(product);
            }

            return Json(totaldata, JsonRequestBehavior.AllowGet);
        }
    }

}

